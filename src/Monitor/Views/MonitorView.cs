using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using LogR.Monitor.Forms;

namespace LogR.Monitor.Views
{
	public partial class MonitorView : Form, IMonitorView
	{
		private bool m_ScrollingEnabled = true;
		ActionQueue m_ActionQueue;

		public MonitorView()
		{
			InitializeComponent();
			LogList = new ViewModels.BindingList<ViewModels.LogViewModel>();
		}

		public ViewModels.BindingList<ViewModels.LogViewModel> LogList { get; set; }

		public event Action StartConfiguration;

		public void AddLog(ViewModels.LogViewModel log)
		{
			m_ActionQueue.Add(() =>
				{
					LogList.Add(log);
					if (this.m_ScrollingEnabled)
					{
						uxLogBindingSource.MoveLast();
					}
				});
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
			m_ActionQueue = new ActionQueue();

			uxLogBindingSource.DataSource = LogList;
			uxLogBindingSource.PositionChanged += new EventHandler(this.bindingSource1_PositionChanged);
			uxLogDataGridView.AutoGenerateColumns = false;
			uxLogDataGridView.DataSource = uxLogBindingSource;
			// Add the AutoFilter header cell to each column.
			foreach (DataGridViewColumn col in uxLogDataGridView.Columns)
			{
				if (col is Forms.DataGridViewAutoFilterTextBoxColumn)
				{
					col.HeaderCell = new DataGridViewAutoFilterColumnHeaderCell(col.HeaderCell);
				}
			}
			uxLogDataGridView.CellFormatting += new DataGridViewCellFormattingEventHandler(this.OnDataGridViewCellFormatting);
			uxWarningFilterCheckBox.Click += (s, arg) =>
			{
				var data = uxLogBindingSource as IBindingListView;
				data.Filter = "Category = 'Warn'";
			};


			this.uxPauseButton.Enabled = false;
			this.uxPauseButton.Click += delegate(object s, EventArgs arg)
			{
				this.uxPauseButton.Enabled = false;
			};
			this.uxClearButton.Click += delegate(object s, EventArgs arg)
			{
				LogList.Clear();
			};
			this.uxScrollingButton.Click += delegate(object s, EventArgs arg)
			{
				this.m_ScrollingEnabled = !this.m_ScrollingEnabled;
				this.uxScrollingButton.Text = (this.m_ScrollingEnabled ? "Stop scrolling" : "Start scrolling");
			};

			m_ActionQueue.Run();
		}

		protected override void OnClosing(CancelEventArgs e)
		{
			m_ActionQueue.Stop();
			base.OnClosing(e);
		}

		private void bindingSource1_PositionChanged(object sender, EventArgs e)
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

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			if (StartConfiguration != null)
			{
				StartConfiguration();
			}
		}

	}
}
