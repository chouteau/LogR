
namespace LogRPush;

public class LogInfo
{
	public LogInfo()
	{
		this.CreationDate = DateTime.Now;
		this.LogId = Guid.NewGuid().ToString();
	}

    public int RowNumber { get; set; }

    public string LogId { get; } = null!;

	public string Message { get; set; } = null!;
	public Category Category { get; set; }
	public DateTime CreationDate { get; set; }

	public string MachineName { get; set; } = null!;

	public string HostName { get; set; } = null!;

	public string Context { get; set; } = null!;

	public string ApplicationName { get; set; } = null!;

	public int ExceptionCount { get; set; } = 1;
	public string? ExceptionStack { get; set; }
	public string? StackChecksum { get; set; }
	public string EnvironmentName { get; set; } = null!;

	public Dictionary<string, string> ExtendedParameterList { get; set; } = new();
}

