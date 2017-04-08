using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogR
{
	public class GlobalConfiguration
	{
		private static Lazy<LogRConfiguration> m_LazyConfiguration = new Lazy<LogRConfiguration>(() =>
		{
			var config = new LogRConfiguration();

			var section = (System.Web.Configuration.CompilationSection)System.Configuration.ConfigurationManager.GetSection("system.web/compilation");
			config.DebugEnabled = section.Debug;

			Microsoft.AspNet.SignalR.GlobalHost.Configuration.DisconnectTimeout = TimeSpan.FromSeconds(60);

			return config;
		}, true);

		public static LogRConfiguration Configuration
		{
			get
			{
				return m_LazyConfiguration.Value;
			}
		}

	}
}
