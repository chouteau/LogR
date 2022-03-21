using LogRPush;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogRWebMonitor
{
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
			LevelList = new();
			LevelList.Add(new CheckedLevel { Value = Category.Trace });
			LevelList.Add(new CheckedLevel { Value = Category.Debug });
			LevelList.Add(new CheckedLevel { Value = Category.Info });
			LevelList.Add(new CheckedLevel { Value = Category.Warn });
			LevelList.Add(new CheckedLevel { Value = Category.Error });
			LevelList.Add(new CheckedLevel { Value = Category.Fatal });
			LevelList.Add(new CheckedLevel { Value = Category.Notification });
		}
		public Guid Id { get; set; } = Guid.NewGuid();
		public string Search { get; set; }
		public List<CheckedLevel> LevelList { get; set; }
		public string MachineName { get; set; }
		public string HostName { get; set; }
		public string Context { get; set; }
		public string ApplicationName { get; set; }
		public int Top { get; set; } = 500;
	}
}
