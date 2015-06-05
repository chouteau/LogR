using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace LogR.Monitor.Models
{
	public class MonitoredApplication
	{
		[Bindable(true)]
		public string ApiKey { get; set; }
		[Bindable(true)]
		public string SignalRUrl { get; set; }
		[Bindable(true)]
		public bool Enabled { get; set; }
	}
}
