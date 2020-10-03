using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;

namespace LogRCore
{
	public class LogRLogger : ILogger
	{
		public LogRLogger(LogRConfiguration config,
			IHubContext<LogRHub> hubContext,
			string configurationName)
		{
			this.Config = config;
			this.HubContext = hubContext;
			this.Semaphore = new SemaphoreSlim(1, 1);
			this.WriteQueue = new System.Collections.Concurrent.ConcurrentQueue<LogInfo>();
			this.ConfigurationName = configurationName;
		}

		protected LogRConfiguration Config { get; }
		protected IHubContext<LogRHub> HubContext { get; }
		protected SemaphoreSlim Semaphore { get; }
		protected System.Collections.Concurrent.ConcurrentQueue<LogInfo> WriteQueue { get; }
		protected string ConfigurationName { get; }

		public IDisposable BeginScope<TState>(TState state)
		{
			return null;
		}

		public bool IsEnabled(LogLevel logLevel)
		{
			return Config.LogLevel <= logLevel;
		}

		public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
		{
			if (!IsEnabled(logLevel))
			{
				return;
			}

			var logInfo = new LogInfo()
			{
				ApplicationName = Config.ApplicationName,
				CreationDate = DateTime.Now,
				HostName = Config.HostName,
				MachineName = Config.MachineName,
				Context = ConfigurationName,
				Message = $"{formatter(state, exception)}",
			};

			logInfo.ExceptionStack = exception.GetExceptionContent();

			switch (logLevel)
			{
				case LogLevel.Trace:
					logInfo.Category = Category.Debug;
					break;
				case LogLevel.Debug:
					logInfo.Category = Category.Debug;
					break;
				case LogLevel.Information:
					logInfo.Category = Category.Info;
					break;
				case LogLevel.Warning:
					logInfo.Category = Category.Warn;
					break;
				case LogLevel.Error:
					logInfo.Category = Category.Error;
					break;
				case LogLevel.Critical:
					logInfo.Category = Category.Fatal;
					break;
				case LogLevel.None:
					logInfo.Category = Category.Debug;
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
				bool result = WriteQueue.TryDequeue(out LogInfo logInfo);
				if (result)
				{
					WriteInternal(logInfo);
					continue;
				}
				break;
			}
		}

		private void WriteInternal(LogInfo logInfo)
		{
			Semaphore.Wait();
			try
			{
				HubContext.Clients.All.SendAsync("WriteLog", logInfo).Wait(5 * 1000);
			}
			finally
			{
				Semaphore.Release();
			}
			Dequeue();
		}
	}
}
