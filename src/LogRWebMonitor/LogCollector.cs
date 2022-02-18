using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogRWebMonitor;
public class LogCollector
{
    private readonly System.Collections.Concurrent.ConcurrentDictionary<string, LogRPush.LogInfo> _logDic;

    public event Action OnChanged;

    public LogCollector(LogRSettings logRSettings)
    {
        this.Settings = logRSettings;
        this._logDic = new System.Collections.Concurrent.ConcurrentDictionary<string, LogRPush.LogInfo>();
    }

    protected LogRSettings Settings { get; }

    public void AddLog(LogRPush.LogInfo logInfo)
    {
        if (_logDic.Count > Settings.LogCountMax)
        {
            var first = _logDic.First();
            _logDic.Remove(first.Key, out var byebye);
        }
        _logDic.TryAdd(logInfo.LogId, logInfo);
        OnChanged?.Invoke();
    }

    public void Clear()
    {
        _logDic.Clear();
    }

    public IEnumerable<LogRPush.LogInfo> GetLogInfoList(LogFilter logFilter = null)
    {
        var result = _logDic.Values.AsQueryable();
        if (logFilter != null)
        {
            if (!string.IsNullOrWhiteSpace(logFilter.Search))
			{
                result = result.Where(i => $"{i.MachineName}{i.Message}{i.ApplicationName}{i.Context}{i.HostName}{i.ExceptionStack}".IndexOf(logFilter.Search, StringComparison.InvariantCultureIgnoreCase) != -1);
			}

            if (logFilter.LevelList.Any(i => i.Checked))
			{
                var levelList = logFilter.LevelList.Where(i => i.Checked).Select(i => i.Value);
                result = result.Where(i => levelList.Contains(i.Category));
			}

            if (!string.IsNullOrWhiteSpace(logFilter.MachineName))
			{
                result = result.Where(i => i.MachineName.Equals(logFilter.MachineName, StringComparison.InvariantCultureIgnoreCase));
			}

            if (!string.IsNullOrWhiteSpace(logFilter.HostName))
            {
                result = result.Where(i => i.MachineName.Equals(logFilter.HostName, StringComparison.InvariantCultureIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(logFilter.ApplicationName))
            {
                result = result.Where(i => i.MachineName.Equals(logFilter.ApplicationName, StringComparison.InvariantCultureIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(logFilter.Context))
            {
                result = result.Where(i => i.MachineName.Equals(logFilter.Context, StringComparison.InvariantCultureIgnoreCase));
            }
        }
        return result.OrderByDescending(i => i.CreationDate)
                        .Take(Settings.LogCountMax -1)
                        .ToList();
    }
}