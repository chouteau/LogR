using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace LogR.Monitor.Services
{
	public class SettingsService
	{
		string sectionName = "monitor";

		public Models.Settings GetSettings()
		{
			var result = new Models.Settings();

			Configuration.MonitorConfigurationSection section = null;
			var configFileName = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(this.GetType().Assembly.Location), "monitor.config");
			if (System.IO.File.Exists(configFileName))
			{
				try
				{
					var map = new ExeConfigurationFileMap();
					map.ExeConfigFilename = configFileName;
					var config = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);
					section = config.GetSection(sectionName) as Configuration.MonitorConfigurationSection;
				}
				catch
				{
					System.IO.File.Delete(configFileName);
				}
			}
			if (section == null)
			{
				section = new Configuration.MonitorConfigurationSection();
			}

			foreach (Configuration.MonitoredApplicationConfigurationElement item in section.MonitoredApplicationList)
			{
				result.MonitoredApplicationList.Add(new Models.MonitoredApplication()
				{
					ApiKey = item.ApiKey,
					SignalRUrl = item.SignalRUrl,
					Enabled = item.Enabled,
				});
			}

			result.LogDebugEnabled = section.LogDebugEnabled;
			result.LogInfoEnabled = section.LogInfoEnabled;
			result.LogWarningEnabled = section.LogWarningEnabled;
			result.LogNotificationEnabled = section.LogNotificationEnabled;
			result.LogErrorEnabled = section.LogErrorEnabled;
			result.LogFatalEnabled = section.LogFatalEnabled;

			return result;
		}

		public void SaveSettings(Models.Settings settings)
		{
			var configFileName = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(this.GetType().Assembly.Location), "monitor.config");
			var fileMap = new ExeConfigurationFileMap
			{
				ExeConfigFilename = configFileName
			};

			var config = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
			var section = config.GetSection(sectionName) as Configuration.MonitorConfigurationSection;
			if (section == null)
			{
				section = new Configuration.MonitorConfigurationSection(); 
				config.Sections.Add(sectionName, section);
			}

			section.ClientId = settings.ClientId;
			section.LogDebugEnabled = settings.LogDebugEnabled;
			section.LogErrorEnabled = settings.LogErrorEnabled;
			section.LogFatalEnabled = settings.LogFatalEnabled;
			section.LogInfoEnabled = settings.LogInfoEnabled;
			section.LogNotificationEnabled = settings.LogNotificationEnabled;
			section.LogWarningEnabled = settings.LogWarningEnabled;

			section.MonitoredApplicationList = new Configuration.MonitoredApplicationConfigurationElementCollection();
			foreach (var item in settings.MonitoredApplicationList)
			{
				if (item.ApiKey == null)
				{
					continue;
				}
				var element = new Configuration.MonitoredApplicationConfigurationElement()
				{
					ApiKey = item.ApiKey,
					SignalRUrl = item.SignalRUrl,
					Enabled = item.Enabled,
				};
				section.MonitoredApplicationList.Add(element);
			}

			config.Save(ConfigurationSaveMode.Minimal);
		}


	}
}
