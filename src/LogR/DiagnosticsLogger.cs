using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace LogR
{
	public class DiagnosticsLogger : ILogger
	{
		public DiagnosticsLogger()
		{
			// var consoleListener = new System.Diagnostics.ConsoleTraceListener();
			// System.Diagnostics.Debug.AutoFlush = true;
			// System.Diagnostics.Trace.AutoFlush = true;
			// System.Diagnostics.Debug.Listeners.Clear();
			// System.Diagnostics.Trace.Listeners.Clear();
			// System.Diagnostics.Debug.Listeners.Add(consoleListener);
			// System.Diagnostics.Trace.Listeners.Add(consoleListener);
		}

		public void Info(string message)
		{
			try
			{
				System.Diagnostics.Trace.TraceInformation(GetRow("Info", message));
			}
			catch { }
		}

		public void Sql(string message)
		{
			try
			{
				System.Diagnostics.Trace.TraceInformation(GetRow("Linq", message));
			}
			catch { }
		}

		public void Info(string message, params object[] prms)
		{
			try
			{
				System.Diagnostics.Trace.TraceInformation(GetRow("Info",message, prms));
			}
			catch { }
		}

		public void Notification(string message)
		{
			try
			{
				System.Diagnostics.Trace.TraceWarning(GetRow("Notification", message));
			}
			catch { }
		}

		public void Notification(string message, params object[] prms)
		{
			try
			{
				System.Diagnostics.Trace.TraceWarning(GetRow("Notification", message, prms));
			}
			catch { }
		}

		public void Warn(string message)
		{
			try
			{
				System.Diagnostics.Trace.TraceWarning(GetRow("Warn", message));
			}
			catch { }
		}

		public void Warn(string message, params object[] prms)
		{
			try
			{
				System.Diagnostics.Trace.TraceWarning(GetRow("Warn",message, prms));
			}
			catch { }
		}

		public void Debug(string message)
		{
			try
			{
				System.Diagnostics.Debug.WriteLine(GetRow("Debug", message, "Debug"));
			}
			catch { }
		}

		public void Debug(string message, params object[] prms)
		{
			try
			{
				System.Diagnostics.Debug.WriteLine(GetRow("Debug",message, prms));
			}
			catch { }
		}

		public void Error(string message)
		{
			try
			{
				System.Diagnostics.Trace.TraceError(GetRow("Error", message));
			}
			catch { }
		}

		public void Error(string message, params object[] prms)
		{
			try
			{
				System.Diagnostics.Trace.TraceError(GetRow("Error", message, prms));
			}
			catch { }
		}

		public void Error(Exception x)
		{
			try
			{
				var message = GetContent(x);
				System.Diagnostics.Trace.TraceError(GetRow("Error", message));
			}
			catch { }
		}

		public void Fatal(string message)
		{
			try
			{
				System.Diagnostics.Trace.TraceError(GetRow("Fatal", message));
			}
			catch { }
		}

		public void Fatal(string message, params object[] prms)
		{
			try
			{
				System.Diagnostics.Trace.TraceError(GetRow("Fatal", message, prms));
			}
			catch { }
		}

		public void Fatal(Exception x)
		{
			try
			{
				var message = GetContent(x);
				System.Diagnostics.Trace.TraceError(GetRow("Fatal", message));
			}
			catch { }
		}

        private System.Diagnostics.TextWriterTraceListener m_Out;

		public System.IO.TextWriter Out
		{
			get
			{
				if (m_Out == null)
				{
                    m_Out = new System.Diagnostics.TextWriterTraceListener();
				}
				return m_Out.Writer;
			}
		}

		public void Watch(string title, Action method)
		{
			var watch = Stopwatch.StartNew();
			watch.Start();
			method.Invoke();
			watch.Stop();
			Debug("{0} = {1}ms", title, watch.ElapsedMilliseconds);
		}

        string GetRow(string prf, string message, params object[] prms)
        {
			var row = string.Format("{0:HH}H{0:mm}:{0:ss}.{0:ffff}|{1}({2})\r\n{3}\r\n", 
				DateTime.Now, 
				System.Threading.Thread.CurrentThread.Name, 
				System.Threading.Thread.CurrentThread.ManagedThreadId, 
				string.Format(message, prms)
				);
			return row;
        }

		private string GetContent(System.Exception ex)
		{
			var content = new StringBuilder();
			content.Append(ex.Message);
			content.Append(ex.StackTrace);
			content.AppendLine();
			content.Append("--------------------------------------------");
			content.AppendLine();
			if (ex.Data != null && ex.Data.Count > 0)
			{
				foreach (object item in ex.Data.Keys)
				{
					if (item != null && ex.Data != null && ex.Data[item] != null)
					{
						content.AppendFormat("{0} = {1}", item, ex.Data[item]);
					}
					content.AppendLine();
				}
			}
			if (ex.InnerException != null)
			{
				content.Append("--------------------------------------------");
				content.AppendLine();
				content.Append("Inner Exception");
				content.AppendLine();
				content.Append(this.GetContent(ex.InnerException));
			}
			return content.ToString();
		}

		public void Dispose()
		{

		}
	}
}
