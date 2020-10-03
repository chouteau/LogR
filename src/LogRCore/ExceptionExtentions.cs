using System;
using System.Collections.Generic;
using System.Text;

namespace LogRCore
{
	public static class ExceptionExtentions
	{
		public static string GetExceptionContent(this Exception ex, int level = 0)
		{
			if (ex == null)
			{
				return null;
			}

			var content = new StringBuilder();
			content.Append("--------------------------------------------");
			content.AppendLine();
			content.AppendLine(ex.Message);
			content.AppendLine("--------------------------------------------");

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
	}
}
