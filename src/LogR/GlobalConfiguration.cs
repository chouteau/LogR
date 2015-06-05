using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogR
{
	public class GlobalConfiguration
	{
		private static object m_Lock = new object();
		private static LogRConfiguration m_Configuration;

		public static LogRConfiguration Configuration
		{
			get
			{
				if (m_Configuration != null)
				{
					return m_Configuration;
				}
				lock (m_Lock)
				{
					if (m_Configuration == null)
					{
						m_Configuration = new LogRConfiguration();

						var section = (System.Web.Configuration.CompilationSection)System.Configuration.ConfigurationManager.GetSection("system.web/compilation");
						m_Configuration.DebugEnabled = section.Debug;
					}
				}
				return m_Configuration;
			}
		}

	}
}
