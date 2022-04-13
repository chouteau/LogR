
namespace LogRPush;

public static class LogRExtensions
{
    public static IServiceCollection AddLogRPush(this IServiceCollection services, Action<LogRSettings> config)
    {
        var settings = new LogRSettings();
        config(settings);
        services.AddSingleton(settings);

        var entryAssembly = Assembly.GetEntryAssembly();
        var version = entryAssembly?.GetName()?.Version?.ToString() ?? "0.0.0.0";
        var productAttribute = entryAssembly?.GetCustomAttribute<System.Reflection.AssemblyProductAttribute>();
        settings.ApplicationName = $"{productAttribute?.Product} ({version})";

        services.AddHttpClient("LogRClient", httpClient =>
        {
            if (!string.IsNullOrWhiteSpace(settings.ApiKey))
			{
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Basic {settings.ApiKey}");
            }
            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd($"LogRPush ({System.Environment.OSVersion}; {System.Environment.MachineName}; {settings.HostName})");
        }).ConfigurePrimaryHttpMessageHandler(() =>
        {
            var handler = new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip,
                AllowAutoRedirect = false,
                UseCookies = false,
            };
            return handler;
        }).SetHandlerLifetime(TimeSpan.FromSeconds(settings.TimeoutInSecond));

        if (!settings.LogServerUrlList.Any())
        {
            throw new NotSupportedException("LogServerUrl does not be empty");
        }

        return services;
    }

    public static void UseLogRPush(this IServiceProvider serviceProvider)
    {
        var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
        var httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
        var settings = serviceProvider.GetRequiredService<LogRSettings>();
        var extender = serviceProvider.GetService<ILogRExtender>();
        var provider = new LogRProvider(httpClientFactory, settings, extender);
        loggerFactory.AddProvider(provider);
    }
}
