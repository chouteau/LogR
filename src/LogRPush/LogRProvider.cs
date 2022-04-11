
namespace LogRPush;

public class LogRProvider : ILoggerProvider
{
    public LogRProvider(IServiceProvider serviceProvider,
		IHttpClientFactory httpClientFactory)
    {
        this.ServiceProvider = serviceProvider;
        this.HttpClientFactory = httpClientFactory;
    }

    protected IServiceProvider ServiceProvider { get; }
    protected IHttpClientFactory HttpClientFactory { get; }

    public ILogger CreateLogger(string categoryName)
    {
        var settings = ServiceProvider.GetRequiredService<LogRSettings>();
        return new LogRLogger(settings, categoryName, HttpClientFactory);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}

