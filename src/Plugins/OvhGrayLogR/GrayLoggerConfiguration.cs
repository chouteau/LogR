using Microsoft.Extensions.Logging;

using Serilog.Events;

namespace OvhGrayLogR
{
    public class GrayLoggerConfiguration
    {
        public string OvhKey { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public LogLevel MinimumLogEventLevel { get; set; } = LogLevel.Information;
        public List<string> AllowedNamespacesStartsWith { get; set; } = new List<string>();
		public string PrefixName { get; set; }
	}
}
