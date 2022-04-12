using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace LogRWebMonitor
{
    public static class StartupExtensions
    {
        public static WebApplicationBuilder AddLogRWebMonitor(this WebApplicationBuilder builder, Action<LogRSettings> config = null)
        {
            var settings = new LogRSettings();
            config?.Invoke(settings);

            var entryAssembly = Assembly.GetEntryAssembly();
            var version = entryAssembly?.GetName()?.Version?.ToString() ?? "0.0.0.0";
            var productAttribute = entryAssembly?.GetCustomAttribute<System.Reflection.AssemblyProductAttribute>();
            settings.ApplicationName = $"{productAttribute?.Product} ({version})";

            builder.Services.AddSingleton(settings);
            builder.Services.AddSingleton<LogCollector>();

            return builder;   
        }

        public static void UseLogRWebMonitor(this WebApplication app, Action<LogRPush.LogInfo> addLog = null)
        {
            var loggerFactory = app.Services.GetRequiredService<ILoggerFactory>();
            var logCollector = app.Services.GetRequiredService<LogCollector>();
            var scope = app.Services.CreateScope();
            var extender = scope.ServiceProvider.GetService<ILogRExtender>();
			
            if (addLog != null)
            {
                logCollector.OnAddLog += addLog;
            }
            
            loggerFactory.AddProvider(new InnerProvider(logCollector, app.Services, extender));

            app.MapPost("/api/logging/writelog", (LogRPush.LogInfo log, [FromServices] LogCollector collector) =>
            {
                collector.AddLog(log);
                return Microsoft.AspNetCore.Http.Results.Ok();
            });
        }
    }
}