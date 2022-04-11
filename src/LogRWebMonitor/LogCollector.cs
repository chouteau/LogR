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
    public event Action<LogRPush.LogInfo> OnAddLog;

    public LogCollector(LogRSettings logRSettings)
    {
        this.Settings = logRSettings;
        this._logDic = new System.Collections.Concurrent.ConcurrentDictionary<string, LogRPush.LogInfo>();
        this.Semaphore = new SemaphoreSlim(1, 1);
        this.WriteQueue = new System.Collections.Concurrent.ConcurrentQueue<LogRPush.LogInfo>();
    }

    protected LogRSettings Settings { get; }
    protected SemaphoreSlim Semaphore { get; }
    protected System.Collections.Concurrent.ConcurrentQueue<LogRPush.LogInfo> WriteQueue { get; }


    public void AddLog(LogRPush.LogInfo logInfo)
    {
        WriteQueue.Enqueue(logInfo);
        if (Semaphore.CurrentCount < 2)
        {
            Dequeue();
        }
    }

    private void Dequeue()
    {
        while (true)
        {
            bool result = WriteQueue.TryDequeue(out LogRPush.LogInfo logInfo);
            if (result)
            {
                AddInternal(logInfo);
                continue;
            }
            break;
        }
    }

    private void AddInternal(LogRPush.LogInfo logInfo)
	{
        Semaphore.Wait();
        if (_logDic.Count > Settings.LogCountMax)
        {
            var first = _logDic.First();
            _logDic.TryRemove(first.Key, out var byebye);
        }
        _logDic.TryAdd(logInfo.LogId, logInfo);
        try
        {
            OnAddLog?.Invoke(logInfo);
            OnChanged?.Invoke();
            Settings.AddLogInfo(logInfo);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
        Semaphore.Release();
        Dequeue();
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
                result = result.Where(i => IsMatchSearch(i, logFilter.Search));
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
                result = result.Where(i => i.HostName.Equals(logFilter.HostName, StringComparison.InvariantCultureIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(logFilter.ApplicationName))
            {
                result = result.Where(i => i.ApplicationName.Equals(logFilter.ApplicationName, StringComparison.InvariantCultureIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(logFilter.Context))
            {
                result = result.Where(i => i.Context.Equals(logFilter.Context, StringComparison.InvariantCultureIgnoreCase));
            }
        }
        return result.OrderByDescending(i => i.CreationDate)
                        .Take(logFilter.Top)
                        .ToList();
    }

    public LogRPush.LogInfo GetLogInfo(string logId)
	{
        _logDic.TryGetValue(logId, out var result);
        return result;
    }

    public bool IsMatchSearch(LogRPush.LogInfo i, string search)
	{
        if (string.IsNullOrWhiteSpace(search))
		{
            return false;
		}
        return $"{i.MachineName}{i.Message}{i.ApplicationName}{i.Context}{i.HostName}{i.ExceptionStack}".IndexOf(search, StringComparison.InvariantCultureIgnoreCase) != -1;
    }
}