using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogRCore
{
	public class LogRProvider : ILoggerProvider
	{
		public LogRProvider(IServiceProvider serviceProvider)
		{
			this.ServiceProvider = serviceProvider;
		}

		protected IServiceProvider ServiceProvider { get; }

		public ILogger CreateLogger(string categoryName)
		{
			var factory = ServiceProvider.GetService<LogRFactory>();
			return factory.CreateLogger(categoryName);
		}

		public void Dispose()
		{
			
		}
	}
}
