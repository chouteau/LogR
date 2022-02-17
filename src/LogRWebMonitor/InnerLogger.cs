using System;
using System.Collections.Generic;
using System.Linq; 
using System.Text;
using System.Threading.Tasks;

namespace LogRWebMonitor; 
internal class InnerLogger : ILogger
{
	private readonly string _categoryName;

	public InnerLogger(LogRSettings logrSettings, string categoryName, LogCollector collector)
	{
		this.LogRSettings = logrSettings;
		this._categoryName = categoryName;
		this.Semaphore = new SemaphoreSlim(1, 1);
		this.WriteQueue = new System.Collections.Concurrent.ConcurrentQueue<LogRPush.LogInfo>();
		this.Collector = collector;
	}

	protected LogRSettings LogRSettings { get; }
	protected SemaphoreSlim Semaphore { get; }
	protected System.Collections.Concurrent.ConcurrentQueue<LogRPush.LogInfo> WriteQueue { get; }
	protected LogCollector Collector { get; }

	public IDisposable BeginScope<TState>(TState state)
	{
		return null;
	}

	public bool IsEnabled(LogLevel logLevel)
	{
		return LogRSettings.LogLevel <= logLevel;
	}

	public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
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
			Message = $"{formatter(state, exception)}",
		};

		logInfo.ExceptionStack = GetExceptionContent(exception);

		switch (logLevel)
		{
			case LogLevel.Trace:
				logInfo.Category = LogRPush.Category.Trace;
				break;
			case LogLevel.Debug:
				logInfo.Category = LogRPush.Category.Debug;
				break;
			case LogLevel.Information:
				logInfo.Category = LogRPush.Category.Info;
				break;
			case LogLevel.Warning:
				logInfo.Category = LogRPush.Category.Warn;
				break;
			case LogLevel.Error:
				logInfo.Category = LogRPush.Category.Error;
				break;
			case LogLevel.Critical:
				logInfo.Category = LogRPush.Category.Fatal;
				break;
			case LogLevel.None:
				logInfo.Category = LogRPush.Category.Debug;
				break;
			default:
				break;
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
			bool result = WriteQueue.TryDequeue(out LogRPush.LogInfo logInfo);
			if (result)
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

	private static string GetExceptionContent(Exception ex, int level = 0)
	{
		if (ex == null)
		{
			return null;
		}

		var content = new StringBuilder();
		content.Append("--------------------------------------------");
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
						data = ex.Data[item].ToString();
						content.AppendFormat("{0} = {1}", item, data);
					}
					catch { }
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
			content.Append(GetExceptionContent(ex.InnerException, level++));
		}
		return content.ToString();
	}


}

