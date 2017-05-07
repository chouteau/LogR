namespace LogR.Monitor.Views
{
	partial class MonitorView
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MonitorView));
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.uxLogDataGridView = new System.Windows.Forms.DataGridView();
			this.uxCreationDateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.uxMachineNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.uxHosterNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.uxApplicationNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.uxCategoryColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.uxMessageColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.uxExceptionStack = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.uxStackTextBox = new System.Windows.Forms.RichTextBox();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.uxConfigurationButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.uxScrollingButton = new System.Windows.Forms.ToolStripButton();
			this.uxClearButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			this.uxExceptionFilterButton = new System.Windows.Forms.ToolStripButton();
			this.uxWarningFilterButton = new System.Windows.Forms.ToolStripButton();
			this.uxNotificationFilterButton = new System.Windows.Forms.ToolStripButton();
			this.uxDebugFilterButton = new System.Windows.Forms.ToolStripButton();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.uxNotificationRichTextBox = new System.Windows.Forms.RichTextBox();
			this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewAutoFilterTextBoxColumn1 = new LogR.Monitor.Forms.DataGridViewAutoFilterTextBoxColumn();
			this.dataGridViewAutoFilterTextBoxColumn2 = new LogR.Monitor.Forms.DataGridViewAutoFilterTextBoxColumn();
			this.dataGridViewAutoFilterTextBoxColumn3 = new LogR.Monitor.Forms.DataGridViewAutoFilterTextBoxColumn();
			this.dataGridViewAutoFilterTextBoxColumn4 = new LogR.Monitor.Forms.DataGridViewAutoFilterTextBoxColumn();
			this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewCheckBoxColumn2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewCheckBoxColumn3 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.uxLogBindingSource = new System.Windows.Forms.BindingSource(this.components);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.uxLogDataGridView)).BeginInit();
			this.toolStrip1.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.uxLogBindingSource)).BeginInit();
			this.SuspendLayout();
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(3, 3);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.uxLogDataGridView);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.uxStackTextBox);
			this.splitContainer1.Size = new System.Drawing.Size(988, 445);
			this.splitContainer1.SplitterDistance = 267;
			this.splitContainer1.TabIndex = 12;
			// 
			// uxLogDataGridView
			// 
			this.uxLogDataGridView.AllowUserToAddRows = false;
			this.uxLogDataGridView.AllowUserToDeleteRows = false;
			this.uxLogDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.uxLogDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.uxCreationDateColumn,
            this.uxMachineNameColumn,
            this.uxHosterNameColumn,
            this.uxApplicationNameColumn,
            this.uxCategoryColumn,
            this.uxMessageColumn,
            this.uxExceptionStack});
			this.uxLogDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.uxLogDataGridView.Location = new System.Drawing.Point(0, 0);
			this.uxLogDataGridView.Name = "uxLogDataGridView";
			this.uxLogDataGridView.Size = new System.Drawing.Size(988, 267);
			this.uxLogDataGridView.TabIndex = 9;
			// 
			// uxCreationDateColumn
			// 
			this.uxCreationDateColumn.DataPropertyName = "CreationDate";
			dataGridViewCellStyle1.Format = "T";
			dataGridViewCellStyle1.NullValue = null;
			this.uxCreationDateColumn.DefaultCellStyle = dataGridViewCellStyle1;
			this.uxCreationDateColumn.HeaderText = "Date";
			this.uxCreationDateColumn.Name = "uxCreationDateColumn";
			this.uxCreationDateColumn.Width = 80;
			// 
			// uxMachineNameColumn
			// 
			this.uxMachineNameColumn.DataPropertyName = "MachineName";
			this.uxMachineNameColumn.FillWeight = 110F;
			this.uxMachineNameColumn.HeaderText = "Machine";
			this.uxMachineNameColumn.Name = "uxMachineNameColumn";
			this.uxMachineNameColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.uxMachineNameColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
			// 
			// uxHosterNameColumn
			// 
			this.uxHosterNameColumn.DataPropertyName = "HostName";
			this.uxHosterNameColumn.FillWeight = 120F;
			this.uxHosterNameColumn.HeaderText = "Host";
			this.uxHosterNameColumn.Name = "uxHosterNameColumn";
			this.uxHosterNameColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.uxHosterNameColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
			this.uxHosterNameColumn.Width = 110;
			// 
			// uxApplicationNameColumn
			// 
			this.uxApplicationNameColumn.DataPropertyName = "ApplicationName";
			this.uxApplicationNameColumn.HeaderText = "Application";
			this.uxApplicationNameColumn.Name = "uxApplicationNameColumn";
			this.uxApplicationNameColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.uxApplicationNameColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
			this.uxApplicationNameColumn.Width = 110;
			// 
			// uxCategoryColumn
			// 
			this.uxCategoryColumn.DataPropertyName = "Category";
			this.uxCategoryColumn.HeaderText = "Category";
			this.uxCategoryColumn.Name = "uxCategoryColumn";
			this.uxCategoryColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.uxCategoryColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
			this.uxCategoryColumn.Width = 70;
			// 
			// uxMessageColumn
			// 
			this.uxMessageColumn.DataPropertyName = "Message";
			this.uxMessageColumn.HeaderText = "Message";
			this.uxMessageColumn.Name = "uxMessageColumn";
			this.uxMessageColumn.Width = 400;
			// 
			// uxExceptionStack
			// 
			this.uxExceptionStack.DataPropertyName = "ExceptionStack";
			this.uxExceptionStack.HeaderText = "Stack";
			this.uxExceptionStack.Name = "uxExceptionStack";
			this.uxExceptionStack.Width = 200;
			// 
			// uxStackTextBox
			// 
			this.uxStackTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.uxStackTextBox.Location = new System.Drawing.Point(0, 0);
			this.uxStackTextBox.Name = "uxStackTextBox";
			this.uxStackTextBox.Size = new System.Drawing.Size(988, 174);
			this.uxStackTextBox.TabIndex = 15;
			this.uxStackTextBox.Text = "";
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uxConfigurationButton,
            this.toolStripSeparator1,
            this.uxScrollingButton,
            this.uxClearButton,
            this.toolStripSeparator2,
            this.toolStripButton1,
            this.uxExceptionFilterButton,
            this.uxWarningFilterButton,
            this.uxNotificationFilterButton,
            this.uxDebugFilterButton});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(1002, 25);
			this.toolStrip1.TabIndex = 14;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// uxConfigurationButton
			// 
			this.uxConfigurationButton.Image = global::LogR.Monitor.Properties.Resources.PropertiesHS;
			this.uxConfigurationButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.uxConfigurationButton.Name = "uxConfigurationButton";
			this.uxConfigurationButton.Size = new System.Drawing.Size(101, 22);
			this.uxConfigurationButton.Text = "Configuration";
			this.uxConfigurationButton.Click += new System.EventHandler(this.StartConfigurationClick);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// uxScrollingButton
			// 
			this.uxScrollingButton.Image = ((System.Drawing.Image)(resources.GetObject("uxScrollingButton.Image")));
			this.uxScrollingButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.uxScrollingButton.Name = "uxScrollingButton";
			this.uxScrollingButton.Size = new System.Drawing.Size(99, 22);
			this.uxScrollingButton.Text = "Stop scrolling";
			// 
			// uxClearButton
			// 
			this.uxClearButton.Image = ((System.Drawing.Image)(resources.GetObject("uxClearButton.Image")));
			this.uxClearButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.uxClearButton.Name = "uxClearButton";
			this.uxClearButton.Size = new System.Drawing.Size(54, 22);
			this.uxClearButton.Text = "Clear";
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			// 
			// toolStripButton1
			// 
			this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
			this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton1.Name = "toolStripButton1";
			this.toolStripButton1.Size = new System.Drawing.Size(51, 22);
			this.toolStripButton1.Text = "Save";
			// 
			// uxExceptionFilterButton
			// 
			this.uxExceptionFilterButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.uxExceptionFilterButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.uxExceptionFilterButton.Image = ((System.Drawing.Image)(resources.GetObject("uxExceptionFilterButton.Image")));
			this.uxExceptionFilterButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.uxExceptionFilterButton.Name = "uxExceptionFilterButton";
			this.uxExceptionFilterButton.Size = new System.Drawing.Size(62, 22);
			this.uxExceptionFilterButton.Text = "Exception";
			// 
			// uxWarningFilterButton
			// 
			this.uxWarningFilterButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.uxWarningFilterButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.uxWarningFilterButton.Image = ((System.Drawing.Image)(resources.GetObject("uxWarningFilterButton.Image")));
			this.uxWarningFilterButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.uxWarningFilterButton.Name = "uxWarningFilterButton";
			this.uxWarningFilterButton.Size = new System.Drawing.Size(56, 22);
			this.uxWarningFilterButton.Text = "Warning";
			// 
			// uxNotificationFilterButton
			// 
			this.uxNotificationFilterButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.uxNotificationFilterButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.uxNotificationFilterButton.Image = ((System.Drawing.Image)(resources.GetObject("uxNotificationFilterButton.Image")));
			this.uxNotificationFilterButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.uxNotificationFilterButton.Name = "uxNotificationFilterButton";
			this.uxNotificationFilterButton.Size = new System.Drawing.Size(74, 22);
			this.uxNotificationFilterButton.Text = "Notification";
			// 
			// uxDebugFilterButton
			// 
			this.uxDebugFilterButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.uxDebugFilterButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.uxDebugFilterButton.Image = ((System.Drawing.Image)(resources.GetObject("uxDebugFilterButton.Image")));
			this.uxDebugFilterButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.uxDebugFilterButton.Name = "uxDebugFilterButton";
			this.uxDebugFilterButton.Size = new System.Drawing.Size(46, 22);
			this.uxDebugFilterButton.Text = "Debug";
			// 
			// tabControl1
			// 
			this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage3);
			this.tabControl1.Location = new System.Drawing.Point(0, 28);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(1002, 477);
			this.tabControl1.TabIndex = 15;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.splitContainer1);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(994, 451);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Logs";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// tabPage3
			// 
			this.tabPage3.Controls.Add(this.uxNotificationRichTextBox);
			this.tabPage3.Location = new System.Drawing.Point(4, 22);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage3.Size = new System.Drawing.Size(994, 451);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "Notifications";
			this.tabPage3.UseVisualStyleBackColor = true;
			// 
			// uxNotificationRichTextBox
			// 
			this.uxNotificationRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.uxNotificationRichTextBox.Location = new System.Drawing.Point(3, 3);
			this.uxNotificationRichTextBox.Name = "uxNotificationRichTextBox";
			this.uxNotificationRichTextBox.Size = new System.Drawing.Size(988, 445);
			this.uxNotificationRichTextBox.TabIndex = 0;
			this.uxNotificationRichTextBox.Text = "";
			// 
			// dataGridViewTextBoxColumn1
			// 
			this.dataGridViewTextBoxColumn1.DataPropertyName = "CreationDate";
			dataGridViewCellStyle2.Format = "T";
			dataGridViewCellStyle2.NullValue = null;
			this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle2;
			this.dataGridViewTextBoxColumn1.HeaderText = "Date";
			this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
			this.dataGridViewTextBoxColumn1.Width = 70;
			// 
			// dataGridViewAutoFilterTextBoxColumn1
			// 
			this.dataGridViewAutoFilterTextBoxColumn1.DataPropertyName = "MachineName";
			this.dataGridViewAutoFilterTextBoxColumn1.HeaderText = "Machine";
			this.dataGridViewAutoFilterTextBoxColumn1.Name = "dataGridViewAutoFilterTextBoxColumn1";
			this.dataGridViewAutoFilterTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridViewAutoFilterTextBoxColumn1.Width = 70;
			// 
			// dataGridViewAutoFilterTextBoxColumn2
			// 
			this.dataGridViewAutoFilterTextBoxColumn2.DataPropertyName = "HostName";
			this.dataGridViewAutoFilterTextBoxColumn2.HeaderText = "Host";
			this.dataGridViewAutoFilterTextBoxColumn2.Name = "dataGridViewAutoFilterTextBoxColumn2";
			this.dataGridViewAutoFilterTextBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridViewAutoFilterTextBoxColumn2.Width = 70;
			// 
			// dataGridViewAutoFilterTextBoxColumn3
			// 
			this.dataGridViewAutoFilterTextBoxColumn3.DataPropertyName = "ApplicationName";
			this.dataGridViewAutoFilterTextBoxColumn3.HeaderText = "Application";
			this.dataGridViewAutoFilterTextBoxColumn3.Name = "dataGridViewAutoFilterTextBoxColumn3";
			this.dataGridViewAutoFilterTextBoxColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridViewAutoFilterTextBoxColumn3.Width = 60;
			// 
			// dataGridViewAutoFilterTextBoxColumn4
			// 
			this.dataGridViewAutoFilterTextBoxColumn4.DataPropertyName = "Category";
			this.dataGridViewAutoFilterTextBoxColumn4.HeaderText = "Category";
			this.dataGridViewAutoFilterTextBoxColumn4.Name = "dataGridViewAutoFilterTextBoxColumn4";
			this.dataGridViewAutoFilterTextBoxColumn4.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridViewAutoFilterTextBoxColumn4.Width = 60;
			// 
			// dataGridViewTextBoxColumn2
			// 
			this.dataGridViewTextBoxColumn2.DataPropertyName = "Message";
			this.dataGridViewTextBoxColumn2.HeaderText = "Message";
			this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
			this.dataGridViewTextBoxColumn2.Width = 400;
			// 
			// dataGridViewTextBoxColumn3
			// 
			this.dataGridViewTextBoxColumn3.DataPropertyName = "ExceptionStack";
			this.dataGridViewTextBoxColumn3.HeaderText = "Stack";
			this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
			this.dataGridViewTextBoxColumn3.Width = 200;
			// 
			// dataGridViewCheckBoxColumn1
			// 
			this.dataGridViewCheckBoxColumn1.DataPropertyName = "IsEnabled";
			this.dataGridViewCheckBoxColumn1.Frozen = true;
			this.dataGridViewCheckBoxColumn1.HeaderText = "E.";
			this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
			this.dataGridViewCheckBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridViewCheckBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.dataGridViewCheckBoxColumn1.ToolTipText = "Enable";
			this.dataGridViewCheckBoxColumn1.Width = 20;
			// 
			// dataGridViewTextBoxColumn4
			// 
			this.dataGridViewTextBoxColumn4.DataPropertyName = "MachineName";
			this.dataGridViewTextBoxColumn4.Frozen = true;
			this.dataGridViewTextBoxColumn4.HeaderText = "Machine";
			this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
			this.dataGridViewTextBoxColumn4.Width = 70;
			// 
			// dataGridViewTextBoxColumn5
			// 
			this.dataGridViewTextBoxColumn5.DataPropertyName = "HostName";
			this.dataGridViewTextBoxColumn5.Frozen = true;
			this.dataGridViewTextBoxColumn5.HeaderText = "Host";
			this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
			this.dataGridViewTextBoxColumn5.Width = 70;
			// 
			// dataGridViewTextBoxColumn6
			// 
			this.dataGridViewTextBoxColumn6.DataPropertyName = "ApplicationName";
			this.dataGridViewTextBoxColumn6.Frozen = true;
			this.dataGridViewTextBoxColumn6.HeaderText = "Application";
			this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
			this.dataGridViewTextBoxColumn6.Width = 60;
			// 
			// dataGridViewTextBoxColumn7
			// 
			this.dataGridViewTextBoxColumn7.DataPropertyName = "Name";
			this.dataGridViewTextBoxColumn7.Frozen = true;
			this.dataGridViewTextBoxColumn7.HeaderText = "Task Name";
			this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
			this.dataGridViewTextBoxColumn7.ReadOnly = true;
			this.dataGridViewTextBoxColumn7.Width = 150;
			// 
			// dataGridViewCheckBoxColumn2
			// 
			this.dataGridViewCheckBoxColumn2.DataPropertyName = "IsRunning";
			this.dataGridViewCheckBoxColumn2.Frozen = true;
			this.dataGridViewCheckBoxColumn2.HeaderText = "R.";
			this.dataGridViewCheckBoxColumn2.Name = "dataGridViewCheckBoxColumn2";
			this.dataGridViewCheckBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridViewCheckBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.dataGridViewCheckBoxColumn2.ToolTipText = "Running";
			this.dataGridViewCheckBoxColumn2.Width = 20;
			// 
			// dataGridViewTextBoxColumn8
			// 
			this.dataGridViewTextBoxColumn8.DataPropertyName = "StartedCount";
			this.dataGridViewTextBoxColumn8.HeaderText = "Count";
			this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
			// 
			// dataGridViewTextBoxColumn9
			// 
			this.dataGridViewTextBoxColumn9.DataPropertyName = "NextRunningDate";
			dataGridViewCellStyle3.Format = "G";
			dataGridViewCellStyle3.NullValue = null;
			this.dataGridViewTextBoxColumn9.DefaultCellStyle = dataGridViewCellStyle3;
			this.dataGridViewTextBoxColumn9.HeaderText = "NRD";
			this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
			this.dataGridViewTextBoxColumn9.ReadOnly = true;
			this.dataGridViewTextBoxColumn9.Width = 120;
			// 
			// dataGridViewTextBoxColumn10
			// 
			this.dataGridViewTextBoxColumn10.DataPropertyName = "Period";
			this.dataGridViewTextBoxColumn10.HeaderText = "Period";
			this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
			// 
			// dataGridViewTextBoxColumn11
			// 
			this.dataGridViewTextBoxColumn11.DataPropertyName = "Interval";
			this.dataGridViewTextBoxColumn11.HeaderText = "Interval";
			this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
			// 
			// dataGridViewTextBoxColumn12
			// 
			this.dataGridViewTextBoxColumn12.DataPropertyName = "StartDay";
			this.dataGridViewTextBoxColumn12.HeaderText = "Day";
			this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
			// 
			// dataGridViewTextBoxColumn13
			// 
			this.dataGridViewTextBoxColumn13.DataPropertyName = "StartHour";
			this.dataGridViewTextBoxColumn13.HeaderText = "Hour";
			this.dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
			// 
			// dataGridViewTextBoxColumn14
			// 
			this.dataGridViewTextBoxColumn14.DataPropertyName = "StartMinute";
			this.dataGridViewTextBoxColumn14.HeaderText = "Minute";
			this.dataGridViewTextBoxColumn14.Name = "dataGridViewTextBoxColumn14";
			// 
			// dataGridViewCheckBoxColumn3
			// 
			this.dataGridViewCheckBoxColumn3.DataPropertyName = "IsParallelizable";
			this.dataGridViewCheckBoxColumn3.HeaderText = "Paralellizable";
			this.dataGridViewCheckBoxColumn3.Name = "dataGridViewCheckBoxColumn3";
			this.dataGridViewCheckBoxColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridViewCheckBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			// 
			// MonitorView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1002, 504);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.toolStrip1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "MonitorView";
			this.Text = "LogR Monitor";
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.uxLogDataGridView)).EndInit();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.uxLogBindingSource)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.DataGridView uxLogDataGridView;
		private System.Windows.Forms.RichTextBox uxStackTextBox;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton uxConfigurationButton;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton uxScrollingButton;
		private System.Windows.Forms.ToolStripButton uxClearButton;
		private System.Windows.Forms.BindingSource uxLogBindingSource;
		private System.Windows.Forms.RichTextBox uxNotificationRichTextBox;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripButton toolStripButton1;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
		private Forms.DataGridViewAutoFilterTextBoxColumn dataGridViewAutoFilterTextBoxColumn1;
		private Forms.DataGridViewAutoFilterTextBoxColumn dataGridViewAutoFilterTextBoxColumn2;
		private Forms.DataGridViewAutoFilterTextBoxColumn dataGridViewAutoFilterTextBoxColumn3;
		private Forms.DataGridViewAutoFilterTextBoxColumn dataGridViewAutoFilterTextBoxColumn4;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
		private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
		private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn2;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn14;
		private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn3;
		private System.Windows.Forms.ToolStripButton uxExceptionFilterButton;
		private System.Windows.Forms.ToolStripButton uxNotificationFilterButton;
		private System.Windows.Forms.ToolStripButton uxDebugFilterButton;
		private System.Windows.Forms.ToolStripButton uxWarningFilterButton;
		private System.Windows.Forms.DataGridViewTextBoxColumn uxCreationDateColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn uxMachineNameColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn uxHosterNameColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn uxApplicationNameColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn uxCategoryColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn uxMessageColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn uxExceptionStack;
	}
}