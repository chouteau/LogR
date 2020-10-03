using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogRCore
{
	public class LogRFactory
	{
		public LogRFactory(IHubContext<LogRHub> hubContext,
			LogRConfiguration config)
		{
			this.HubContext = hubContext;
			this.Config = config;
		}

		protected IHubContext<LogRHub> HubContext { get; }
		protected LogRConfiguration Config { get; }


		public ILogger CreateLogger(string categoryName)
		{
			return new LogRLogger(Config, HubContext, categoryName);
		}


	}
}
