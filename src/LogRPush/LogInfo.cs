using System;
using System.Collections.Generic;

namespace LogRPush;

public class LogInfo
{
    public int RowNumber { get; set; }

    public string LogId { get; } = $"{Guid.NewGuid()}";

	public string Message { get; set; } = null!;
	public Microsoft.Extensions.Logging.LogLevel  LogLevel { get; set; }
	public DateTime CreationDate { get; set; } = DateTime.Now;

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

