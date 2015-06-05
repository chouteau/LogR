using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace LogR.Monitor.Views
{
	public interface IMonitorView
	{
		void Show();
		event Action StartConfiguration;
		ViewModels.BindingList<ViewModels.LogViewModel> LogList { get; set; }
		void AddLog(ViewModels.LogViewModel log);
		void Notify(string message);
	}
}
