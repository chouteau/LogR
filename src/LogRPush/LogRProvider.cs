
namespace LogRPush;

public class LogRProvider : ILoggerProvider
{
    public LogRProvider(
        IHttpClientFactory httpClientFactory,
        LogRSettings settings,
        ILogRExtender extender)
    {
		this.HttpClientFactory = httpClientFactory;
		this.Settings = settings;
		this.Extender = extender;
	}

    protected IHttpClientFactory HttpClientFactory { get; }
    protected LogRSettings Settings { get; }
    protected ILogRExtender Extender { get; }

    public ILogger CreateLogger(string categoryName)
    {
        return new LogRLogger(Settings, categoryName, HttpClientFactory, Extender);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}

