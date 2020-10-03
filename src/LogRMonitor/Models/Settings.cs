using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace LogRMonitor.Models
{
	public class Settings
	{
		public Settings()
		{
			MonitoredApplicationList = new List<MonitoredApplication>();
			LogDebugEnabled = false;
			LogSqlEnabled = false;
			LogInfoEnabled = true;
			LogFatalEnabled = true;
			LogNotificationEnabled = true;
			LogErrorEnabled = true;
			LogWarningEnabled = true;
		}

		public List<MonitoredApplication> MonitoredApplicationList { get; set; }

		[Bindable(true)]
		public bool LogDebugEnabled { get; set; }
		[Bindable(true)]
		public bool LogSqlEnabled { get; set; }
		[Bindable(true)]
		public bool LogInfoEnabled { get; set; }
		[Bindable(true)]
		public bool LogWarningEnabled { get; set; }
		[Bindable(true)]
		public bool LogNotificationEnabled { get; set; }
		[Bindable(true)]
		public bool LogErrorEnabled { get; set; }
		[Bindable(true)]
		public bool LogFatalEnabled { get; set; }
	}
}
