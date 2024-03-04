using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogWinRMonitor.Services
{
	public interface IHandler : IDisposable
	{
		event Action<string> Received;
		event Action<Exception> Exception;
		event Action Disconnected;
		void Start(Models.MonitoredApplication settings, string clientName);
		void Stop();
	}
}
