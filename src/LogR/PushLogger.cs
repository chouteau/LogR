﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace LogR
{
	public class PushLogger : ILogger
	{

		public PushLogger()
		{
		}

		public void Debug(string message)
		{
			if (!GlobalConfiguration.Configuration.DebugEnabled)
			{
				return;
			}

			AsyncLogger.Current.LogMessage(new LogInfo()
			{
				Category = Category.Debug,
				Message = message,
			});
		}

		public void Sql(string message)
		{
			if (!GlobalConfiguration.Configuration.DebugEnabled)
			{
				return;
			}

			AsyncLogger.Current.LogMessage(new LogInfo()
			{
				Category = Category.Sql,
				Message = message,
			});
		}

		public void Debug(string message, params object[] prms)
		{
			if (!GlobalConfiguration.Configuration.DebugEnabled)
			{
				return;
			}

			if (prms != null && prms.Length > 0)
			{
				message = string.Format(message, prms);
			}

			AsyncLogger.Current.LogMessage(new LogInfo()
			{
				Category = Category.Debug,
				Message = message
			});
		}

		public void Info(string message)
		{
			AsyncLogger.Current.LogMessage(new LogInfo()
			{
				Category = Category.Info,
				Message = message,
			});
		}

		public void Info(string message, params object[] prms)
		{
			if (prms != null && prms.Length > 0)
			{
				message = string.Format(message, prms);
			}

			AsyncLogger.Current.LogMessage(new LogInfo()
			{
				Category = Category.Info,
				Message = message,
			});
		}

		public void Warn(string message)
		{
			AsyncLogger.Current.LogMessage(new LogInfo()
			{
				Category = Category.Warn,
				Message = message,
			});
		}

		public void Warn(string message, params object[] prms)
		{
			if (prms != null && prms.Length > 0)
			{
				message = string.Format(message, prms);
			}

			AsyncLogger.Current.LogMessage(new LogInfo()
			{
				Category = Category.Warn,
				Message = message,
			});
		}

		public void Error(string message)
		{
			AsyncLogger.Current.LogMessage(new LogInfo()
			{
				Category = Category.Error,
				Message = message,
				Exception = new Exception(message),
				ExceptionStack = System.Environment.StackTrace,
			});
		}

		public void Error(string message, params object[] prms)
		{
			if (prms != null && prms.Length > 0)
			{
				message = string.Format(message, prms);
			}

			AsyncLogger.Current.LogMessage(new LogInfo()
			{
				Category = Category.Error,
				Message = message,
				Exception = new Exception(message),
				ExceptionStack = System.Environment.StackTrace,
			});
		}

		public void Error(Exception x)
		{
			AsyncLogger.Current.LogMessage(new LogInfo()
			{
				Category = Category.Error,
				Message = GetContent(x),
				Exception = x,
			});
		}

		public void Fatal(string message)
		{
			AsyncLogger.Current.LogMessage(new LogInfo()
			{
				Category = Category.Fatal,
				Message = message,
				Exception = new Exception(message),
				ExceptionStack = System.Environment.StackTrace,
			});
		}

		public void Fatal(string message, params object[] prms)
		{
			if (prms != null && prms.Length > 0)
			{
				message = string.Format(message, prms);
			}

			AsyncLogger.Current.LogMessage(new LogInfo()
			{
				Category = Category.Fatal,
				Message = message,
				Exception = new Exception(message),
				ExceptionStack = System.Environment.StackTrace,
			});
		}

		public void Fatal(Exception x)
		{
			AsyncLogger.Current.LogMessage(new LogInfo()
			{
				Category = Category.Fatal,
				Message = GetContent(x),
				Exception = x,
			});
		}

		public void Notification(string message)
		{
			AsyncLogger.Current.LogMessage(new LogInfo()
			{
				Category = Category.Notification,
				Message = message,
			});
		}

		public void Notification(string message, params object[] prms)
		{
			if (prms != null && prms.Length > 0)
			{
				message = string.Format(message, prms);
			}
			AsyncLogger.Current.LogMessage(new LogInfo()
			{
				Category = Category.Notification,
				Message = message,
			});
		}

		public System.IO.TextWriter Out
		{
			get
			{
				return null;
			}
		}

		public void Watch(string title, Action method)
		{
			var watch = Stopwatch.StartNew();
			watch.Start();
			method.Invoke();
			watch.Stop();
			Debug("{0} = {1}ms", title, watch.ElapsedMilliseconds);
		}

		private string GetContent(System.Exception ex)
		{
			var loop = 0;
			var content = new StringBuilder();
			var x = ex;
			while (true)
			{
				content.AppendLine(x.Message);
				content.AppendLine(x.StackTrace);
				content.AppendLine();
				if (x.Data != null && x.Data.Count > 0)
				{
					content.AppendLine("--------------------------------------------");
					content.AppendLine();
					foreach (object item in x.Data.Keys)
					{
						if (item != null && x.Data != null && x.Data[item] != null)
						{
							content.AppendFormat("{0} = {1}", item, x.Data[item]);
						}
						content.AppendLine();
					}
					content.Append("--------------------------------------------");
				}

				content.AppendLine();

				if (x.InnerException == null)
				{
					break;
				}

				x = x.InnerException;
				loop++;
				if (loop > 10)
				{
					break;
				}
			}
			return content.ToString();
		}

		public void Dispose()
		{
			AsyncLogger.Current.Dispose();
		}
	}
}
