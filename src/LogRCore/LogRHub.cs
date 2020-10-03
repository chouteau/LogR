using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LogRCore
{
	public class LogRHub : Hub
	{
		public async Task WriteLog(LogInfo logInfo)
		{
			await Clients.All.SendAsync("info", logInfo);
		}
	}
}
