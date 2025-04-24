using System.Reflection;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace LogRWebMonitor;

public static class StartupExtensions
{
	public static WebApplicationBuilder AddLogRWebMonitor(this WebApplicationBuilder builder, Action<LogRSettings>? config = null)
	{
		var settings = new LogRSettings();
		config?.Invoke(settings);

		var entryAssembly = Assembly.GetEntryAssembly();
		var version = entryAssembly?.GetName()?.Version?.ToString() ?? "0.0.0.0";
		var productAttribute = entryAssembly?.GetCustomAttribute<System.Reflection.AssemblyProductAttribute>();
		settings.ApplicationName = $"{productAttribute?.Product} ({version})";

		builder.Services.AddSingleton(settings);
		builder.Services.AddSingleton<LogCollector>();
		builder.Services.AddMemoryCache();

		return builder;
	}

	public static void UseLogRWebMonitor(this WebApplication app, Action<LogRPush.LogInfo>? addLog = null)
	{
		var loggerFactory = app.Services.GetRequiredService<ILoggerFactory>();
		var logCollector = app.Services.GetRequiredService<LogCollector>();
		var scope = app.Services.CreateScope();
		var extender = scope.ServiceProvider.GetService<ILogRExtender>();
		var settings = scope.ServiceProvider.GetRequiredService<LogRSettings>();
		if (settings.FailDirectory.StartsWith(@".\"))
		{
			var current = System.IO.Path.GetDirectoryName(typeof(StartupExtensions).Assembly.Location)!;
			settings.FailDirectory = System.IO.Path.Combine(current, settings.FailDirectory, "Logs");
		}

		if (!System.IO.Directory.Exists(settings.FailDirectory))
		{
			System.IO.Directory.CreateDirectory(settings.FailDirectory);
		}

		if (addLog != null)
		{
			logCollector.OnAddLog += addLog;
		}

		loggerFactory.AddProvider(new InnerProvider(logCollector, app.Services, extender));

		app.MapPost(settings.EndPoint, (LogRPush.LogInfo log, [FromServices] LogCollector collector) =>
		{
			foreach (var keyword in settings.KeywordMessageFilters)
			{
				if (log.Message is not null
				&& log.Message.Contains(keyword, StringComparison.InvariantCultureIgnoreCase))
				{
					return Microsoft.AspNetCore.Http.Results.Ok();
				}
				if (log.ExceptionStack is not null
					&& log.ExceptionStack.Contains(keyword, StringComparison.InvariantCultureIgnoreCase))
				{
					return Microsoft.AspNetCore.Http.Results.Ok();
				}
			}
			try
			{
				collector.AddLog(log);
			}
			catch (Exception ex)
			{
				try
				{
					var fileName = $"Logs{DateTime.Now:yyyyMMddHH}.txt";
					fileName = System.IO.Path.Combine(settings.FailDirectory, fileName);
					System.IO.File.AppendAllText(fileName, $"{ex.Message}\r\n{ex.StackTrace}\r\n");
				}
				catch { /* dead for science */ }
			}
			return Microsoft.AspNetCore.Http.Results.Ok();
		});
		var hubEndpoint = $"/logrhub/{settings.HubKeyEndPoint}".TrimEnd('/');
		app.MapHub<LogRHub>(hubEndpoint);
	}
}