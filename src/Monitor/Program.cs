using System;
using System.Windows.Forms;

namespace LogR.Monitor
{
	internal static class Program
	{
		private static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			var app = new MonitorApplication();
			app.Run();
		}
	}
}
