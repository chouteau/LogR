using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace LogRMonitor.ViewModels
{
	public delegate void SetProperty();

	public abstract class ViewModelBase<TModel> : INotifyPropertyChanged, INotifyPropertyChanging
	{
		private class PropertyChangedHistory
		{
			public string PropertyName
			{
				get;
				set;
			}
			public object Value
			{
				get;
				set;
			}
			public DateTime LastUpdate
			{
				get;
				set;
			}
		}
		private List<ViewModelBase<TModel>.PropertyChangedHistory> m_PropertyChangedList;
		private bool m_IsSelected;
		public event PropertyChangedEventHandler PropertyChanged;
		public event PropertyChangingEventHandler PropertyChanging;
		[Bindable(false), Browsable(false)]
		public TModel Model
		{
			get;
			private set;
		}
		[Browsable(false)]
		public virtual bool IsDirty
		{
			get;
			protected set;
		}
		[Bindable(true), Browsable(false)]
		public virtual bool IsSelected
		{
			get
			{
				return this.m_IsSelected;
			}
			set
			{
				this.m_IsSelected = value;
				this.RaisePropertyChanged("IsSelected");
			}
		}
		[Bindable(false), Browsable(false)]
		public virtual bool AllowEntityEvents
		{
			get;
			set;
		}
		[Bindable(false), Browsable(false)]
		public virtual bool ReadOnly
		{
			get;
			set;
		}
		public ViewModelBase(TModel model)
		{
			this.Model = model;
			this.IsDirty = false;
			this.AllowEntityEvents = false;
			this.ReadOnly = false;
			this.m_PropertyChangedList = new List<ViewModelBase<TModel>.PropertyChangedHistory>();
		}
		public void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression)
		{
			string propertyName = this.ExtractPropertyName<T>(propertyExpression);
			this.RaisePropertyChanged(propertyName);
		}
		protected virtual object GetOldValue<T>(Expression<Func<T>> propertyExpression)
		{
			string propertyName = null;
			T value;
			this.ExtractPropertyNameAndValue<T>(propertyExpression, out propertyName, out value);
			return (
				from i in this.m_PropertyChangedList
				orderby i.LastUpdate descending
				select i).FirstOrDefault((ViewModelBase<TModel>.PropertyChangedHistory i) => i.PropertyName == propertyName);
		}
		protected virtual void SetPropertyValue<T>(Expression<Func<T>> propertyExpression, T newValue, SetProperty dlg)
		{
			if (!this.ReadOnly)
			{
				string propertyName = null;
				T oldValue;
				this.ExtractPropertyNameAndValue<T>(propertyExpression, out propertyName, out oldValue);
				if (newValue == null || !newValue.Equals(oldValue))
				{
					if (this.AllowEntityEvents)
					{
						this.RaisePropertyChanging(propertyName);
					}
					dlg();
					this.IsDirty = true;
					ViewModelBase<TModel>.PropertyChangedHistory propertyChanged = new ViewModelBase<TModel>.PropertyChangedHistory();
					propertyChanged.PropertyName = propertyName;
					propertyChanged.Value = oldValue;
					propertyChanged.LastUpdate = DateTime.Now;
					this.m_PropertyChangedList.Add(propertyChanged);
					if (this.AllowEntityEvents)
					{
						this.RaisePropertyChanged(propertyName);
					}
				}
			}
		}
		public void UpdateProperty(string propertyName, object value)
		{
			base.GetType().GetProperty(propertyName).SetValue(this, value, null);
		}
		public virtual void AcceptChanges()
		{
			this.IsDirty = false;
			this.m_PropertyChangedList = new List<ViewModelBase<TModel>.PropertyChangedHistory>();
		}
		public virtual void SetIsDirty()
		{
			this.IsDirty = true;
		}
		protected virtual void RaisePropertyChanged(string propertyName)
		{
			PropertyChangedEventHandler handler = this.PropertyChanged;
			if (handler != null)
			{
				handler(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		protected void RaisePropertyChanged(params string[] propertyNames)
		{
			for (int i = 0; i < propertyNames.Length; i++)
			{
				string name = propertyNames[i];
				this.RaisePropertyChanged(name);
			}
		}
		protected virtual void RaisePropertyChanging(string propertyName)
		{
			PropertyChangingEventHandler handler = this.PropertyChanging;
			if (handler != null)
			{
				handler(this, new PropertyChangingEventArgs(propertyName));
			}
		}
		private string ExtractPropertyName<T>(Expression<Func<T>> propertyExpresssion)
		{
			if (propertyExpresssion == null)
			{
				throw new ArgumentNullException("propertyExpression");
			}
			MemberExpression memberExpression = propertyExpresssion.Body as MemberExpression;
			if (memberExpression == null)
			{
				throw new ArgumentException("The expression is not a member access expression.", "propertyExpression");
			}
			PropertyInfo property = memberExpression.Member as PropertyInfo;
			if (property == null)
			{
				throw new ArgumentException("The member access expression does not access a property.", "propertyExpression");
			}
			if (!property.DeclaringType.IsAssignableFrom(base.GetType()))
			{
				throw new ArgumentException("The referenced property belongs to a different type.", "propertyExpression");
			}
			MethodInfo getMethod = property.GetGetMethod(true);
			if (getMethod == null)
			{
				throw new ArgumentException("The referenced property does not have a get method.", "propertyExpression");
			}
			if (getMethod.IsStatic)
			{
				throw new ArgumentException("The referenced property is a static property.", "propertyExpression");
			}
			return memberExpression.Member.Name;
		}
		private void ExtractPropertyNameAndValue<T>(Expression<Func<T>> propertyExpresssion, out string propertyName, out T value)
		{
			if (propertyExpresssion == null)
			{
				throw new ArgumentNullException("propertyExpression");
			}
			MemberExpression memberExpression = propertyExpresssion.Body as MemberExpression;
			if (memberExpression == null)
			{
				throw new ArgumentException("The expression is not a member access expression.", "propertyExpression");
			}
			PropertyInfo property = memberExpression.Member as PropertyInfo;
			if (property == null)
			{
				throw new ArgumentException("The member access expression does not access a property.", "propertyExpression");
			}
			if (!property.DeclaringType.IsAssignableFrom(base.GetType()))
			{
				throw new ArgumentException("The referenced property belongs to a different type.", "propertyExpression");
			}
			MethodInfo getMethod = property.GetGetMethod(true);
			if (getMethod == null)
			{
				throw new ArgumentException("The referenced property does not have a get method.", "propertyExpression");
			}
			if (getMethod.IsStatic)
			{
				throw new ArgumentException("The referenced property is a static property.", "propertyExpression");
			}
			value = (T)((object)property.GetValue(this, null));
			propertyName = memberExpression.Member.Name;
		}
	}
}
