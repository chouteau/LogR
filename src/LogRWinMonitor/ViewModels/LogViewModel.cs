using System;
using System.ComponentModel;

namespace LogWinRMonitor.ViewModels
{
	public class LogViewModel : ViewModelBase<LogRPush.LogInfo>
	{
		public LogViewModel()
			: base(new LogRPush.LogInfo())
		{
		}
		public LogViewModel(LogRPush.LogInfo model)
			: base(model)
		{
		}

		[Bindable(true)]
		public string LogId
		{
			get
			{
				return base.Model.LogId;
			}
		}

		[Bindable(true)]
		public DateTime CreationDate
		{
			get
			{
				return base.Model.CreationDate;
			}
			set
			{
				this.SetPropertyValue<DateTime>(() => this.CreationDate, value, delegate
				{
					this.Model.CreationDate = value;
				});
			}
		}
		[Bindable(true)]
		public string Message
		{
			get
			{
				return base.Model.Message;
			}
			set
			{
				this.SetPropertyValue<string>(() => this.Message, value, delegate
				{
					this.Model.Message = value;
				});
			}
		}
		[Bindable(true)]
		public LogRPush.Category Category
		{
			get
			{
				return base.Model.Category;
			}
			set
			{
				this.SetPropertyValue<LogRPush.Category>(() => this.Category, value, delegate
				{
					this.Model.Category = value;
				});
			}
		}
		[Bindable(true)]
		public string ExceptionStack
		{
			get
			{
				return base.Model.ExceptionStack;
			}
			set
			{
				this.SetPropertyValue<string>(() => this.ExceptionStack, value, delegate
				{
					this.Model.ExceptionStack = value;
				});
			}
		}

		[Bindable(true)]
		public string MachineName
		{
			get
			{
				return base.Model.MachineName;
			}
		}

		[Bindable(true)]
		public string HostName
		{
			get
			{
				return base.Model.HostName;
			}
		}

		[Bindable(true)]
		public string ApplicationName
		{
			get
			{
				return base.Model.ApplicationName;
			}
		}

		[Bindable(true)]
		public string Context
		{
			get
			{
				return base.Model.Context;
			}
		}
	}
}
