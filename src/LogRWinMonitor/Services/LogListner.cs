using System;

using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;

namespace LogWinRMonitor.Services
{
	public class LogListner(Models.MonitoredApplication settings)
	{
		HubConnection m_HubConnection;

		public Action<LogRPush.LogInfo> AddLog { get; set; }
		public event Action<Exception, string> Error;

		public void Start()
		{
			StartLogMonitoring();
		}

		public void Stop()
		{
		}

		private void StartLogMonitoring()
		{
			m_HubConnection = new HubConnectionBuilder()
								.ConfigureLogging(c => c.SetMinimumLevel(LogLevel.None))
								.WithAutomaticReconnect()
								.WithUrl(settings.SignalRUrl)
								.Build();

			var endPoint = new Uri(settings.SignalRUrl).PathAndQuery.TrimStart('/');

			m_HubConnection.Closed += async (error) =>
			{
				try
				{
					await m_HubConnection.StartAsync();
				}
				catch (Exception ex)
				{
					AddLog(new LogRPush.LogInfo
					{
						ApplicationName = "Monitor",
						LogLevel = LogLevel.Error,
						CreationDate = DateTime.Now,
						HostName = "Monitor",
						MachineName = System.Environment.MachineName,
						Message = $"Error on connect to {settings.SignalRUrl} - {ex.Message}",
					});
				}
			};

			m_HubConnection.On<LogRPush.LogInfo>("WriteLog", log =>
			{
				if (log is null)
				{
					return;
				}

				if (log.Message?.IndexOf($"{endPoint}", StringComparison.InvariantCultureIgnoreCase) != -1)
				{
					return;
				}

				if (AddLog != null)
				{
					AddLog(log);
				}
			});

			AddLog(new LogRPush.LogInfo()
			{
				ApplicationName = "Monitor",
				LogLevel = LogLevel.Information,
				CreationDate = DateTime.Now,
				HostName = "Monitor",
				MachineName = System.Environment.MachineName,
				Message = $"Ready for listen {settings.SignalRUrl}",
			});

			m_HubConnection.StartAsync();
		}
	}
}
