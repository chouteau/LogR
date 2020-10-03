using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace LogRMonitor.ViewModels
{
	internal class PropertyComparer<TKey> : IComparer<TKey>
	{
		private PropertyDescriptor _property;
		private ListSortDirection _direction;
		public PropertyComparer(PropertyDescriptor property, ListSortDirection direction)
		{
			this._property = property;
			this._direction = direction;
		}
		public int Compare(TKey xVal, TKey yVal)
		{
			object xValue = this.GetPropertyValue(xVal, this._property);
			object yValue = this.GetPropertyValue(yVal, this._property);
			int result;
			if (this._direction == ListSortDirection.Ascending)
			{
				result = this.CompareAscending(xValue, yValue);
			}
			else
			{
				result = this.CompareDescending(xValue, yValue);
			}
			return result;
		}
		public bool Equals(TKey xVal, TKey yVal)
		{
			return xVal.Equals(yVal);
		}
		public int GetHashCode(TKey obj)
		{
			return obj.GetHashCode();
		}
		private int CompareAscending(object xValue, object yValue)
		{
			int result;
			if (xValue is IComparable)
			{
				result = ((IComparable)xValue).CompareTo(yValue);
			}
			else
			{
				if (yValue == null)
				{
					result = 0;
				}
				else
				{
					if (xValue == null)
					{
						result = 0;
					}
					else
					{
						if (xValue.Equals(yValue))
						{
							result = 0;
						}
						else
						{
							result = xValue.ToString().CompareTo(yValue.ToString());
						}
					}
				}
			}
			return result;
		}
		private int CompareDescending(object xValue, object yValue)
		{
			return this.CompareAscending(xValue, yValue) * -1;
		}
		private object GetPropertyValue(TKey value, PropertyDescriptor property)
		{
			return property.GetValue(value);
		}
	}
}
