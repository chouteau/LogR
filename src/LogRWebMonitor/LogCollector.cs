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

    public IEnumerable<LogRPush.LogInfo> GetLogInfoList(Func<LogRPush.LogInfo, bool> predicate = null)
    {
        var result = _logDic.Values.OrderByDescending(i => i.CreationDate).ToList();
        if (predicate != null)
        {
            result = _logDic.Values.Where(predicate)
                        .Take(100).ToList();
        }
        return result;
    }
}