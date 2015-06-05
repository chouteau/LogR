using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;

namespace LogR.Monitor.Views
{
	public partial class ConfigurationView : Form
	{
		Models.Settings m_Settings;

		public ConfigurationView(Models.Settings settings)
		{
			InitializeComponent();
			m_Settings = settings;
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			uxSettingsBindingSource.DataSource = m_Settings;
			uxApplicationsBindingSource.DataSource = m_Settings.MonitoredApplicationList;

			uxApplicationsDataGridView.AutoGenerateColumns = false;
			uxApplicationsDataGridView.DataSource = uxApplicationsBindingSource;

			uxDebugFilterCheckBox.DataBindings.Add("Checked", uxSettingsBindingSource, "LogDebugEnabled", true, DataSourceUpdateMode.OnPropertyChanged);
			uxWarningFilterCheckBox.DataBindings.Add("Checked", uxSettingsBindingSource, "LogWarningEnabled", true, DataSourceUpdateMode.OnPropertyChanged);
			uxInfoFilterCheckBox.DataBindings.Add("Checked", uxSettingsBindingSource, "LogInfoEnabled", true, DataSourceUpdateMode.OnPropertyChanged);
			uxNotificationFilterCheckBox.DataBindings.Add("Checked", uxSettingsBindingSource, "LogNotificationEnabled", true, DataSourceUpdateMode.OnPropertyChanged);
			uxExceptionFilterCheckBox.DataBindings.Add("Checked", uxSettingsBindingSource, "LogErrorEnabled", true, DataSourceUpdateMode.OnPropertyChanged);
			uxFatalFilterCheckBox.DataBindings.Add("Checked", uxSettingsBindingSource, "LogFatalEnabled", true, DataSourceUpdateMode.OnPropertyChanged);
			uxSqlCheckBox.DataBindings.Add("Checked", uxSettingsBindingSource, "LogSqlEnabled", true, DataSourceUpdateMode.OnPropertyChanged);
		}

		protected override void OnClosing(CancelEventArgs e)
		{
			uxApplicationsDataGridView.EndEdit();
			base.OnClosing(e);
		}

	}
}
