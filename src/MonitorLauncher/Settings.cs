using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorLauncher
{
	public class Settings
	{
		public Settings()
		{
			MonitorUpdateUrl = "https://github.com/chouteau/LogR/releases/download/Latest/LogRMon.zip";
		}
		public string MonitorUpdateUrl { get; set; }
	}
}
