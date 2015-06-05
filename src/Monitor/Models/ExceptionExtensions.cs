using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogR.Monitor.Models
{
	public static class ExceptionExtensions
	{
		public static List<string> ToFlatMessage(this AggregateException exception)
		{
			var result = new List<string>();
			result.Add(exception.Message);

			foreach (var item in exception.InnerExceptions)
			{
				result.AddRange(item.GetMessage());
			}
			// result.Add(exception.ToString());
			return result;
		}

		public static List<string> GetMessage(this Exception exeption)
		{
			if (exeption == null)
			{
				return new List<string>();
			}
			var result = new List<string>();
			result.Add(exeption.Message);
			result.AddRange(exeption.InnerException.GetMessage());
			return result;
		}
	}
}
