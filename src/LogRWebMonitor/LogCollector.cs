using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace LogRWebMonitor;
public class LogCollector
{
    private readonly System.Collections.Concurrent.ConcurrentDictionary<string, LogRPush.LogInfo> _logDic = new();
    private readonly System.Collections.Concurrent.ConcurrentDictionary<string, LogRPush.LogInfo> _logDetails = new();
	private readonly IHubContext<LogRHub> _hubContext;
	private int _rowCount = 0;

    public event Action<LogRPush.LogInfo> OnAddLog = default!;

    public LogCollector(LogRSettings logRSettings, IHubContext<LogRHub> hubContext)
    {
        this.Settings = logRSettings;
		_hubContext = hubContext;
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
            bool result = WriteQueue.TryDequeue(out LogRPush.LogInfo? logInfo);
            if (result
                && logInfo is not null)
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
        if (logInfo.Category == LogRPush.Category.Error
            || logInfo.Category == LogRPush.Category.Fatal)
		{
            logInfo.StackChecksum = CalculateChecksum(logInfo);
            var existing = _logDic.Values.FirstOrDefault(i => i.StackChecksum == logInfo.StackChecksum);
            if (existing != null)
			{
                logInfo.ExceptionCount = existing.ExceptionCount + 1;
                _logDic.TryRemove(existing.LogId, out var byebye);
			}
		}
        _logDic.TryAdd(logInfo.LogId, logInfo);

        logInfo.RowNumber = _rowCount++;
        try
        {
            OnAddLog?.Invoke(logInfo);
			_hubContext.Clients.All.SendAsync("WriteLog", logInfo);
		}
		catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

		Semaphore.Release();
    }


    public void Clear()
    {
        _logDic.Clear();
    }

    public List<LogRPush.LogInfo> GetLogInfoList(LogFilter? logFilter = null)
	{
		var result = _logDic.Values.AsQueryable();
		if (logFilter is not null)
		{
			if (!string.IsNullOrWhiteSpace(logFilter.Search))
			{
				result = result.Where(i => IsMatchSearch(i, logFilter.Search));
			}

			if (logFilter.LevelList.Exists(i => i.Checked))
			{
				var levelList = logFilter.LevelList.Where(i => i.Checked).Select(i => i.Value);
				result = result.Where(i => levelList.Contains(i.Category));
			}

			if (logFilter.MachineNameList.Count > 0)
			{
				result = result.Where(l => logFilter.MachineNameList.Any(m => l.MachineName.Equals(m, StringComparison.InvariantCultureIgnoreCase)));
			}

			if (logFilter.HostNameList.Count > 0)
			{
				result = result.Where(l => logFilter.HostNameList.Any(m => l.HostName.Equals(m, StringComparison.InvariantCultureIgnoreCase)));
			}

			if (logFilter.ApplicationNameList.Count > 0)
			{
				result = result.Where(l => logFilter.ApplicationNameList.Any(m => l.ApplicationName.Equals(m, StringComparison.InvariantCultureIgnoreCase)));
			}

			if (logFilter.ContextList.Count > 0)
			{
				result = result.Where(l => logFilter.ContextList.Any(m => l.Context.Equals(m, StringComparison.InvariantCultureIgnoreCase)));
			}

			return result.OrderByDescending(i => i.CreationDate)
							.Take(logFilter.Top)
							.ToList();
		}
		return result.OrderByDescending(i => i.CreationDate)
				.Take(200)
				.ToList();
	}

    public void AddLogDetail(LogRPush.LogInfo logInfo)
    {
        _logDetails.AddOrUpdate(logInfo.LogId, logInfo, (old, exiting) => logInfo);
    }

    public LogRPush.LogInfo? GetLogInfo(string logId)
	{
        _logDetails.TryGetValue(logId, out var result);
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

    private string CalculateChecksum(LogRPush.LogInfo logInfo)
	{
        using var crypto = System.Security.Cryptography.SHA256.Create();
        var input = $"{logInfo.MachineName}{logInfo.ApplicationName}{logInfo.ExceptionStack}";
        var buffer = System.Text.Encoding.UTF8.GetBytes(input);
        var hash = crypto.ComputeHash(buffer);
        var result = string.Join(string.Empty, from b in hash select b.ToString("X2"));
        return result;
    }
}