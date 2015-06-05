using System;
using System.Runtime.Serialization;

namespace LogR.Monitor.Models
{
	public class Log
	{
		public Log()
		{
			this.CreationDate = DateTime.Now;
			this.LogId = Guid.NewGuid().ToString();
		}

		public string LogId { get; set;	}
		public string Message { get; set; }
		public Category Category { get; set; }
		public DateTime CreationDate { get; set; }
		public string ExceptionStack { get; set; }
		public int? ExceptionId { get; set; }
		public string MachineName { get; set; }
		public string HostName { get; set; }
		public string Context { get; set; }
		public string ApplicationName { get; set; }
	}
}
