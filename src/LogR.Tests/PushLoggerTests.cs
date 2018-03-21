using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LogR.Tests
{
	[TestClass]
	public class PushLoggerTests
	{
		public PushLoggerTests()
		{
			Logger = new LogR.PushLogger();
			GlobalConfiguration.Configuration.DisableAsync = true;
			GlobalConfiguration.Configuration.FromEmail = "test@email.com";
			GlobalConfiguration.Configuration.ToEmail = "log@email.com";
			GlobalConfiguration.Configuration.ApplicationName = "TEST";
		}

		public static LogR.ILogger Logger { get; private set; }

		[ClassCleanup]
		public static void Cleanup()
		{
			Logger.Dispose();
		}

		[TestMethod]
		public void Debug()
		{
			Logger.Debug("test");
		}

		[TestMethod]
		public void Fatal()
		{
			Logger.Fatal(new Exception("fatal error"));
		}
	}
}
