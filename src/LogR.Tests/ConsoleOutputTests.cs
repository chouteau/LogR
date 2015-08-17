using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogR.Tests
{
	[TestClass]
	public class ConsoleOutputTests
	{

		public ConsoleOutputTests()
		{
			this.Logger = new LogR.TestsLogger();
		}

		protected LogR.ILogger Logger { get; private set; }

		[TestMethod]
		public void Log()
		{
			Logger.Debug("test");
			Logger.Debug("test {0}", "with param");

			Logger.Info("test");
			Logger.Info("test {0}", "with param");

			Logger.Notification("test");
			Logger.Notification("test {0}", "with param");

			Logger.Warn("test");
			Logger.Warn("test {0}", "with param");

			Logger.Sql("test");

			Logger.Error("test");
			Logger.Error("test {0}", "with param");
			Logger.Error(new Exception("Test"));

			Logger.Fatal("test");
			Logger.Fatal("test {0}", "with param");
			Logger.Fatal(new Exception("Test"));
		}

		[TestMethod]
		public async Task LogAsync()
		{
			await Task.Run(() =>
			{
				Log();
			});
		}
	}
}