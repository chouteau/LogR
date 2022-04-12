using Microsoft.AspNetCore.Components;

namespace LogRWebMonitorDemo;

public class LogExtension : LogRWebMonitor.ILogRExtender
{
	private int _counter;

	public LogExtension()
	{
	}

	public string CurrentUri { get; set; }

	public Dictionary<string, string> GetParameters()
	{
		var _dic = new Dictionary<string, string>();
		_dic.Add("Uri", CurrentUri);
		_dic.Add("Demo", $"counter{_counter++}");
		return _dic;
	}
}
