using System;

namespace LogRMonitor.Models
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
		Notification,
		Internal
	}
}
