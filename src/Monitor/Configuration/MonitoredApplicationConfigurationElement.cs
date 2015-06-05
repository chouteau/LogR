using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace LogR.Monitor.Configuration
{
	public class MonitoredApplicationConfigurationElement : ConfigurationElement
	{
		[ConfigurationProperty("apiKey", IsRequired = true)]
		public string ApiKey
		{
			get
			{
				return (string)this["apiKey"];
			}
			set
			{
				this["apiKey"] = value;
			}
		}

		[ConfigurationProperty("signalRUrl", IsRequired = false)]
		public string SignalRUrl
		{
			get
			{
				return (string)this["signalRUrl"];
			}
			set
			{
				this["signalRUrl"] = value;
			}
		}

		[ConfigurationProperty("enabled", IsRequired = false, DefaultValue = true)]
		public bool Enabled
		{
			get
			{
				return (bool)this["enabled"];
			}
			set
			{
				this["enabled"] = value;
			}
		}
	}
}
