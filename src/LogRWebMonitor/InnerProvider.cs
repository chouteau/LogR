
namespace LogRWebMonitor;

public class InnerProvider : ILoggerProvider
{
    public InnerProvider(LogCollector collector, IServiceProvider serviceProvider, ILogRExtender? extender)
    {
        this.Collector = collector;
        this.ServiceProvider = serviceProvider;
        this.Extender = extender;
    }

    protected LogCollector Collector { get; }
    protected IServiceProvider ServiceProvider { get; }
    protected ILogRExtender? Extender { get; }

    public ILogger CreateLogger(string categoryName)
    {
        var settings = ServiceProvider.GetRequiredService<LogRSettings>();
        return new InnerLogger(settings, categoryName, Collector, Extender);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}

