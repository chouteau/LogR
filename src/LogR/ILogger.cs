using System;
using System.Collections.Generic;
using System.Text;

namespace LogR
{
	public interface ILogger : IDisposable
	{
		void Info(string message);
		void Info(string message, params object[] prms);
		void Sql(string message);
		void Warn(string message);
		void Warn(string message, params object[] prms);
		void Debug(string message);
		void Debug(string message, params object[] prms);
		void Error(string message);
		void Error(string message, params object[] prms);
		void Error(Exception x);
		void Fatal(string message);
		void Fatal(string message, params object[] prms);
		void Fatal(Exception x);
		void Notification(string message);
		void Notification(string message, params object[] prms);
		System.IO.TextWriter Out { get; }
		void Watch(string title, System.Threading.ThreadStart method);
		void Dispose();
	}
}
