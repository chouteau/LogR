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
        public string ApplicationName { get; internal set; } = null!;
        public string HostName { get; set; } = null!;
		public string EnvironmentName { get; set; } = null!;
        public string EndPoint { get; set; } = "/api/logging/writelog";
		public string FailDirectory { get; set; } = @".\";
        public List<string> KeywordMessageFilters { get; set; } = new();
        public string HubKeyEndPoint { get; set; } = null!;
    }
}
