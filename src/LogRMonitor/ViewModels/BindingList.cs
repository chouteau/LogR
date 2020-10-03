using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace LogRMonitor.ViewModels
{
	public class BindingList<TModel> : System.ComponentModel.BindingList<TModel>, IRaiseItemChangedEvents, ITypedList, IBindingListView
	{
		private bool _isSorted = false;
		[NonSerialized]
		private PropertyDescriptor _sortProperty;
		private ListSortDirection _sortDirection = ListSortDirection.Descending;
		[NonSerialized]
		private PropertyDescriptorCollection _shape;
		private bool _sortColumns = true;
		private List<TModel> m_UnFilteredList;

		public BindingList()
		{
			this._sortColumns = true;
			this._shape = this.GetShape();
			this.m_UnFilteredList = new List<TModel>();
		}

		protected override bool SupportsSortingCore
		{
			get
			{
				return true;
			}
		}

		protected override ListSortDirection SortDirectionCore
		{
			get
			{
				return this._sortDirection;
			}
		}

		protected override PropertyDescriptor SortPropertyCore
		{
			get
			{
				return this._sortProperty;
			}
		}

		bool IRaiseItemChangedEvents.RaisesItemChangedEvents
		{
			get
			{
				return true;
			}
		}

		protected override void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction)
		{
			var items = base.Items as List<TModel>;
			if (items != null && null != prop)
			{
				var pc = new PropertyComparer<TModel>(prop, direction);
				items.Sort(pc);
				this._isSorted = true;
			}
			else
			{
				this._isSorted = false;
			}
		}
		public PropertyDescriptorCollection GetItemProperties(PropertyDescriptor[] listAccessors)
		{
			PropertyDescriptorCollection pdc;
			if (null == listAccessors)
			{
				pdc = this._shape;
			}
			else
			{
				pdc = ListBindingHelper.GetListItemProperties(listAccessors[0].PropertyType);
			}
			return pdc;
		}

		public string GetListName(PropertyDescriptor[] listAccessors)
		{
			return typeof(TModel).Name;
		}

		private PropertyDescriptorCollection GetShape()
		{
			var pdc = TypeDescriptor.GetProperties(typeof(TModel), new Attribute[]
			{
				new BrowsableAttribute(true)
			});

			if (this._sortColumns)
			{
				pdc = pdc.Sort();
			}
			return pdc;
		}

		protected override int FindCore(PropertyDescriptor prop, object key)
		{
			return FindCore(0, prop, key);
		}

		protected int FindCore(int startIndex, PropertyDescriptor prop, object key)
		{
			var propInfo = typeof(TModel).GetProperty(prop.Name);
			TModel item;

			if (key != null)
			{
				for (int i = startIndex; i < Count; ++i)
				{
					item = (TModel)Items[i];
					if (propInfo.GetValue(item, null).Equals(key))
					{
						return i;
					}
				}
			}

			return -1;

		}

		#region IBindingListView Members

		private ListSortDescriptionCollection m_ListSortDescriptionCollection;
		public string FilterPropertyNameValue { get; set; }
		public Object FilterCompareValue { get; set; }
		private string m_FilterValue;

		public string Filter 
		{
			get
			{
				return m_FilterValue;
			}
			set
			{
				if (m_FilterValue == value)
				{
					return;
				}

				RaiseListChangedEvents = false;

				if (value == null)
				{
					this.ClearItems();
					foreach (TModel t in m_UnFilteredList)
					{
						this.Items.Add(t);
					}

					m_FilterValue = value;
				}
				else if (value == "") 
				{ 
				}
				else if (System.Text.RegularExpressions.Regex.Matches(value, "[?[\\w ]+]? ?[=] ?'?[\\w|/: ]+'?",  System.Text.RegularExpressions.RegexOptions.Singleline).Count == 1)
				{
					m_UnFilteredList.Clear();
					m_UnFilteredList.AddRange(this.Items);
					m_FilterValue = value;
					GetFilterParts();
					ApplyFilter();
				}
				else if (System.Text.RegularExpressions.Regex.Matches(value, "[?[\\w ]+]? ?[=] ?'?[\\w|/: ]+'?", System.Text.RegularExpressions.RegexOptions.Singleline).Count > 1)
				{
					throw new ArgumentException("Multi-column filtering is not implemented.");
				}
				else 
				{
					throw new ArgumentException("Filter is not in the format: propName = 'value'.");
				}

				RaiseListChangedEvents = true;

				OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
			}
		}

		public void ApplySort(ListSortDescriptionCollection sorts)
		{
			m_ListSortDescriptionCollection = sorts;
		}

		public void RemoveFilter()
		{
			if (Filter != null)
			{
				Filter = null;
			}
		}

		public ListSortDescriptionCollection SortDescriptions
		{
			get
			{
				return m_ListSortDescriptionCollection;
			}
		}

		public bool SupportsAdvancedSorting
		{
			get { return true; }
		}

		public bool SupportsFiltering
		{
			get { return true; }
		}

		#endregion

		private void GetFilterParts()
		{
			var filterParts = Filter.Split(new char[] { '=' }, 	StringSplitOptions.RemoveEmptyEntries);
			FilterPropertyNameValue = filterParts[0].Replace("[", "").Replace("]", "").Trim();

			PropertyDescriptor propDesc = TypeDescriptor.GetProperties(typeof(TModel))[FilterPropertyNameValue.ToString()];

			if (propDesc != null)
			{
				try
				{
					TypeConverter converter = TypeDescriptor.GetConverter(propDesc.PropertyType);
					FilterCompareValue = converter.ConvertFromString(filterParts[1].Replace("'", "").Trim());
				}
				catch (NotSupportedException)
				{
					throw new ArgumentException("Specified filter value " + FilterCompareValue + " can not be converted from string...Implement a type converter for " + propDesc.PropertyType.ToString());
				}
			}
			else
			{
				throw new ArgumentException("Specified property '" + FilterPropertyNameValue + "' is not found on type " + typeof(TModel).Name + ".");
			}
		}

		private void ApplyFilter()
		{
			m_UnFilteredList.Clear();
			m_UnFilteredList.AddRange(this.Items);
			var results = new List<TModel>();

			PropertyDescriptor propDesc = TypeDescriptor.GetProperties(typeof(TModel))[FilterPropertyNameValue];

			if (propDesc != null)
			{
				int tempResults = -1;
				do
				{
					tempResults = FindCore(tempResults + 1, propDesc, FilterCompareValue);
					if (tempResults != -1)
					{
						results.Add(this[tempResults]);
					}

				} while (tempResults != -1);
			}

			this.ClearItems();

			if (results != null && results.Count > 0)
			{
				foreach (var itemFound in results)
				{
					this.Add(itemFound);
				}
			}
		}

	}
}
