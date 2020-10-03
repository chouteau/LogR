using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using LogRMonitor.Models;
using Microsoft.AspNet.SignalR.Client.Hubs;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;

namespace LogRMonitor.Services
{
	public class LogListner
	{
		Models.MonitoredApplication m_Settings;
		HubConnection m_HubConnection;

		public LogListner(Models.MonitoredApplication settings)
		{
			m_Settings = settings;
		}

		public Action<Models.Log> AddLog;
		public event Action<Exception,string> Error;

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
								.WithUrl(m_Settings.SignalRUrl)
								.Build();

			m_HubConnection.Closed += async (error) =>
			{
				try
				{
					await m_HubConnection.StartAsync();
				}
				catch
				{

				}
			};

			m_HubConnection.On<Models.Log>("WriteLog", log =>
			{
				if (log == null)
				{
					return;
				}

				if (AddLog != null)
				{
					AddLog(log);
				}
			});

			AddLog(new Log() 
			{ 
				ApplicationName = "Monitor", 
				Category = Category.Info, 
				CreationDate = DateTime.Now,
				HostName = "Monitor",
				MachineName = System.Environment.MachineName,
				Message = $"Ready for listen {m_Settings.SignalRUrl}",
				LogId = Guid.NewGuid().ToString()
			});

			m_HubConnection.StartAsync();
		}
	}
}
