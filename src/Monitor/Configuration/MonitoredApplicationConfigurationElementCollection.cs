using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace LogR.Monitor.Configuration
{
	public class MonitoredApplicationConfigurationElementCollection : ConfigurationElementCollection
	{
		public bool IsDirty
		{
			get
			{
				return base.IsModified();
			}
		}

		protected override ConfigurationElement CreateNewElement()
		{
			return new MonitoredApplicationConfigurationElement();
		}

		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((MonitoredApplicationConfigurationElement)element).SignalRUrl;
		}

		public void Add(MonitoredApplicationConfigurationElement element)
		{
			base.BaseAdd(element);
		}

		public void Remove(MonitoredApplicationConfigurationElement element)
		{
			base.BaseRemove(element);
		}

		public void Clear()
		{
			base.BaseClear();
		}

		public override bool IsReadOnly()
		{
			return false;
		}

		public bool ContainsKey(string name)
		{
			foreach (var item in base.BaseGetAllKeys())
			{
				if (item.ToString().Equals(name, StringComparison.InvariantCultureIgnoreCase))
				{
					return true;
				}
			}
			return false;
		}

		public new ConfigurationElement this[string name]
		{
			get
			{
				return (MonitoredApplicationConfigurationElement)base.BaseGet(name);
			}
		}

		protected override bool ThrowOnDuplicate
		{
			get
			{
				return true;
			}
		}

		public override System.Configuration.ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return System.Configuration.ConfigurationElementCollectionType.AddRemoveClearMap;
			}
		}

	}
}
