
namespace LogRPush;

public class LogRSettings
{
    public string HostName { get; set; } = null!;
    public List<string> LogServerUrlList { get; } = new();
    public string ApiKey { get; set; } = null!;
    public int TimeoutInSecond { get; set; } = 5;
    public int LogQueueLimit { get; set; } = 100;
	public string ApplicationName { get; internal set; } = null!;
    public string EnvironmentName { get; set; } = null!;
    public string EndPoint { get; set; } = "/api/logging/writelog";
    internal LogLevel LogLevel { get; set; } = LogLevel.None;
}
