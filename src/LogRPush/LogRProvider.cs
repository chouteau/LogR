
using Microsoft.Extensions.Configuration;

namespace LogRPush;

public sealed class LogRProvider : ILoggerProvider
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly LogRSettings _settings;
    private readonly ILogRExtender? _extender;
    private readonly LogLevel _minLogLevel;

    public LogRProvider(
        IHttpClientFactory httpClientFactory,
        LogRSettings settings,
        ILogRExtender? extender,
        IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        _settings = settings;
        _extender = extender;
        _minLogLevel = configuration.GetValue<LogLevel>("Logging:LogLevel:Default");
    }

    public ILogger CreateLogger(string categoryName)
    {
        return new LogRLogger(_settings, categoryName, _httpClientFactory, _extender, _minLogLevel);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}

