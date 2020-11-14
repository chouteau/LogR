using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Internal;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.AspNetCore.Routing;

namespace LogRCore
{
	public static class MiddleWareExtensions
	{
		public static IServiceCollection ConfigureLogR(this IServiceCollection services, Action<LogRConfiguration> configuration)
		{
			var c = new LogRConfiguration();
			configuration.Invoke(c);
			services.ConfigureLogR(c);
			return services;
		}

		public static IServiceCollection ConfigureLogR(this IServiceCollection services, LogRConfiguration configuration)
		{
			services.AddSingleton<LogRConfiguration>(configuration);
			services.AddTransient<LogRFactory>();
			return services;
		}

		public static IApplicationBuilder UseLogR(this IApplicationBuilder applicationBuilder, ILoggerFactory loggerFactory)
		{
			var config = applicationBuilder.ApplicationServices.GetService<LogRConfiguration>();
			applicationBuilder.UseEndpoints(configure =>
			{
				configure.MapHub<LogRCore.LogRHub>(config.EndPoint, options =>
				{
					options.Transports = Microsoft.AspNetCore.Http.Connections.HttpTransportType.WebSockets
										| Microsoft.AspNetCore.Http.Connections.HttpTransportType.LongPolling;
				});
			});

			loggerFactory.AddLogRProvider(applicationBuilder.ApplicationServices);

			return applicationBuilder;
		}

		public static ILoggerFactory AddLogRProvider(this ILoggerFactory loggerFactory, IServiceProvider serviceProvider)
		{
			loggerFactory.AddProvider(new LogRProvider(serviceProvider));
			return loggerFactory;
		}
	}
}
