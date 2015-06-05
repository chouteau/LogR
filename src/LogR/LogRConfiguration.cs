using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogR
{
	public class LogRConfiguration
	{
		public LogRConfiguration()
		{
			DebugEnabled = true;
		}

		public bool DebugEnabled { get; set; }
		public string HostName { get; set; }
		public string ApplicationName { get; set; }
		public string Context { get; set; }
		public string FromEmail { get; set; }
		public string ToEmail { get; set; }
	}
}
