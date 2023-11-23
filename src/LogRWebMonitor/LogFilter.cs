using LogRPush;

namespace LogRWebMonitor;

public class CheckedLevel
{
	public string Name => $"{Value}";
	public bool Checked { get; set; } = true;
	public Category Value { get; set; }
}

public class LogFilter
{
	public LogFilter()
	{
		LevelList = new()
		{
			new CheckedLevel { Value = Category.Trace },
			new CheckedLevel { Value = Category.Debug },
			new CheckedLevel { Value = Category.Info },
			new CheckedLevel { Value = Category.Warn },
			new CheckedLevel { Value = Category.Error },
			new CheckedLevel { Value = Category.Fatal },
			new CheckedLevel { Value = Category.Notification }
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
