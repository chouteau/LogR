using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

using LogRPush;

using LogWinRMonitor.Models;

using Microsoft.Extensions.Logging;

namespace LogWinRMonitor.Views
{
	public partial class MonitorView : Form, IMonitorView
	{
		private bool m_ScrollingEnabled = true;
		private bool m_AppendLogEnabled = true;
		private ActionQueue m_ActionQueue;
		LogFilter filter = new();
		List<FilterItem> hostList = new();
		List<FilterItem> machineNameList = new();
		List<FilterItem> applicationNameList = new();
		List<FilterItem> contextList = new();

		public MonitorView()
		{
			InitializeComponent();
			UnfilteredLogList = new ViewModels.BindingList<ViewModels.LogViewModel>();
			LogList = new ViewModels.BindingList<ViewModels.LogViewModel>();
		}

		public ViewModels.BindingList<ViewModels.LogViewModel> UnfilteredLogList { get; set; }
		public ViewModels.BindingList<ViewModels.LogViewModel> LogList { get; set; }

		public event Action StartConfiguration;

		public void AddLog(ViewModels.LogViewModel log)
		{
			UnfilteredLogList.Add(log);

			var level = filter.LevelList.SingleOrDefault(i => i.Value == log.LogLevel && i.Checked);
			var searchFilter = filter.Search == null || IsMatchSearch(log.Model, filter.Search);

			var machineNameFilter = filter.AllMachine
									|| filter.MachineNameList.Exists(i => i.Equals(log.MachineName, StringComparison.InvariantCultureIgnoreCase));

			var contextFilter = filter.AllContext
								|| filter.ContextList.Exists(i => i.Equals(log.Context, StringComparison.InvariantCultureIgnoreCase));

			var hostNameFilter = filter.AllHost
								|| filter.HostNameList.Exists(i => i.Equals(log.HostName, StringComparison.InvariantCultureIgnoreCase));

			var applicationNameFilter = filter.AllApplication
										|| filter.ApplicationNameList.Exists(i => i.Equals(log.ApplicationName, StringComparison.InvariantCultureIgnoreCase));

			m_ActionQueue.Add(() =>
			{

				if (m_AppendLogEnabled
					&& level != null
					&& machineNameFilter
					&& contextFilter
					&& hostNameFilter
					&& applicationNameFilter
					&& searchFilter)
				{
					if (LogList.Count > 20000)
					{
						var last = LogList.Last();
						LogList.Remove(last);
					}
					LogList.Insert(0, log);

					if (!hostList.Exists(i => log.HostName.Equals(i.Name, StringComparison.InvariantCultureIgnoreCase)))
					{
						hostList.Add(new FilterItem
						{
							IsSelected = true,
							Name = log.HostName
						});
					}

					if (!machineNameList.Exists(i => log.MachineName.Equals(i.Name, StringComparison.InvariantCultureIgnoreCase)))
					{
						machineNameList.Add(new FilterItem
						{
							IsSelected = true,
							Name = log.MachineName
						});
					}

					if (!applicationNameList.Exists(i => log.ApplicationName.Equals(i.Name, StringComparison.InvariantCultureIgnoreCase)))
					{
						applicationNameList.Add(new FilterItem
						{
							IsSelected = true,
							Name = log.ApplicationName
						});
					}

					if (!contextList.Exists(i => log.Context.Equals(i.Name, StringComparison.InvariantCultureIgnoreCase)))
					{
						contextList.Add(new FilterItem
						{
							IsSelected = true,
							Name = log.Context
						});
					}
				}

				if (this.m_ScrollingEnabled)
				{
					if (this.LogList.Count > 20000)
					{
						this.LogList.RemoveAt(0);
					}
					uxLogBindingSource.MoveFirst();
				}
			});
		}

		public bool IsMatchSearch(LogRPush.LogInfo i, string search)
		{
			if (string.IsNullOrWhiteSpace(search))
			{
				return false;
			}
			filter.Search = search;
			return $"{i.MachineName}{i.Message}{i.ApplicationName}{i.Context}{i.HostName}{i.ExceptionStack}".IndexOf(search, StringComparison.InvariantCultureIgnoreCase) != -1;
		}

		public void Notify(string message)
		{
			m_ActionQueue.Add(() =>
				{
					try
					{
						uxNotificationRichTextBox.AppendText(message);
						uxNotificationRichTextBox.AppendText(System.Environment.NewLine);
					}
					catch
					{
					}
				});
		}

