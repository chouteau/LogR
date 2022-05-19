
namespace LogRPush;

public class LogRSettings
{
    public LogRSettings()
    {
        LogLevel = LogLevel.Information;
        TimeoutInSecond = 5;
		LogQueueLimit = 100;
		LogServerUrlList = new List<string>();
        EndPoint = "/api/logging/writelog";
    }
    public LogLevel LogLevel { get; set; }
    public string HostName { get; set; }
    public IList<string> LogServerUrlList { get; }
    public string ApiKey { get; set; }
    public int TimeoutInSecond { get; set; }
	public int LogQueueLimit { get; set; }
	public string ApplicationName { get; internal set; }
	public string EnvironmentName { get; set; }
    public string EndPoint { get; set; }

}
