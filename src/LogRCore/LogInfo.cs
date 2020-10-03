using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace LogRCore
{
	public enum Category
	{
		Debug,
		Sql,
		Info,
		Warn,
		Error,
		Fatal,
		Notification,
	}

	public class LogInfo : IDisposable
	{
		public static readonly object m_Lock = new object();

		public LogInfo()
		{
			this.CreationDate = DateTime.Now;
			this.LogId = Guid.NewGuid().ToString();
		}

		public string LogId { get; }

		public string Message { get; set; }
		public Category Category { get; set; }
		public DateTime CreationDate { get; set; }

		public string MachineName { get; set; }

		public string HostName { get; set; }

		public string Context { get; set; }

		public string ApplicationName { get; set; }

		public string ExceptionStack { get; set; }

		public void Dispose()
		{
			Message = null;	
		}
	}
}
