using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System.Runtime.InteropServices;
using System.Reflection;

namespace LogR.Monitor.Views
{
	public partial class MonitorView : Form, IMonitorView
	{
		private bool m_ScrollingEnabled = true;
		private bool m_AppendLogEnabled = true;
		private bool m_Filtering = false;
		private ActionQueue m_ActionQueue;
		private System.Linq.Expressions.Expression<Func<ViewModels.LogViewModel, bool>> m_CurrentFilter;

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
			if (!m_AppendLogEnabled)
			{
				return;
			}
			m_ActionQueue.Add((Action)(() =>
				{
					this.UnfilteredLogList.Add(log);
					if (!m_Filtering)
					{
						if (m_CurrentFilter != null)
						{
							var tempList = new ViewModels.BindingList<ViewModels.LogViewModel>();
							tempList.Add(log);
							foreach (var item in tempList.AsQueryable().Where(m_CurrentFilter))
							{
								this.LogList.Add(item);
							}
						}
						else
						{
							this.LogList.Add(log);
						}
					}
					if (this.m_ScrollingEnabled)
					{
						if (this.LogList.Count > 20000)
						{
							this.LogList.RemoveAt(0);
						}
						uxLogBindingSource.MoveLast();
					}
				}));
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

			this.uxClearButton.Click += delegate(object s, EventArgs arg)
			{
				LogList.Clear();
			};
			this.uxScrollingButton.Click += delegate(object s, EventArgs arg)
			{
				if (m_ScrollingEnabled)
				{
					m_ScrollingEnabled = false;
					uxScrollingButton.Text = "Start scrolling";
					uxScrollingButton.Image = Properties.Resources.stop_pause_16x16;
				}
				else
				{
					m_ScrollingEnabled = true;
					this.uxScrollingButton.Text = "Stop scrolling";
					uxScrollingButton.Image = Properties.Resources.start_20x20;
				}
			};

			uxDebugFilterButton.Click += (s, arg) => { FilterDataSource(uxDebugFilterButton, i => i.Category == Models.Category.Debug); };
			uxWarningFilterButton.Click += (s, arg) => { FilterDataSource(uxWarningFilterButton, i => i.Category == Models.Category.Warn); }; 
			uxExceptionFilterButton.Click += (s, arg) => { FilterDataSource(uxExceptionFilterButton, i => i.Category == Models.Category.Error || i.Category == Models.Category.Fatal); }; 
			uxNotificationFilterButton.Click += (s, arg) => { FilterDataSource(uxNotificationFilterButton, i => i.Category == Models.Category.Notification); }; 
			uxFatalFilterButton.Click += (s, arg) => { FilterDataSource(uxFatalFilterButton, i => i.Category == Models.Category.Fatal); };

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
					uxStopStartLogToolStripButton.Image = Properties.Resources.stop_pause_16x16;
				}
				else
				{
					m_AppendLogEnabled = true;
					uxStopStartLogToolStripButton.Text = "Stop logs";
					uxStopStartLogToolStripButton.Image = Properties.Resources.start_20x20;
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
			switch (log.Category)
			{
				case Models.Category.Info:
					e.CellStyle.BackColor = Color.YellowGreen;
					break;
				case Models.Category.Warn:
					e.CellStyle.BackColor = Color.Orange;
					break;
				case Models.Category.Error:
					e.CellStyle.BackColor = Color.Red;
					e.CellStyle.ForeColor = Color.White;
					break;
				case Models.Category.Fatal:
					e.CellStyle.BackColor = Color.Black;
					e.CellStyle.ForeColor = Color.White;
					break;
				case Models.Category.Notification:
					e.CellStyle.BackColor = Color.LightCyan;
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

		private void FilterDataSource(ToolStripButton button, System.Linq.Expressions.Expression<Func<ViewModels.LogViewModel, bool>> predicate)
		{
			m_Filtering = true;
			LogList.Clear();
			if (!button.Checked)
			{
				m_CurrentFilter = predicate;
				var list = UnfilteredLogList.AsQueryable().Where(predicate).ToList();
				foreach (var item in list)
				{
					LogList.Add(item);
				}
			}
			else
			{
				m_CurrentFilter = null;
				foreach (var item in UnfilteredLogList)
				{
					LogList.Add(item);
				}
			}
			uxLogBindingSource.MoveLast();
			button.Checked = !button.Checked;

			if (button != uxDebugFilterButton)
			{
				uxDebugFilterButton.Checked = false;
			}
			if (button != uxWarningFilterButton)
			{
				uxWarningFilterButton.Checked = false;
			}
			if (button != uxExceptionFilterButton)
			{
				uxExceptionFilterButton.Checked = false;
			}
			if (button != uxNotificationFilterButton)
			{
				uxNotificationFilterButton.Checked = false;
			}

			m_Filtering = false;
		}

		private void Search(string input)
		{
			if (string.IsNullOrWhiteSpace(input))
			{
				m_CurrentFilter = null;
				LogList.Clear();
				foreach (var item in UnfilteredLogList)
				{
					LogList.Add(item);
				}
				return;
			}
			m_Filtering = true;
			LogList.Clear();
			var list = UnfilteredLogList.AsQueryable()
							.Where(i => (i.Message??string.Empty).IndexOf(input, StringComparison.InvariantCultureIgnoreCase) != -1
										|| (i.ExceptionStack??string.Empty).IndexOf(input, StringComparison.InvariantCultureIgnoreCase) != -1
										|| (i.MachineName??string.Empty).IndexOf(input, StringComparison.InvariantCultureIgnoreCase) != -1
										|| (i.HostName??string.Empty).IndexOf(input, StringComparison.InvariantCultureIgnoreCase) != -1
									)
							.ToList();
			foreach (var item in list)
			{
				LogList.Add(item);
			}
			uxLogBindingSource.MoveLast();
			m_Filtering = false;
		}
	}
}
