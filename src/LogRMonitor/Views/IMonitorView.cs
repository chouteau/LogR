using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace LogRMonitor.Views
{
	public interface IMonitorView
	{
		void Show();
		event Action StartConfiguration;
		void AddLog(ViewModels.LogViewModel log);
		void Notify(string message);
	}
}
