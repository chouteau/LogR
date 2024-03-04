using System;
using System.Collections.Generic;

using LogRPush;

using Microsoft.Extensions.Logging;

namespace LogWinRMonitor.Models;

public class CheckedLevel
{
	public string Name => $"{Value}";
	public bool Checked { get; set; } = true;
	public LogLevel Value { get; set; }
}

public class LogFilter
{
	public LogFilter()
	{
		LevelList = new()
		{
			new CheckedLevel { Value = LogLevel.Trace },
			new CheckedLevel { Value = LogLevel.Debug },
			new CheckedLevel { Value = LogLevel.Information },
			new CheckedLevel { Value = LogLevel.Warning },
			new CheckedLevel { Value = LogLevel.Error },
			new CheckedLevel { Value = LogLevel.Critical },
		};
	}
	public Guid Id { get; set; } = Guid.NewGuid();
	public List<CheckedLevel> LevelList { get; set; }
	public string? Search { get; set; }

    public bool AllMachine { get; set; } = true;
    public List<string> MachineNameList { get; set; } = new();

	public bool AllHost { get; set; } = true;
    public List<string> HostNameList { get; set; } = new();

    public bool AllContext { get; set; } = true;
	public List<string> ContextList { get; set; } = new();

    public bool AllApplication  { get; set; } = true;
	public List<string> ApplicationNameList { get; set; } = new();

	public int Top { get; set; } = 500;
}