		protected override void OnLoad(EventArgs e)
		{
			var versionAttribute = typeof(Program).Assembly.GetCustomAttribute<System.Reflection.AssemblyFileVersionAttribute>().Version;
			this.Text = this.Text + " " + versionAttribute;

			m_ActionQueue = new ActionQueue();

			uxLogBindingSource.DataSource = LogList;
			uxLogBindingSource.PositionChanged += new EventHandler(this.BindingSourcePositionChanged);
			uxLogDataGridView.AutoGenerateColumns = false;
			uxLogDataGridView.DataSource = uxLogBindingSource;
			uxLogDataGridView.CellFormatting += new DataGridViewCellFormattingEventHandler(this.OnDataGridViewCellFormatting);

			this.uxClearButton.Click += delegate (object s, EventArgs arg)
			{
				LogList.Clear();
			};
			this.uxScrollingButton.Click += delegate (object s, EventArgs arg)
			{
				if (m_ScrollingEnabled)
				{
					m_ScrollingEnabled = false;
					uxScrollingButton.Text = "Start scrolling";
					uxScrollingButton.Image = LogWinRMonitor.Properties.Resources.stop_pause_16x16;
				}
				else
				{
					m_ScrollingEnabled = true;
					this.uxScrollingButton.Text = "Stop scrolling";
					uxScrollingButton.Image = LogWinRMonitor.Properties.Resources.start_20x20;
				}
			};

			uxTraceFilterButton.Click += (s, arg) => { FilterDataSource(uxTraceFilterButton, LogLevel.Trace); };
			uxDebugFilterButton.Click += (s, arg) => { FilterDataSource(uxDebugFilterButton, LogLevel.Debug); };
			uxInfoFilterButton.Click += (s, arg) => { FilterDataSource(uxInfoFilterButton, LogLevel.Information); };
			uxWarningFilterButton.Click += (s, arg) => { FilterDataSource(uxWarningFilterButton, LogLevel.Warning); };
			uxExceptionFilterButton.Click += (s, arg) => { FilterDataSource(uxExceptionFilterButton, LogLevel.Error); };
			uxFatalFilterButton.Click += (s, arg) => { FilterDataSource(uxFatalFilterButton, LogLevel.Critical); };

			uxSearchToolStripButton.Click += (s, arg) =>
			{
				Search(uxSearchToolStripTextBox.Text);
			};
			uxSearchToolStripTextBox.KeyDown += (s, arg) =>
			{
				if (arg.KeyCode == Keys.Enter)
				{
					Search(uxSearchToolStripTextBox.Text);
				}
			};

			uxStopStartLogToolStripButton.Click += (s, arg) =>
			{
				if (m_AppendLogEnabled)
				{
					m_AppendLogEnabled = false;
					uxStopStartLogToolStripButton.Text = "Start logs";
					uxStopStartLogToolStripButton.Image = LogWinRMonitor.Properties.Resources.stop_pause_16x16;
				}
				else
				{
					m_AppendLogEnabled = true;
					uxStopStartLogToolStripButton.Text = "Stop logs";
					uxStopStartLogToolStripButton.Image = LogWinRMonitor.Properties.Resources.start_20x20;
				}
			};

			m_ActionQueue.Run();
		}

		protected override void OnClosing(CancelEventArgs e)
		{
			m_ActionQueue.Stop();
			base.OnClosing(e);
		}

		private void BindingSourcePositionChanged(object sender, EventArgs e)
		{
			if (uxLogDataGridView.DataSource == null)
			{
				return;
			}
			var log = uxLogBindingSource.Current as ViewModels.LogViewModel;
			if (log != null)
			{
				this.uxStackTextBox.Text = log.ExceptionStack;
			}
		}

		private void OnDataGridViewCellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			if (e.RowIndex == -1 || e.RowIndex >= LogList.Count)
			{
				return;
			}
			var row = uxLogDataGridView.Rows[e.RowIndex];
			var log = row.DataBoundItem as ViewModels.LogViewModel;
			if (log == null)
			{
				return;
			}
			switch (log.LogLevel)
			{
				case LogLevel.Trace:
					e.CellStyle.BackColor = Color.LightGray;
					e.CellStyle.ForeColor = Color.Black;
					break;
				case LogLevel.Debug:
					e.CellStyle.BackColor = Color.Gray;
					e.CellStyle.ForeColor = Color.White;
					break;
				case LogLevel.Information:
					e.CellStyle.BackColor = Color.YellowGreen;
					break;
				case LogLevel.Warning:
					e.CellStyle.BackColor = Color.Orange;
					break;
				case LogLevel.Error:
					e.CellStyle.BackColor = Color.Red;
					e.CellStyle.ForeColor = Color.White;
					break;
				case LogLevel.Critical:
					e.CellStyle.BackColor = Color.Black;
					e.CellStyle.ForeColor = Color.White;
					break;
				default:
					e.CellStyle.BackColor = Color.White;
					e.CellStyle.ForeColor = Color.Brown;
					break;
			}
		}

		private void StartConfigurationClick(object sender, EventArgs e)
		{
			if (StartConfiguration != null)
			{
				StartConfiguration();
			}
		}

		private void FilterDataSource(ToolStripButton button, LogLevel logLevel)
		{
			LogList.Clear();
			if (!button.Checked)
			{
				filter.LevelList.Single(i => i.Value == logLevel).Checked = false;
			}
			else
			{
				filter.LevelList.Single(i => i.Value == logLevel).Checked = true;
			}
			uxLogBindingSource.MoveLast();
			button.Checked = !button.Checked;
		}

		private void Search(string input)
		{
			if (string.IsNullOrWhiteSpace(input))
			{
				LogList.Clear();
				foreach (var item in UnfilteredLogList)
				{
					LogList.Add(item);
				}
				return;
			}
			LogList.Clear();
			var list = UnfilteredLogList.AsQueryable()
							.Where(i => (i.Message ?? string.Empty).IndexOf(input, StringComparison.InvariantCultureIgnoreCase) != -1
										|| (i.ExceptionStack ?? string.Empty).IndexOf(input, StringComparison.InvariantCultureIgnoreCase) != -1
										|| (i.MachineName ?? string.Empty).IndexOf(input, StringComparison.InvariantCultureIgnoreCase) != -1
										|| (i.HostName ?? string.Empty).IndexOf(input, StringComparison.InvariantCultureIgnoreCase) != -1
									)
							.ToList();
			foreach (var item in list)
			{
				LogList.Add(item);
			}
			uxLogBindingSource.MoveLast();
		}
	}
}
