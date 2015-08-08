using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogR
{
	public class TestsLogger : ILogger
	{
		public TestsLogger()
		{
			System.Diagnostics.Debug.AutoFlush = true;
		}

		private System.Diagnostics.TextWriterTraceListener m_Out;

		public TextWriter Out
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

		public void Debug(string message)
		{
			System.Diagnostics.Debug.WriteLine(Format(message), "Debug");
		}

		public void Debug(string message, params object[] prms)
		{
			System.Diagnostics.Debug.WriteLine(Format(message, prms), "Debug");
		}

		public void Dispose()
		{
		}

		public void Error(Exception x)
		{
			System.Diagnostics.Debug.WriteLine(string.Empty);
			System.Diagnostics.Debug.WriteLine(Format(x.ToString()), "Error");
			System.Diagnostics.Debug.WriteLine(string.Empty);
		}

		public void Error(string message)
		{
			System.Diagnostics.Debug.WriteLine(string.Empty);
			System.Diagnostics.Debug.WriteLine(Format(message), "Error");
			System.Diagnostics.Debug.WriteLine(string.Empty);
		}

		public void Error(string message, params object[] prms)
		{
			System.Diagnostics.Debug.WriteLine(string.Empty);
			System.Diagnostics.Debug.WriteLine(Format(message, prms), "Error");
			System.Diagnostics.Debug.WriteLine(string.Empty);
		}

		public void Fatal(Exception x)
		{
			System.Diagnostics.Debug.WriteLine(string.Empty);
			System.Diagnostics.Debug.WriteLine(Format(x.ToString()), "Fatal");
			System.Diagnostics.Debug.WriteLine(string.Empty);
		}

		public void Fatal(string message)
		{
			System.Diagnostics.Debug.WriteLine(string.Empty);
			System.Diagnostics.Debug.WriteLine(Format(message), "Fatal");
			System.Diagnostics.Debug.WriteLine(string.Empty);
		}

		public void Fatal(string message, params object[] prms)
		{
			System.Diagnostics.Debug.WriteLine(string.Empty);
			System.Diagnostics.Debug.WriteLine(Format(message, prms), "Fatal");
			System.Diagnostics.Debug.WriteLine(string.Empty);
		}

		public void Info(string message)
		{
			System.Diagnostics.Debug.WriteLine(Format(message), "Info");
		}

		public void Info(string message, params object[] prms)
		{
			System.Diagnostics.Debug.WriteLine(Format(message, prms), "Info");
		}

		public void Notification(string message)
		{
			System.Diagnostics.Debug.WriteLine(Format(message), "Notification");
		}

		public void Notification(string message, params object[] prms)
		{
			System.Diagnostics.Debug.WriteLine(Format(message, prms), "Notification");
		}

		public void Sql(string message)
		{
			System.Diagnostics.Debug.WriteLine(Format(message), "Sql");
		}

		public void Warn(string message)
		{
			System.Diagnostics.Debug.WriteLine(Format(message), "Warn");
		}

		public void Warn(string message, params object[] prms)
		{
			System.Diagnostics.Debug.WriteLine(Format(message, prms),"Warn");
		}

		public void Watch(string title, Action method)
		{
			var watch = System.Diagnostics.Stopwatch.StartNew();
			watch.Start();
			method.Invoke();
			watch.Stop();
			Debug("{0} = {1}ms", title, watch.ElapsedMilliseconds);
		}

		string Format(string message, params object[] prms)
		{
			var row = string.Format("\t{0:HH}H{0:mm}:{0:ss}.{0:ffff}|{1}({2}){3}",
				DateTime.Now,
				System.Threading.Thread.CurrentThread.Name,
				System.Threading.Thread.CurrentThread.ManagedThreadId,
				string.Format(message, prms)
				);
			return row;
		}

	}
}
