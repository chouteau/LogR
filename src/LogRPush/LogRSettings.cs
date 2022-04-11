
namespace LogRPush;

public class LogRSettings
{
    public LogRSettings()
    {
        LogLevel = LogLevel.Information;
        TimeoutInSecond = 10;
        LogServerUrlList = new List<string>();
    }
    public LogLevel LogLevel { get; set; }
    public string HostName { get; set; }
    public IList<string> LogServerUrlList { get; }
    public string ApiKey { get; set; }
    public int TimeoutInSecond { get; set; }
    public string ApplicationName { get; internal set; }
	public string EnvironmentName { get; set; }

}
