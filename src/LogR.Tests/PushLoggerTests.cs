using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LogR.Tests
{
	[TestClass]
	public class PushLoggerTests
	{
		public PushLoggerTests()
		{
			this.Logger = new LogR.PushLogger();
		}

		protected LogR.ILogger Logger { get; private set; }

		[TestMethod]
		public void Debug()
		{
			Logger.Debug("test");
		}
	}
}
