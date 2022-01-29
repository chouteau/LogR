
namespace LogRWebMonitor;

public class InnerProvider : ILoggerProvider
{
    public InnerProvider(LogCollector collector, IServiceProvider serviceProvider)
    {
        this.Collector = collector;
        this.ServiceProvider = serviceProvider;
    }

    protected LogCollector Collector { get; }
    protected IServiceProvider ServiceProvider { get; }

    public ILogger CreateLogger(string categoryName)
    {
        var settings = ServiceProvider.GetRequiredService<LogRSettings>();
        return new InnerLogger(settings, categoryName, Collector);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}

