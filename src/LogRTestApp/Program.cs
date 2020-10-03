using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using System;
using Microsoft.Extensions.DependencyInjection;
using LogRCore;
using Microsoft.Extensions.Logging;

namespace LogRTestApp
{
	class Program
	{
		static void Main(string[] args)
		{
			var services = new ServiceCollection();

			var remoteConfig = new LogRCore.LogRConfiguration();
			remoteConfig.LogLevel = LogLevel.Debug;

			services.ConfigureLogR(remoteConfig);

			var sp = services.BuildServiceProvider();

			var loggerFactory = sp.GetRequiredService<ILoggerFactory>();
			loggerFactory.AddLogRProvider(sp);

			var logger = sp.GetRequiredService<ILogger<Program>>();

			logger.LogInformation("test");

			Console.Read();

		}
	}
}
