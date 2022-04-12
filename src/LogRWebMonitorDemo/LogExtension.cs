namespace LogRWebMonitorDemo;

public class LogExtension : LogRWebMonitor.ILogRExtender
{
	private int _counter;

	public LogExtension(IHttpContextAccessor httpContextAccessor)
	{
		this.HttpContextAccessor = httpContextAccessor;
	}

	protected IHttpContextAccessor HttpContextAccessor { get; }

	public Dictionary<string, string> GetParameters()
	{
		var result = new Dictionary<string, string>();
		result.Add("Url", $"{HttpContextAccessor?.HttpContext?.Request?.Path}");
		result.Add("Demo", $"counter{_counter++}");
		return result;
	}
}
