using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogRCore
{
	public class LogRConfiguration
	{
		public LogRConfiguration()
		{
			MachineName = System.Environment.MachineName;
			LogLevel = LogLevel.Information;
			EndPoint = "/logger";
		}
		public string ApplicationName { get; set; }
		public string HostName { get; set; }
		public string MachineName { get; set; }
		public LogLevel	LogLevel { get; set; }
		public int EventId { get; set; }
		public string EndPoint { get; set; }
	}
}
