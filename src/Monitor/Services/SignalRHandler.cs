using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNet.SignalR.Client;

namespace LogR.Monitor.Services
{
	public class SignalRHandler : IHandler
	{
		private Connection m_Connection;

		#region ILoggerHandler Members

		public event Action<string> Received;
		public event Action<Exception> Exception;
		public event Action Disconnected;

		public void Start(Models.MonitoredApplication settings, string clientName)
		{
			var url = string.Format("{0}/{1}", settings.SignalRUrl.Trim('/'), settings.ApiKey);
			this.m_Connection = new Connection(url);
			m_Connection.Headers.Add("apiKey", settings.ApiKey);
			this.m_Connection.Received += Received;
			this.m_Connection.Error += (ex) =>
				{
					if (Exception != null)
					{
						ex.Data.Add("url", url);
						Exception(ex);
					}
				};
			this.m_Connection.Reconnected += () =>
				{
					if (Exception != null)
					{
						Exception(new Exception("Task listner reconnected"));
					}
				};
			this.m_Connection.Start().ContinueWith((task) =>
			{
				if (task.IsFaulted)
				{
					if (Exception != null)
					{
						Exception(task.Exception);
					}
				}
			}).Wait(10 * 1000);

			this.m_Connection.Closed += OnDisconnect;
		}

		public void OnDisconnect()
		{
			bool islive = false;
			m_Connection.Start().ContinueWith((task) =>
			{
				if (!task.IsFaulted)
				{
					islive = true;
				}
			}).Wait(30 * 1000);

			if (islive)
			{
				OnDisconnect();
			}
		}

		public void Stop()
		{
			if (m_Connection != null)
			{
				// m_LoggerConnection.Disconnect();
			}
		}

		#endregion

		public void Dispose()
		{
			m_Connection = null;
		}
	}
}
