using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace LogR
{
	public enum Category
	{
		Debug,
		Sql,
		Info,
		Warn,
		Error,
		Fatal,
		Notification,
	}

	[DataContract]
	public class LogInfo : IDisposable
	{
		public static readonly object m_Lock = new object();

		public LogInfo()
		{
			this.CreationDate = DateTime.Now;
			this.LogId = Guid.NewGuid().ToString();
		}

		[DataMember]
		public string LogId { get; set; }
		[IgnoreDataMember]
		public Exception Exception { get; set; }
		[DataMember]
		public string Message { get; set; }
		[DataMember]
		public Category Category { get; set; }
		[DataMember]
		public DateTime CreationDate { get; set; }
		[DataMember]
		public string ExceptionStack 
		{
			get
			{
				if (Exception == null)
				{
					return null;
				}

				var stack = string.Empty;
				try
				{
					stack = GetExceptionContent(Exception, 0);
				}
				catch
				{
					stack = Exception.ToString();
				}
				return stack;
			}
			set
			{
			}
		}

		[DataMember]
		public int? ExceptionId
		{
			get
			{
				if (Exception == null)
				{
					return null;
				}

				return ExceptionStack.GetHashCode();
			}
			set
			{
			}
		}

		[DataMember]
		public string MachineName { get; set; }

		[DataMember]
		public string HostName { get; set; }

		[DataMember]
		public string Context { get; set; }

		[DataMember]
		public string ApplicationName { get; set; }

		public static string GetExceptionContent(Exception ex, int level)
		{
			var content = new StringBuilder();
			content.Append("--------------------------------------------");
			content.AppendLine();
			content.AppendLine(ex.Message);
			content.AppendLine("--------------------------------------------");

			var sqlEx = ex as System.Data.SqlClient.SqlException;
			var key = string.Format("SqlDataExecption:{0}", level);
			if (sqlEx != null
				&& !ex.Data.Contains(key))
			{
				ex.Data.Add(key, "--------------------");
				ex.Data.Add(string.Format("SqlErrorCode:{0}", level), sqlEx.ErrorCode);
				ex.Data.Add(string.Format("LineNumber:{0}", level), sqlEx.LineNumber);
				ex.Data.Add(string.Format("Number:{0}", level), sqlEx.Number);
				ex.Data.Add(string.Format("Procedure:{0}", level), sqlEx.Procedure);
				ex.Data.Add(string.Format("Server:{0}", level), sqlEx.Server);
			}

			lock (m_Lock)
			{
				// Ajout des extensions d'erreur
				if (ex.Data != null
					&& ex.Data.Count > 0)
				{
					foreach (var item in ex.Data.Keys)
					{
						if (item != null && ex.Data != null && ex.Data[item] != null)
						{
							string data = string.Empty;
							try
							{
								data = ex.Data[item].ToString();
								content.AppendFormat("{0} = {1}", item, data);
							}
							catch { }
						}
						content.AppendLine();
					}
				}
			}

			content.Append(ex.StackTrace);
			content.AppendLine();
			if (ex.InnerException != null)
			{
				content.Append("--------------------------------------------");
				content.AppendLine();
				content.Append("Inner Exception");
				content.AppendLine();
				content.Append(GetExceptionContent(ex.InnerException, level++));
			}
			return content.ToString();
		}


		public void Dispose()
		{
			Message = null;	
		}
	}
}
