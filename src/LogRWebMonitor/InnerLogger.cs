using System;
using System.Collections.Generic;
using System.Linq; 
using System.Text;
using System.Threading.Tasks;

namespace LogRWebMonitor; 
internal class InnerLogger : ILogger
{
	private readonly string _categoryName;

	public InnerLogger(LogRSettings logrSettings, string categoryName, LogCollector collector, ILogRExtender? extender)
	{
		this.LogRSettings = logrSettings;
		this._categoryName = categoryName;
		this.Semaphore = new SemaphoreSlim(1, 1);
		this.WriteQueue = new System.Collections.Concurrent.ConcurrentQueue<LogRPush.LogInfo>();
		this.Collector = collector;
		this.Extender = extender;
	}

	protected LogRSettings LogRSettings { get; }
	protected SemaphoreSlim Semaphore { get; }
	protected System.Collections.Concurrent.ConcurrentQueue<LogRPush.LogInfo> WriteQueue { get; }
	protected LogCollector Collector { get; }
	protected ILogRExtender? Extender { get; }

	public IDisposable? BeginScope<TState>(TState state) 
		where TState : notnull
	{
		return default;
	}

	public bool IsEnabled(LogLevel logLevel)
	{
		return LogRSettings.LogLevel <= logLevel;
	}

	public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception, string> formatter)
	{
		if (!IsEnabled(logLevel))
		{
			return;
		}

		var logInfo = new LogRPush.LogInfo()
		{
			ApplicationName = LogRSettings.ApplicationName,
			CreationDate = DateTime.Now,
			HostName = LogRSettings.HostName,
			MachineName = System.Environment.MachineName,
			Context = _categoryName,
			EnvironmentName = LogRSettings.EnvironmentName,
            Message = $"{formatter(state, exception ?? new())}",
            ExceptionStack = GetExceptionContent(exception),
			LogLevel = logLevel
        };

        var tagList = state as IReadOnlyList<KeyValuePair<string, object?>>;
        if (tagList is not null
            && tagList.Count > 0)
        {
            foreach (var tag in tagList)
            {
                if ("{OriginalFormat}".Equals(tag.Key, StringComparison.InvariantCultureIgnoreCase))
                {
                    continue;
                }
                if (tag.Value is null)
                {
                    continue;
                }
                logInfo.ExtendedParameterList.Add(tag.Key, $"{tag.Value}");
            }
        }

        if (Extender != null)
		{
			logInfo.ExtendedParameterList = Extender.GetParameters();
		}

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
				WriteInternal(logInfo);
				continue;
			}
			break;
		}
	}

	private void WriteInternal(LogRPush.LogInfo logInfo)
	{
		Semaphore.Wait();
		try
		{
			Collector.AddLog(logInfo);
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.Message);
		}
		finally
		{
			Semaphore.Release();
		}
		Dequeue();
	}

	private static string? GetExceptionContent(Exception? ex)
	{
		if (ex is null)
		{
			return null;
		}

		var content = new StringBuilder();
		content.AppendLine("--------------------------------------------");
		content.AppendLine();
		content.AppendLine(ex.Message);
		content.AppendLine("--------------------------------------------");

		// Ajout des extensions d'erreur
		if (ex.Data != null
			&& ex.Data.Count > 0)
		{
			foreach (var item in ex.Data.Keys)
			{
				if (item != null && ex.Data != null && ex.Data[item] != null)
				{
					string data = string.Empty;
					try
					{
						data = $"{ex.Data[item]}";
						content.AppendFormat("{0} = {1}", item, data);
					}
					catch { /* do nothing */ }
				}
				content.AppendLine();
			}
		}

		content.Append(ex.StackTrace);
		content.AppendLine();
		if (ex.InnerException != null)
		{
			content.Append("--------------------------------------------");
			content.AppendLine();
			content.Append("Inner Exception");
			content.AppendLine();
			content.Append(GetExceptionContent(ex.InnerException));
		}
		return content.ToString();
	}


}

