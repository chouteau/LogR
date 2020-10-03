using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace LogRMonitor.Services
{
	public class SettingsService
	{
		public Models.Settings GetSettings()
		{
			var result = new Models.Settings();
			var configFileName = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "monitor.config");
			if (System.IO.File.Exists(configFileName))
			{
				var content = System.IO.File.ReadAllText(configFileName);
				try
				{
					result = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.Settings>(content);
				}
				catch
				{
					System.IO.File.Copy(configFileName, configFileName + ".bak", false);
				}
			}

			return result;
		}

		public void SaveSettings(Models.Settings settings)
		{
			var configFileName = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "monitor.config");
			var content = Newtonsoft.Json.JsonConvert.SerializeObject(settings);
			var bakCount = 0;
			if (System.IO.File.Exists(configFileName))
			{
				while (true)
				{
					var backupFile = configFileName + ".bak";
					if (bakCount > 0)
					{
						backupFile = backupFile + bakCount.ToString();
					}
					if (System.IO.File.Exists(backupFile))
					{
						bakCount++;
						continue;
					}
					System.IO.File.Copy(configFileName, backupFile, false);
					break;
				}
				System.IO.File.Delete(configFileName);
			}
			System.IO.File.WriteAllText(configFileName, content);
		}


	}
}
