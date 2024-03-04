using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace LogWinRMonitor.Models
{
	public class Settings
	{
		public List<MonitoredApplication> MonitoredApplicationList { get; set; } = new();

		[Bindable(true)]
		public bool LogTraceEnabled { get; set; } = true;
		[Bindable(true)]
		public bool LogDebugEnabled { get; set; } = true;
		[Bindable(true)]
		public bool LogInfoEnabled { get; set; } = true;
		[Bindable(true)]
		public bool LogWarningEnabled { get; set; } = true;
		[Bindable(true)]
		public bool LogErrorEnabled { get; set; } = true;
		[Bindable(true)]
		public bool LogFatalEnabled { get; set; } = true;
		[Bindable(true)]
		public bool LogSqlEnabled { get; set; } = true;
	}
}
