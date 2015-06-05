using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace LogR.Monitor.Configuration
{
	public class MonitorConfigurationSection : ConfigurationSection
	{
		[ConfigurationProperty("monitoredApplications", IsRequired = true)]
		public MonitoredApplicationConfigurationElementCollection MonitoredApplicationList
		{
			get
			{
				return (MonitoredApplicationConfigurationElementCollection)this["monitoredApplications"];
			}
			set
			{
				this["monitoredApplications"] = value;
			}
		}

		[ConfigurationProperty("clientId", IsRequired = true)]
		public string ClientId
		{
			get
			{
				return (string)this["clientId"];
			}
			set
			{
				this["clientId"] = value;
			}
		}

		[ConfigurationProperty("logDebugEnabled", IsRequired = true)]
		public bool LogDebugEnabled
		{
			get
			{
				return (bool)this["logDebugEnabled"];
			}
			set
			{
				this["logDebugEnabled"] = value;
			}
		}

		[ConfigurationProperty("logInfoEnabled", IsRequired = true)]
		public bool LogInfoEnabled
		{
			get
			{
				return (bool)this["logInfoEnabled"];
			}
			set
			{
				this["logInfoEnabled"] = value;
			}
		}

		[ConfigurationProperty("logWarningEnabled", IsRequired = true)]
		public bool LogWarningEnabled
		{
			get
			{
				return (bool)this["logWarningEnabled"];
			}
			set
			{
				this["logWarningEnabled"] = value;
			}
		}

		[ConfigurationProperty("logNotificationEnabled", IsRequired = true)]
		public bool LogNotificationEnabled
		{
			get
			{
				return (bool)this["logNotificationEnabled"];
			}
			set
			{
				this["logNotificationEnabled"] = value;
			}
		}

		[ConfigurationProperty("logErrorEnabled", IsRequired = true)]
		public bool LogErrorEnabled
		{
			get
			{
				return (bool)this["logErrorEnabled"];
			}
			set
			{
				this["logErrorEnabled"] = value;
			}
		}

		[ConfigurationProperty("logFatalEnabled", IsRequired = true)]
		public bool LogFatalEnabled
		{
			get
			{
				return (bool)this["logFatalEnabled"];
			}
			set
			{
				this["logFatalEnabled"] = value;
			}
		}
	}
}
