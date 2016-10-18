using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using LogR.Monitor.Models;

namespace LogR.Monitor
{
	public class MonitorApplication : System.Windows.Forms.ApplicationContext
	{
		Views.IMonitorView m_View;
		Models.Settings m_Settings;

		public MonitorApplication()
		{
			m_View = new Views.MonitorView();
			m_View.StartConfiguration += StartConfiguration;
			this.MainForm = (System.Windows.Forms.Form)m_View;
		}

		protected Services.SettingsService SettingsService { get; private set; }
		protected List<Services.LogListner> LogListnerList { get; private set; }

		public void Run()
		{
			SettingsService = new Services.SettingsService();

			m_Settings = SettingsService.GetSettings();

			if (m_Settings.MonitoredApplicationList.Count == 0)
			{
				StartConfiguration();
				if (m_Settings.MonitoredApplicationList.Count == 0)
				{
					return;
				}
				SettingsService.SaveSettings(m_Settings);
			}

			LogListnerList = new List<Services.LogListner>();
			foreach (var item in m_Settings.MonitoredApplicationList)
			{
				if (!item.Enabled)
				{
					continue;
				}

				var ll = new Services.LogListner(item);
				InitializeLogListner(ll);
				LogListnerList.Add(ll);
			}

			m_View.Show();

			foreach (var item in m_Settings.MonitoredApplicationList)
			{
				if (!item.Enabled)
				{
					continue;
				}
				m_View.Notify(string.Format("listen {0}", item.SignalRUrl));
			}

			System.Threading.Tasks.Task.Factory.StartNew(() =>
				{
					foreach (var item in LogListnerList)
					{
						try
						{
							item.Start();
							m_View.Notify("started");
						}
						catch(Exception ex)
						{
							Error(ex, null);
						}
					}
				});

			Application.Run(this);

			foreach (var item in LogListnerList)
			{
				item.Stop();
			}
		}

		void InitializeLogListner(Services.LogListner logListner)
		{
			logListner.AddLog = AddLog;
			logListner.Error += Error;
		}

		void StartConfiguration()
		{
			var configurationView = new Views.ConfigurationView(m_Settings);
			var result = configurationView.ShowDialog();

			if (result == System.Windows.Forms.DialogResult.OK)
			{
				SettingsService.SaveSettings(m_Settings);
			}
		}

		void Error(Exception error, string endpoint)
		{
			m_View.Notify(error.ToString());
			var log = new Models.Log();
			log.ApplicationName = "Monitor";
			log.Category = Category.Error;
			log.CreationDate = DateTime.Now;
			log.HostName = "Monitor";
			log.MachineName = endpoint;
			log.Message = error.Message;
			log.LogId = Guid.NewGuid().ToString();
			log.ExceptionStack = error.ToString();
			AddLog(log);
		}

		void AddLog(Models.Log log)
		{
			if (log.Category == Models.Category.Debug
					&& !m_Settings.LogDebugEnabled)
			{
				return;
			}

			if (log.Category == Category.Sql
					&& !m_Settings.LogSqlEnabled)
			{
				return;
			}

			m_View.AddLog(new ViewModels.LogViewModel(log));
		}

	}
}
