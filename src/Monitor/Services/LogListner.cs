using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using LogR.Monitor.Models;

namespace LogR.Monitor.Services
{
	public class LogListner
	{
		AutoResetEvent m_Terminated = new AutoResetEvent(false);
		// System.Threading.Thread m_LogThread;
		Models.MonitoredApplication m_Settings;
		IHandler m_Handler;

		public LogListner(Models.MonitoredApplication settings)
		{
			m_Settings = settings;
		}

		public Action<Models.Log> AddLog;
		public event Action<Exception> Error;

		public void Start()
		{
			// m_LogThread = new System.Threading.Thread(StartLogMonitoring);
			// m_LogThread.Start();
			StartLogMonitoring();
		}

		public void Stop()
		{
			m_Terminated.Set();
			//if (m_LogThread != null)
			//{
			//	m_LogThread.Join(1 * 1000);
			//	m_LogThread.Abort();
			//}
		}

		private void StartLogMonitoring()
		{
			// var resynchronize = new AutoResetEvent(false);
			m_Handler = new SignalRHandler();
			m_Handler.Received += new Action<string>(m_LoggerConnection_Received);
			m_Handler.Exception += (error) =>
				{
					if (Error != null)
					{
						Error(error);
					}
					m_Terminated.Set();
				};

			m_Handler.Start(m_Settings, "loglistner");
			AddLog(new Log() 
			{ 
				ApplicationName = "Monitor", 
				Category = Category.Debug, 
				CreationDate = DateTime.Now,
				HostName = "Monitor",
				MachineName = System.Environment.MachineName,
				Message = "Ready for " + m_Settings.SignalRUrl,
				LogId = Guid.NewGuid().ToString()
			});
		}

		private void m_LoggerConnection_Received(string obj)
		{
			if (obj == null
				|| obj.Trim() == string.Empty)
			{
				return;
			}

			Models.Log log = null;
			try
			{
				log = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.Log>(obj);
			}
			catch
			{
				return;
			}

			if (log == null)
			{
				return;
			}

			if (AddLog != null)
			{
				AddLog(log);
			}
		}

	}
}
