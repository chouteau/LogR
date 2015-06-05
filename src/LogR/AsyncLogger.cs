using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Net;

using Newtonsoft.Json;

namespace LogR
{
	internal class AsyncLogger : IDisposable
	{
		private Queue<Action> m_Queue = new Queue<Action>();
		private ManualResetEvent m_NewLog = new ManualResetEvent(false);
		private ManualResetEvent m_Terminate = new ManualResetEvent(false);
		private bool m_Terminated = false;
		private Thread m_LoggingThread;
		private DiagnosticsLogger m_InnerLogger;

		private AsyncLogger()
		{
			m_LoggingThread = new Thread(new ThreadStart(ProcessQueue));
			m_LoggingThread.Name = "LogRThread";
			m_LoggingThread.IsBackground = true;
			m_LoggingThread.Start();

			m_InnerLogger = new DiagnosticsLogger();
		}

		private static AsyncLogger m_Current;
		private static object m_Lock = new object();

		public static AsyncLogger Current
		{
			get
			{
				if (m_Current == null)
				{
					lock (m_Lock)
					{
						if (m_Current == null)
						{
							m_Current = new AsyncLogger();
						}
					}
				}
				return m_Current;
			}
		}

		void ProcessQueue()
		{
			while (!m_Terminated)
			{
				var waitHandles = new WaitHandle[] { m_Terminate, m_NewLog };
				int result = ManualResetEvent.WaitAny(waitHandles, 60 * 1000, true);
				if (result == 0)
				{
					m_Terminated = true;
					break;
				}
				m_NewLog.Reset();

				if (m_Queue.Count == 0)
				{
					continue;
				}
				// Enqueue
				Queue<Action> queueCopy;
				lock (m_Queue)
				{
					queueCopy = new Queue<Action>(m_Queue);
					m_Queue.Clear();
				}

				if (m_Terminated)
				{
					break;
				}

				foreach (var log in queueCopy)
				{
					log();
				}
			}
		}

		public void LogMessage(LogInfo log)
		{
			lock (m_Queue)
			{
				m_Queue.Enqueue(() => AsyncLogMessage(log));
			}
			m_NewLog.Set();
		}

		private void AsyncLogMessage(LogInfo log)
		{
			if (log == null)
			{
				return;
			}

			log.Message = log.Message.Substring(0, Math.Min(10000, log.Message.Length));
			try
			{
				switch (log.Category)
				{
					case Category.Debug:
						m_InnerLogger.Debug(log.Message);
						break;
					case Category.Info:
						m_InnerLogger.Info(log.Message);
						break;
					case Category.Sql:
						m_InnerLogger.Sql(log.Message);
						break;
					case Category.Error:
						// SendMessage("Error", log.Message, log.Exception.Message);
						m_InnerLogger.Error(log.Exception);
						break;
					case Category.Warn:
						m_InnerLogger.Warn(log.Message);
						break;
					case Category.Fatal:
						m_InnerLogger.Fatal(log.Exception);
						SendMessage("Fatal", log.Message, log.Exception.Message);
						break;
					case Category.Notification:
						m_InnerLogger.Notification(log.Message);
						SendMessage("Notification", log.Message, null);
						break;
					default:
						break;
				}
			}
			catch
			{
				System.Diagnostics.EventLog.WriteEntry("Application", log.Message, System.Diagnostics.EventLogEntryType.Error);
			}

			log.MachineName = System.Environment.MachineName;
			log.HostName = GlobalConfiguration.Configuration.HostName;
			log.ApplicationName = GlobalConfiguration.Configuration.ApplicationName;
			log.Context = GlobalConfiguration.Configuration.Context;

			var content = Newtonsoft.Json.JsonConvert.SerializeObject(log);

			try
			{
				LoggerConnection.PushMessage(content);
			}
			catch (Exception ex)
			{
				System.Diagnostics.EventLog.WriteEntry("Application", ex.ToString(), System.Diagnostics.EventLogEntryType.Error);
			}
		}

		public void Dispose()
		{
			try
			{
				m_Terminated = true;
				if (m_Terminate != null)
				{
					m_Terminate.Set();
				}
				if (m_LoggingThread != null
					&& !m_LoggingThread.Join(TimeSpan.FromSeconds(5)))
				{
					m_LoggingThread.Abort();
				}
			}
			catch
			{
				// Dead for science
			}
		}

		void SendMessage(string prefix, string body, string subject, params object[] prms)
		{
			if (prms != null
				&& prms.Length > 0)
			{
				body = string.Format(body, prms);
			}

			var message = new System.Net.Mail.MailMessage(GlobalConfiguration.Configuration.FromEmail , GlobalConfiguration.Configuration.ToEmail);
			var appName = GlobalConfiguration.Configuration.ApplicationName;
			subject = "[" + appName + "][" + prefix + "] " + (subject ?? body ?? string.Empty);
			subject = subject.Substring(0, Math.Min(subject.Length, 70));
			message.Subject = subject;
			message.Body = body;

			var sender = new System.Net.Mail.SmtpClient();
			if (sender.DeliveryMethod == System.Net.Mail.SmtpDeliveryMethod.SpecifiedPickupDirectory)
			{
				if (sender.PickupDirectoryLocation.StartsWith(@".\"))
				{
					var currentPath = System.IO.Path.GetDirectoryName(this.GetType().Assembly.Location);
					var fullPath = System.IO.Path.Combine(currentPath, sender.PickupDirectoryLocation.Replace(@".\",""));
					sender.PickupDirectoryLocation = fullPath;
					if (!System.IO.Directory.Exists(sender.PickupDirectoryLocation))
					{
						System.IO.Directory.CreateDirectory(sender.PickupDirectoryLocation);
					}
				}
			}
			try
			{
				sender.Send(message);
			}
			catch
			{
				System.Diagnostics.EventLog.WriteEntry("Application", subject + body, System.Diagnostics.EventLogEntryType.Error);
			}
		}
	}
}
