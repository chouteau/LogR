
namespace LogRPush;

public class LogRProvider : ILoggerProvider
{
    public LogRProvider(IServiceProvider serviceProvider)
    {
        this.ServiceProvider = serviceProvider;
    }

    protected IServiceProvider ServiceProvider { get; }

    public ILogger CreateLogger(string categoryName)
    {
        var settings = ServiceProvider.GetRequiredService<LogRSettings>();
        var httpClientFactory = ServiceProvider.GetRequiredService<IHttpClientFactory>();
        var extender = ServiceProvider.GetService<ILogRExtender>();
        return new LogRLogger(settings, categoryName, httpClientFactory, extender);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}

