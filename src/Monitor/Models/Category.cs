using System;

namespace LogR.Monitor.Models
{
	// [Flags]
	public enum Category
	{
		Debug,
		Sql,
		Info,
		Warn,
		Error,
		Fatal,
		Notification
	}
}
