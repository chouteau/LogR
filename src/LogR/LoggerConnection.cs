using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.AspNet.SignalR;

namespace LogR
{
	public class LoggerConnection : PersistentConnection
	{
		internal static void PushMessage(string message)
		{
			var cnx = Microsoft.AspNet.SignalR.GlobalHost.ConnectionManager.GetConnectionContext<LoggerConnection>();
			if (cnx != null)
			{
				cnx.Connection.Broadcast(message);
			}
		}
	}
}
