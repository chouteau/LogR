namespace LogWinRMonitor.Views
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
			components = new System.ComponentModel.Container();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MonitorView));
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
			splitContainer1 = new System.Windows.Forms.SplitContainer();
			uxLogDataGridView = new System.Windows.Forms.DataGridView();
			uxCreationDateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			uxCategoryColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			uxApplicationNameColumn = new Forms.DataGridViewAutoFilterTextBoxColumn();
			uxContextColumn = new Forms.DataGridViewAutoFilterTextBoxColumn();
			uxMessageColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			uxMachineNameColumn = new Forms.DataGridViewAutoFilterTextBoxColumn();
			uxHosterNameColumn = new Forms.DataGridViewAutoFilterTextBoxColumn();
			Environnement = new System.Windows.Forms.DataGridViewTextBoxColumn();
			uxStackTextBox = new System.Windows.Forms.RichTextBox();
			toolStrip1 = new System.Windows.Forms.ToolStrip();
			uxConfigurationButton = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			uxScrollingButton = new System.Windows.Forms.ToolStripButton();
			uxStopStartLogToolStripButton = new System.Windows.Forms.ToolStripButton();
			uxClearButton = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			uxFatalFilterButton = new System.Windows.Forms.ToolStripButton();
			uxExceptionFilterButton = new System.Windows.Forms.ToolStripButton();
			uxWarningFilterButton = new System.Windows.Forms.ToolStripButton();
			uxInfoFilterButton = new System.Windows.Forms.ToolStripButton();
			uxDebugFilterButton = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			uxSearchToolStripTextBox = new System.Windows.Forms.ToolStripTextBox();
			uxSearchToolStripButton = new System.Windows.Forms.ToolStripButton();
			uxTraceFilterButton = new System.Windows.Forms.ToolStripButton();
			tabControl1 = new System.Windows.Forms.TabControl();
			tabPage1 = new System.Windows.Forms.TabPage();
			tabPage3 = new System.Windows.Forms.TabPage();
			uxNotificationRichTextBox = new System.Windows.Forms.RichTextBox();
			uxLogBindingSource = new System.Windows.Forms.BindingSource(components);
			dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			dataGridViewAutoFilterTextBoxColumn1 = new Forms.DataGridViewAutoFilterTextBoxColumn();
			dataGridViewAutoFilterTextBoxColumn2 = new Forms.DataGridViewAutoFilterTextBoxColumn();
			dataGridViewAutoFilterTextBoxColumn3 = new Forms.DataGridViewAutoFilterTextBoxColumn();
			dataGridViewAutoFilterTextBoxColumn4 = new Forms.DataGridViewAutoFilterTextBoxColumn();
			dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			dataGridViewCheckBoxColumn2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			dataGridViewTextBoxColumn13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			dataGridViewTextBoxColumn14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			dataGridViewCheckBoxColumn3 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
			splitContainer1.Panel1.SuspendLayout();
			splitContainer1.Panel2.SuspendLayout();
			splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)uxLogDataGridView).BeginInit();
			toolStrip1.SuspendLayout();
			tabControl1.SuspendLayout();
			tabPage1.SuspendLayout();
			tabPage3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)uxLogBindingSource).BeginInit();
			SuspendLayout();
			// 
			// splitContainer1
			// 
			splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			splitContainer1.Location = new System.Drawing.Point(5, 4);
			splitContainer1.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
			splitContainer1.Name = "splitContainer1";
			splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			splitContainer1.Panel1.Controls.Add(uxLogDataGridView);
			// 
			// splitContainer1.Panel2
			// 
			splitContainer1.Panel2.Controls.Add(uxStackTextBox);
			splitContainer1.Size = new System.Drawing.Size(1559, 773);
			splitContainer1.SplitterDistance = 462;
			splitContainer1.SplitterWidth = 7;
			splitContainer1.TabIndex = 12;
			// 
			// uxLogDataGridView
			// 
			uxLogDataGridView.AllowUserToAddRows = false;
			uxLogDataGridView.AllowUserToDeleteRows = false;
			uxLogDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			uxLogDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { uxCreationDateColumn, uxCategoryColumn, uxApplicationNameColumn, uxContextColumn, uxMessageColumn, uxMachineNameColumn, uxHosterNameColumn, Environnement });
			uxLogDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
			uxLogDataGridView.Location = new System.Drawing.Point(0, 0);
			uxLogDataGridView.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
			uxLogDataGridView.Name = "uxLogDataGridView";
			uxLogDataGridView.RowHeadersWidth = 51;
			uxLogDataGridView.Size = new System.Drawing.Size(1559, 462);
			uxLogDataGridView.TabIndex = 9;
			// 
			// uxCreationDateColumn
			// 
			uxCreationDateColumn.DataPropertyName = "CreationDate";
			dataGridViewCellStyle4.Format = "T";
			dataGridViewCellStyle4.NullValue = null;
			uxCreationDateColumn.DefaultCellStyle = dataGridViewCellStyle4;
			uxCreationDateColumn.HeaderText = "Date";
			uxCreationDateColumn.MinimumWidth = 6;
			uxCreationDateColumn.Name = "uxCreationDateColumn";
			uxCreationDateColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			uxCreationDateColumn.Width = 80;
			// 
			// uxCategoryColumn
			// 
			uxCategoryColumn.DataPropertyName = "Category";
			uxCategoryColumn.HeaderText = "Cat.";
			uxCategoryColumn.MinimumWidth = 6;
			uxCategoryColumn.Name = "uxCategoryColumn";
			uxCategoryColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			uxCategoryColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
			uxCategoryColumn.Width = 70;
			// 
			// uxApplicationNameColumn
			// 
			uxApplicationNameColumn.DataPropertyName = "ApplicationName";
			uxApplicationNameColumn.HeaderText = "Application";
			uxApplicationNameColumn.MinimumWidth = 6;
			uxApplicationNameColumn.Name = "uxApplicationNameColumn";
			uxApplicationNameColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			uxApplicationNameColumn.Width = 300;
			// 
			// uxContextColumn
			// 
			uxContextColumn.DataPropertyName = "Context";
			uxContextColumn.HeaderText = "Context";
			uxContextColumn.MinimumWidth = 6;
			uxContextColumn.Name = "uxContextColumn";
			uxContextColumn.ReadOnly = true;
			uxContextColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			uxContextColumn.Width = 300;
			// 
			// uxMessageColumn
			// 
			uxMessageColumn.DataPropertyName = "Message";
			uxMessageColumn.HeaderText = "Message";
			uxMessageColumn.MinimumWidth = 6;
			uxMessageColumn.Name = "uxMessageColumn";
			uxMessageColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			uxMessageColumn.Width = 500;
			// 
			// uxMachineNameColumn
			// 
			uxMachineNameColumn.DataPropertyName = "MachineName";
			uxMachineNameColumn.FillWeight = 110F;
			uxMachineNameColumn.HeaderText = "Machine";
			uxMachineNameColumn.MinimumWidth = 6;
			uxMachineNameColumn.Name = "uxMachineNameColumn";
			uxMachineNameColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			uxMachineNameColumn.Width = 125;
			// 
			// uxHosterNameColumn
			// 
			uxHosterNameColumn.DataPropertyName = "HostName";
			uxHosterNameColumn.FillWeight = 120F;
			uxHosterNameColumn.HeaderText = "Host";
			uxHosterNameColumn.MinimumWidth = 6;
			uxHosterNameColumn.Name = "uxHosterNameColumn";
			uxHosterNameColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			uxHosterNameColumn.Width = 110;
			// 
			// Environnement
			// 
			Environnement.DataPropertyName = "EnvironmentName";
			Environnement.HeaderText = "Environment";
			Environnement.MinimumWidth = 6;
			Environnement.Name = "Environnement";
			Environnement.Width = 125;
			// 
			// uxStackTextBox
			// 
			uxStackTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			uxStackTextBox.Location = new System.Drawing.Point(0, 0);
			uxStackTextBox.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
			uxStackTextBox.Name = "uxStackTextBox";
			uxStackTextBox.Size = new System.Drawing.Size(1559, 304);
			uxStackTextBox.TabIndex = 15;
			uxStackTextBox.Text = "";
			// 
			// toolStrip1
			// 
			toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { uxConfigurationButton, toolStripSeparator1, uxScrollingButton, uxStopStartLogToolStripButton, uxClearButton, toolStripSeparator2, toolStripButton1, uxFatalFilterButton, uxExceptionFilterButton, uxWarningFilterButton, uxInfoFilterButton, uxDebugFilterButton, toolStripSeparator3, uxSearchToolStripTextBox, uxSearchToolStripButton, uxTraceFilterButton });
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(1577, 27);
			toolStrip1.TabIndex = 14;
			toolStrip1.Text = "toolStrip1";
			// 
			// uxConfigurationButton
			// 
			uxConfigurationButton.Image = (System.Drawing.Image)resources.GetObject("uxConfigurationButton.Image");
			uxConfigurationButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			uxConfigurationButton.Name = "uxConfigurationButton";
			uxConfigurationButton.Size = new System.Drawing.Size(124, 24);
			uxConfigurationButton.Text = "Configuration";
			uxConfigurationButton.Click += StartConfigurationClick;
			// 
			// toolStripSeparator1
			// 
			toolStripSeparator1.Name = "toolStripSeparator1";
			toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
			// 
			// uxScrollingButton
			// 
			uxScrollingButton.Image = (System.Drawing.Image)resources.GetObject("uxScrollingButton.Image");
			uxScrollingButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			uxScrollingButton.Name = "uxScrollingButton";
			uxScrollingButton.Size = new System.Drawing.Size(124, 24);
			uxScrollingButton.Text = "Stop scrolling";
			// 
			// uxStopStartLogToolStripButton
			// 
			uxStopStartLogToolStripButton.Image = (System.Drawing.Image)resources.GetObject("uxStopStartLogToolStripButton.Image");
			uxStopStartLogToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			uxStopStartLogToolStripButton.Name = "uxStopStartLogToolStripButton";
			uxStopStartLogToolStripButton.Size = new System.Drawing.Size(96, 24);
			uxStopStartLogToolStripButton.Text = "Stop logs";
			// 
			// uxClearButton
			// 
			uxClearButton.Image = (System.Drawing.Image)resources.GetObject("uxClearButton.Image");
			uxClearButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			uxClearButton.Name = "uxClearButton";
			uxClearButton.Size = new System.Drawing.Size(67, 24);
			uxClearButton.Text = "Clear";
			// 
			// toolStripSeparator2
			// 
			toolStripSeparator2.Name = "toolStripSeparator2";
			toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
			// 
			// toolStripButton1
			// 
			toolStripButton1.Image = (System.Drawing.Image)resources.GetObject("toolStripButton1.Image");
			toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButton1.Name = "toolStripButton1";
			toolStripButton1.Size = new System.Drawing.Size(64, 24);
			toolStripButton1.Text = "Save";
			// 
			// uxFatalFilterButton
			// 
			uxFatalFilterButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			uxFatalFilterButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			uxFatalFilterButton.Image = (System.Drawing.Image)resources.GetObject("uxFatalFilterButton.Image");
			uxFatalFilterButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			uxFatalFilterButton.Name = "uxFatalFilterButton";
			uxFatalFilterButton.Size = new System.Drawing.Size(44, 24);
			uxFatalFilterButton.Text = "Fatal";
			// 
			// uxExceptionFilterButton
			// 
			uxExceptionFilterButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			uxExceptionFilterButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			uxExceptionFilterButton.Image = (System.Drawing.Image)resources.GetObject("uxExceptionFilterButton.Image");
			uxExceptionFilterButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			uxExceptionFilterButton.Name = "uxExceptionFilterButton";
			uxExceptionFilterButton.Size = new System.Drawing.Size(78, 24);
			uxExceptionFilterButton.Text = "Exception";
			// 
			// uxWarningFilterButton
			// 
			uxWarningFilterButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			uxWarningFilterButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			uxWarningFilterButton.Image = (System.Drawing.Image)resources.GetObject("uxWarningFilterButton.Image");
			uxWarningFilterButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			uxWarningFilterButton.Name = "uxWarningFilterButton";
			uxWarningFilterButton.Size = new System.Drawing.Size(68, 24);
			uxWarningFilterButton.Text = "Warning";
			// 
			// uxInfoFilterButton
			// 
			uxInfoFilterButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			uxInfoFilterButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			uxInfoFilterButton.Image = (System.Drawing.Image)resources.GetObject("uxInfoFilterButton.Image");
			uxInfoFilterButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			uxInfoFilterButton.Name = "uxInfoFilterButton";
			uxInfoFilterButton.Size = new System.Drawing.Size(92, 24);
			uxInfoFilterButton.Text = "Notification";
			// 
			// uxDebugFilterButton
			// 
			uxDebugFilterButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			uxDebugFilterButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			uxDebugFilterButton.Image = (System.Drawing.Image)resources.GetObject("uxDebugFilterButton.Image");
			uxDebugFilterButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			uxDebugFilterButton.Name = "uxDebugFilterButton";
			uxDebugFilterButton.Size = new System.Drawing.Size(58, 24);
			uxDebugFilterButton.Text = "Debug";
			// 
			// toolStripSeparator3
			// 
			toolStripSeparator3.Name = "toolStripSeparator3";
			toolStripSeparator3.Size = new System.Drawing.Size(6, 27);
			// 
			// uxSearchToolStripTextBox
			// 
			uxSearchToolStripTextBox.Name = "uxSearchToolStripTextBox";
			uxSearchToolStripTextBox.Size = new System.Drawing.Size(198, 27);
			// 
			// uxSearchToolStripButton
			// 
			uxSearchToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			uxSearchToolStripButton.Image = (System.Drawing.Image)resources.GetObject("uxSearchToolStripButton.Image");
			uxSearchToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			uxSearchToolStripButton.Name = "uxSearchToolStripButton";
			uxSearchToolStripButton.Size = new System.Drawing.Size(29, 24);
			uxSearchToolStripButton.Text = "Search";
			// 
			// uxTraceFilterButton
			// 
			uxTraceFilterButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			uxTraceFilterButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			uxTraceFilterButton.Image = (System.Drawing.Image)resources.GetObject("uxTraceFilterButton.Image");
			uxTraceFilterButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			uxTraceFilterButton.Name = "uxTraceFilterButton";
			uxTraceFilterButton.Size = new System.Drawing.Size(48, 24);
			uxTraceFilterButton.Text = "Trace";
			// 
			// tabControl1
			// 
			tabControl1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			tabControl1.Controls.Add(tabPage1);
			tabControl1.Controls.Add(tabPage3);
			tabControl1.Location = new System.Drawing.Point(0, 31);
			tabControl1.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
			tabControl1.Name = "tabControl1";
			tabControl1.SelectedIndex = 0;
			tabControl1.Size = new System.Drawing.Size(1577, 814);
			tabControl1.TabIndex = 15;
			// 
			// tabPage1
			// 
			tabPage1.Controls.Add(splitContainer1);
			tabPage1.Location = new System.Drawing.Point(4, 29);
			tabPage1.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
			tabPage1.Name = "tabPage1";
			tabPage1.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
			tabPage1.Size = new System.Drawing.Size(1569, 781);
			tabPage1.TabIndex = 0;
			tabPage1.Text = "Logs";
			tabPage1.UseVisualStyleBackColor = true;
			// 
			// tabPage3
			// 
			tabPage3.Controls.Add(uxNotificationRichTextBox);
			tabPage3.Location = new System.Drawing.Point(4, 29);
			tabPage3.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
			tabPage3.Name = "tabPage3";
			tabPage3.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
			tabPage3.Size = new System.Drawing.Size(1569, 781);
			tabPage3.TabIndex = 2;
			tabPage3.Text = "Notifications";
			tabPage3.UseVisualStyleBackColor = true;
			// 
			// uxNotificationRichTextBox
			// 
			uxNotificationRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			uxNotificationRichTextBox.Location = new System.Drawing.Point(5, 4);
			uxNotificationRichTextBox.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
			uxNotificationRichTextBox.Name = "uxNotificationRichTextBox";
			uxNotificationRichTextBox.Size = new System.Drawing.Size(1559, 773);
			uxNotificationRichTextBox.TabIndex = 0;
			uxNotificationRichTextBox.Text = "";
			// 
			// dataGridViewTextBoxColumn1
			// 
			dataGridViewTextBoxColumn1.DataPropertyName = "CreationDate";
			dataGridViewCellStyle5.Format = "T";
			dataGridViewCellStyle5.NullValue = null;
			dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle5;
			dataGridViewTextBoxColumn1.HeaderText = "Date";
			dataGridViewTextBoxColumn1.MinimumWidth = 6;
			dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
			dataGridViewTextBoxColumn1.Width = 70;
			// 
			// dataGridViewTextBoxColumn2
			// 
			dataGridViewTextBoxColumn2.DataPropertyName = "Message";
			dataGridViewTextBoxColumn2.FillWeight = 110F;
			dataGridViewTextBoxColumn2.HeaderText = "Message";
			dataGridViewTextBoxColumn2.MinimumWidth = 6;
			dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
			dataGridViewTextBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
			dataGridViewTextBoxColumn2.Width = 400;
			// 
			// dataGridViewTextBoxColumn3
			// 
			dataGridViewTextBoxColumn3.DataPropertyName = "ExceptionStack";
			dataGridViewTextBoxColumn3.FillWeight = 120F;
			dataGridViewTextBoxColumn3.HeaderText = "Stack";
			dataGridViewTextBoxColumn3.MinimumWidth = 6;
			dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
			dataGridViewTextBoxColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			dataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
			dataGridViewTextBoxColumn3.Width = 200;
			// 
			// dataGridViewTextBoxColumn4
			// 
			dataGridViewTextBoxColumn4.DataPropertyName = "MachineName";
			dataGridViewTextBoxColumn4.Frozen = true;
			dataGridViewTextBoxColumn4.HeaderText = "Machine";
			dataGridViewTextBoxColumn4.MinimumWidth = 6;
			dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
			dataGridViewTextBoxColumn4.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			dataGridViewTextBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
			dataGridViewTextBoxColumn4.Width = 70;
			// 
			// dataGridViewTextBoxColumn5
			// 
			dataGridViewTextBoxColumn5.DataPropertyName = "HostName";
			dataGridViewTextBoxColumn5.Frozen = true;
			dataGridViewTextBoxColumn5.HeaderText = "Host";
			dataGridViewTextBoxColumn5.MinimumWidth = 6;
			dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
			dataGridViewTextBoxColumn5.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			dataGridViewTextBoxColumn5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
			dataGridViewTextBoxColumn5.Width = 70;
			// 
			// dataGridViewTextBoxColumn6
			// 
			dataGridViewTextBoxColumn6.DataPropertyName = "ApplicationName";
			dataGridViewTextBoxColumn6.Frozen = true;
			dataGridViewTextBoxColumn6.HeaderText = "Application";
			dataGridViewTextBoxColumn6.MinimumWidth = 6;
			dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
			dataGridViewTextBoxColumn6.Width = 60;
			// 
			// dataGridViewTextBoxColumn7
			// 
			dataGridViewTextBoxColumn7.DataPropertyName = "Name";
			dataGridViewTextBoxColumn7.Frozen = true;
			dataGridViewTextBoxColumn7.HeaderText = "Task Name";
			dataGridViewTextBoxColumn7.MinimumWidth = 6;
			dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
			dataGridViewTextBoxColumn7.ReadOnly = true;
			dataGridViewTextBoxColumn7.Width = 150;
			// 
			// dataGridViewAutoFilterTextBoxColumn1
			// 
			dataGridViewAutoFilterTextBoxColumn1.DataPropertyName = "MachineName";
			dataGridViewAutoFilterTextBoxColumn1.HeaderText = "Machine";
			dataGridViewAutoFilterTextBoxColumn1.MinimumWidth = 6;
			dataGridViewAutoFilterTextBoxColumn1.Name = "dataGridViewAutoFilterTextBoxColumn1";
			dataGridViewAutoFilterTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			dataGridViewAutoFilterTextBoxColumn1.Width = 70;
			// 
			// dataGridViewAutoFilterTextBoxColumn2
			// 
			dataGridViewAutoFilterTextBoxColumn2.DataPropertyName = "HostName";
			dataGridViewAutoFilterTextBoxColumn2.HeaderText = "Host";
			dataGridViewAutoFilterTextBoxColumn2.MinimumWidth = 6;
			dataGridViewAutoFilterTextBoxColumn2.Name = "dataGridViewAutoFilterTextBoxColumn2";
			dataGridViewAutoFilterTextBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			dataGridViewAutoFilterTextBoxColumn2.Width = 70;
			// 
			// dataGridViewAutoFilterTextBoxColumn3
			// 
			dataGridViewAutoFilterTextBoxColumn3.DataPropertyName = "ApplicationName";
			dataGridViewAutoFilterTextBoxColumn3.HeaderText = "Application";
			dataGridViewAutoFilterTextBoxColumn3.MinimumWidth = 6;
			dataGridViewAutoFilterTextBoxColumn3.Name = "dataGridViewAutoFilterTextBoxColumn3";
			dataGridViewAutoFilterTextBoxColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			dataGridViewAutoFilterTextBoxColumn3.Width = 60;
			// 
			// dataGridViewAutoFilterTextBoxColumn4
			// 
			dataGridViewAutoFilterTextBoxColumn4.DataPropertyName = "Category";
			dataGridViewAutoFilterTextBoxColumn4.HeaderText = "Category";
			dataGridViewAutoFilterTextBoxColumn4.MinimumWidth = 6;
			dataGridViewAutoFilterTextBoxColumn4.Name = "dataGridViewAutoFilterTextBoxColumn4";
			dataGridViewAutoFilterTextBoxColumn4.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			dataGridViewAutoFilterTextBoxColumn4.Width = 60;
			// 
			// dataGridViewCheckBoxColumn1
			// 
			dataGridViewCheckBoxColumn1.DataPropertyName = "IsEnabled";
			dataGridViewCheckBoxColumn1.Frozen = true;
			dataGridViewCheckBoxColumn1.HeaderText = "E.";
			dataGridViewCheckBoxColumn1.MinimumWidth = 6;
			dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
			dataGridViewCheckBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			dataGridViewCheckBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			dataGridViewCheckBoxColumn1.ToolTipText = "Enable";
			dataGridViewCheckBoxColumn1.Width = 20;
			// 
			// dataGridViewCheckBoxColumn2
			// 
			dataGridViewCheckBoxColumn2.DataPropertyName = "IsRunning";
			dataGridViewCheckBoxColumn2.Frozen = true;
			dataGridViewCheckBoxColumn2.HeaderText = "R.";
			dataGridViewCheckBoxColumn2.MinimumWidth = 6;
			dataGridViewCheckBoxColumn2.Name = "dataGridViewCheckBoxColumn2";
			dataGridViewCheckBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			dataGridViewCheckBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			dataGridViewCheckBoxColumn2.ToolTipText = "Running";
			dataGridViewCheckBoxColumn2.Width = 20;
			// 
			// dataGridViewTextBoxColumn8
			// 
			dataGridViewTextBoxColumn8.DataPropertyName = "StartedCount";
			dataGridViewTextBoxColumn8.HeaderText = "Count";
			dataGridViewTextBoxColumn8.MinimumWidth = 6;
			dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
			dataGridViewTextBoxColumn8.Width = 125;
			// 
			// dataGridViewTextBoxColumn9
			// 
			dataGridViewTextBoxColumn9.DataPropertyName = "NextRunningDate";
			dataGridViewCellStyle6.Format = "G";
			dataGridViewCellStyle6.NullValue = null;
			dataGridViewTextBoxColumn9.DefaultCellStyle = dataGridViewCellStyle6;
			dataGridViewTextBoxColumn9.HeaderText = "NRD";
			dataGridViewTextBoxColumn9.MinimumWidth = 6;
			dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
			dataGridViewTextBoxColumn9.ReadOnly = true;
			dataGridViewTextBoxColumn9.Width = 120;
			// 
			// dataGridViewTextBoxColumn10
			// 
			dataGridViewTextBoxColumn10.DataPropertyName = "Period";
			dataGridViewTextBoxColumn10.HeaderText = "Period";
			dataGridViewTextBoxColumn10.MinimumWidth = 6;
			dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
			dataGridViewTextBoxColumn10.Width = 125;
			// 
			// dataGridViewTextBoxColumn11
			// 
			dataGridViewTextBoxColumn11.DataPropertyName = "Interval";
			dataGridViewTextBoxColumn11.HeaderText = "Interval";
			dataGridViewTextBoxColumn11.MinimumWidth = 6;
			dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
			dataGridViewTextBoxColumn11.Width = 125;
			// 
			// dataGridViewTextBoxColumn12
			// 
			dataGridViewTextBoxColumn12.DataPropertyName = "StartDay";
			dataGridViewTextBoxColumn12.HeaderText = "Day";
			dataGridViewTextBoxColumn12.MinimumWidth = 6;
			dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
			dataGridViewTextBoxColumn12.Width = 125;
			// 
			// dataGridViewTextBoxColumn13
			// 
			dataGridViewTextBoxColumn13.DataPropertyName = "StartHour";
			dataGridViewTextBoxColumn13.HeaderText = "Hour";
			dataGridViewTextBoxColumn13.MinimumWidth = 6;
			dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
			dataGridViewTextBoxColumn13.Width = 125;
			// 
			// dataGridViewTextBoxColumn14
			// 
			dataGridViewTextBoxColumn14.DataPropertyName = "StartMinute";
			dataGridViewTextBoxColumn14.HeaderText = "Minute";
			dataGridViewTextBoxColumn14.MinimumWidth = 6;
			dataGridViewTextBoxColumn14.Name = "dataGridViewTextBoxColumn14";
			dataGridViewTextBoxColumn14.Width = 125;
			// 
			// dataGridViewCheckBoxColumn3
			// 
			dataGridViewCheckBoxColumn3.DataPropertyName = "IsParallelizable";
			dataGridViewCheckBoxColumn3.HeaderText = "Paralellizable";
			dataGridViewCheckBoxColumn3.MinimumWidth = 6;
			dataGridViewCheckBoxColumn3.Name = "dataGridViewCheckBoxColumn3";
			dataGridViewCheckBoxColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			dataGridViewCheckBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			dataGridViewCheckBoxColumn3.Width = 125;
			// 
			// MonitorView
			// 
			AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			ClientSize = new System.Drawing.Size(1577, 845);
			Controls.Add(tabControl1);
			Controls.Add(toolStrip1);
			Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
			Name = "MonitorView";
			Text = "LogRCore Monitor";
			splitContainer1.Panel1.ResumeLayout(false);
			splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
			splitContainer1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)uxLogDataGridView).EndInit();
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			tabControl1.ResumeLayout(false);
			tabPage1.ResumeLayout(false);
			tabPage3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)uxLogBindingSource).EndInit();
			ResumeLayout(false);
			PerformLayout();
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
		private System.Windows.Forms.ToolStripButton uxInfoFilterButton;
		private System.Windows.Forms.ToolStripButton uxDebugFilterButton;
		private System.Windows.Forms.ToolStripButton uxWarningFilterButton;
		private System.Windows.Forms.ToolStripButton uxFatalFilterButton;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripTextBox uxSearchToolStripTextBox;
		private System.Windows.Forms.ToolStripButton uxSearchToolStripButton;
		private System.Windows.Forms.ToolStripButton uxStopStartLogToolStripButton;
		private System.Windows.Forms.ToolStripButton uxTraceFilterButton;
		private System.Windows.Forms.DataGridViewTextBoxColumn uxCreationDateColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn uxCategoryColumn;
		private Forms.DataGridViewAutoFilterTextBoxColumn uxApplicationNameColumn;
		private Forms.DataGridViewAutoFilterTextBoxColumn uxContextColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn uxMessageColumn;
		private Forms.DataGridViewAutoFilterTextBoxColumn uxMachineNameColumn;
		private Forms.DataGridViewAutoFilterTextBoxColumn uxHosterNameColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn Environnement;
	}
}