using Microsoft.AspNetCore.SignalR;

namespace LogRWebMonitor;
public class LogRHub : Hub
{
	public async Task WriteLog(LogRPush.LogInfo logInfo)
	{
		await Clients.All.SendAsync("ReceiveLog", logInfo);
	}
}
