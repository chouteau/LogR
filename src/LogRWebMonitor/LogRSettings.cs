using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogRWebMonitor
{
    public class LogRSettings
    {
        public int LogCountMax { get; set; } = 20000;
        public LogLevel LogLevel { get; set; } = LogLevel.Information;
        public string ApplicationName { get; internal set; }
        public string HostName { get; set; }
		public string EnvironmentName { get; set; }
    }
}
