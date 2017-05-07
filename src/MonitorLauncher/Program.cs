using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorLauncher
{
	class Program
	{
		static void Main(string[] args)
		{
			var settingFile = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "settings.config");
			Settings settings = null;
			if (System.IO.File.Exists(settingFile))
			{
				var content = System.IO.File.ReadAllText(settingFile);
				if (content != null)
				{
					settings = Newtonsoft.Json.JsonConvert.DeserializeObject<Settings>(content);
				}
			}
			if (settings == null)
			{
				settings = new Settings();
			}

			var downloadService = new DownloaderService();
			downloadService.Initialize(settings);

			var hasUpdate = downloadService.HasUpdate();
			if (hasUpdate)
			{
				downloadService.DownloadLatest();
				downloadService.DeployLatest();
			}

			var mainFile = System.IO.Path.Combine(downloadService.LatestFolder, "LogRMon.exe");
			if (!System.IO.File.Exists(mainFile))
			{
				System.Windows.Forms.MessageBox.Show("LogRMon.exe does not exists in latest path");
				return;
			}
			var psi = new System.Diagnostics.ProcessStartInfo(mainFile);
			psi.CreateNoWindow = false;
			psi.UseShellExecute = false;
			psi.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;

			System.Diagnostics.Process.Start(psi);
		}
	}
}
