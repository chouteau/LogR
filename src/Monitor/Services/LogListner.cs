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
		Models.MonitoredApplication m_Settings;
		IHandler m_Handler;

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
			m_Handler = new SignalRHandler();
			m_Handler.Received += new Action<string>(m_LoggerConnection_Received);
			m_Handler.Exception += (error) =>
				{
					var ex = error;
					while(true)
					{
						if (ex.InnerException == null)
						{
							break;
						}
						ex = ex.InnerException;						
					}
					if (Error != null)
					{
						Error(ex, m_Settings.SignalRUrl);
					}
				};

			m_Handler.Start(m_Settings, "loglistner");
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
			catch(Exception ex)
			{
				log = new Log();
				log.ApplicationName = "Monitor";
				log.Category = Category.Warn;
				log.CreationDate = DateTime.Now;
				log.HostName = "Monitor";
				log.MachineName = System.Environment.MachineName;
				log.Message = ex.Message;
				log.LogId = Guid.NewGuid().ToString();
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
