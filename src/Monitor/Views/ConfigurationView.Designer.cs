namespace LogR.Monitor.Views
{
	partial class ConfigurationView
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
			this.uxSaveButton = new System.Windows.Forms.Button();
			this.uxCancelButton = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.uxDebugFilterCheckBox = new System.Windows.Forms.CheckBox();
			this.uxInfoFilterCheckBox = new System.Windows.Forms.CheckBox();
			this.uxWarningFilterCheckBox = new System.Windows.Forms.CheckBox();
			this.uxNotificationFilterCheckBox = new System.Windows.Forms.CheckBox();
			this.uxExceptionFilterCheckBox = new System.Windows.Forms.CheckBox();
			this.uxFatalFilterCheckBox = new System.Windows.Forms.CheckBox();
			this.uxSettingsBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.uxApplicationsDataGridView = new System.Windows.Forms.DataGridView();
			this.uxApplicationsBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.uxEnabledColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.uxApiUrlColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.uxApiKeyColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.uxSqlCheckBox = new System.Windows.Forms.CheckBox();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.uxSettingsBindingSource)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.uxApplicationsDataGridView)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.uxApplicationsBindingSource)).BeginInit();
			this.SuspendLayout();
			// 
			// uxSaveButton
			// 
			this.uxSaveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.uxSaveButton.CausesValidation = false;
			this.uxSaveButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.uxSaveButton.Location = new System.Drawing.Point(387, 218);
			this.uxSaveButton.Name = "uxSaveButton";
			this.uxSaveButton.Size = new System.Drawing.Size(75, 23);
			this.uxSaveButton.TabIndex = 2;
			this.uxSaveButton.Text = "Save";
			this.uxSaveButton.UseVisualStyleBackColor = true;
			// 
			// uxCancelButton
			// 
			this.uxCancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.uxCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.uxCancelButton.Location = new System.Drawing.Point(468, 218);
			this.uxCancelButton.Name = "uxCancelButton";
			this.uxCancelButton.Size = new System.Drawing.Size(75, 23);
			this.uxCancelButton.TabIndex = 3;
			this.uxCancelButton.Text = "Cancel";
			this.uxCancelButton.UseVisualStyleBackColor = true;
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.uxSqlCheckBox);
			this.groupBox1.Controls.Add(this.uxDebugFilterCheckBox);
			this.groupBox1.Controls.Add(this.uxInfoFilterCheckBox);
			this.groupBox1.Controls.Add(this.uxWarningFilterCheckBox);
			this.groupBox1.Controls.Add(this.uxNotificationFilterCheckBox);
			this.groupBox1.Controls.Add(this.uxExceptionFilterCheckBox);
			this.groupBox1.Controls.Add(this.uxFatalFilterCheckBox);
			this.groupBox1.Location = new System.Drawing.Point(12, 143);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(531, 69);
			this.groupBox1.TabIndex = 21;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Level";
			// 
			// uxDebugFilterCheckBox
			// 
			this.uxDebugFilterCheckBox.AutoSize = true;
			this.uxDebugFilterCheckBox.Location = new System.Drawing.Point(6, 19);
			this.uxDebugFilterCheckBox.Name = "uxDebugFilterCheckBox";
			this.uxDebugFilterCheckBox.Size = new System.Drawing.Size(58, 17);
			this.uxDebugFilterCheckBox.TabIndex = 0;
			this.uxDebugFilterCheckBox.Text = "Debug";
			this.uxDebugFilterCheckBox.UseVisualStyleBackColor = true;
			// 
			// uxInfoFilterCheckBox
			// 
			this.uxInfoFilterCheckBox.AutoSize = true;
			this.uxInfoFilterCheckBox.Checked = true;
			this.uxInfoFilterCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.uxInfoFilterCheckBox.Location = new System.Drawing.Point(6, 42);
			this.uxInfoFilterCheckBox.Name = "uxInfoFilterCheckBox";
			this.uxInfoFilterCheckBox.Size = new System.Drawing.Size(44, 17);
			this.uxInfoFilterCheckBox.TabIndex = 1;
			this.uxInfoFilterCheckBox.Text = "Info";
			this.uxInfoFilterCheckBox.UseVisualStyleBackColor = true;
			// 
			// uxWarningFilterCheckBox
			// 
			this.uxWarningFilterCheckBox.AutoSize = true;
			this.uxWarningFilterCheckBox.Checked = true;
			this.uxWarningFilterCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.uxWarningFilterCheckBox.Location = new System.Drawing.Point(213, 19);
			this.uxWarningFilterCheckBox.Name = "uxWarningFilterCheckBox";
			this.uxWarningFilterCheckBox.Size = new System.Drawing.Size(66, 17);
			this.uxWarningFilterCheckBox.TabIndex = 2;
			this.uxWarningFilterCheckBox.Text = "Warning";
			this.uxWarningFilterCheckBox.UseVisualStyleBackColor = true;
			// 
			// uxNotificationFilterCheckBox
			// 
			this.uxNotificationFilterCheckBox.AutoSize = true;
			this.uxNotificationFilterCheckBox.Checked = true;
			this.uxNotificationFilterCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.uxNotificationFilterCheckBox.Location = new System.Drawing.Point(213, 42);
			this.uxNotificationFilterCheckBox.Name = "uxNotificationFilterCheckBox";
			this.uxNotificationFilterCheckBox.Size = new System.Drawing.Size(79, 17);
			this.uxNotificationFilterCheckBox.TabIndex = 3;
			this.uxNotificationFilterCheckBox.Text = "Notification";
			this.uxNotificationFilterCheckBox.UseVisualStyleBackColor = true;
			// 
			// uxExceptionFilterCheckBox
			// 
			this.uxExceptionFilterCheckBox.AutoSize = true;
			this.uxExceptionFilterCheckBox.Checked = true;
			this.uxExceptionFilterCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.uxExceptionFilterCheckBox.Location = new System.Drawing.Point(332, 42);
			this.uxExceptionFilterCheckBox.Name = "uxExceptionFilterCheckBox";
			this.uxExceptionFilterCheckBox.Size = new System.Drawing.Size(73, 17);
			this.uxExceptionFilterCheckBox.TabIndex = 5;
			this.uxExceptionFilterCheckBox.Text = "Exception";
			this.uxExceptionFilterCheckBox.UseVisualStyleBackColor = true;
			// 
			// uxFatalFilterCheckBox
			// 
			this.uxFatalFilterCheckBox.AutoSize = true;
			this.uxFatalFilterCheckBox.Checked = true;
			this.uxFatalFilterCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.uxFatalFilterCheckBox.Location = new System.Drawing.Point(332, 19);
			this.uxFatalFilterCheckBox.Name = "uxFatalFilterCheckBox";
			this.uxFatalFilterCheckBox.Size = new System.Drawing.Size(49, 17);
			this.uxFatalFilterCheckBox.TabIndex = 4;
			this.uxFatalFilterCheckBox.Text = "Fatal";
			this.uxFatalFilterCheckBox.UseVisualStyleBackColor = true;
			// 
			// uxApplicationsDataGridView
			// 
			this.uxApplicationsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.uxApplicationsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.uxEnabledColumn,
            this.uxApiUrlColumn,
            this.uxApiKeyColumn});
			this.uxApplicationsDataGridView.Location = new System.Drawing.Point(13, 13);
			this.uxApplicationsDataGridView.Name = "uxApplicationsDataGridView";
			this.uxApplicationsDataGridView.Size = new System.Drawing.Size(530, 124);
			this.uxApplicationsDataGridView.TabIndex = 22;
			// 
			// dataGridViewCheckBoxColumn1
			// 
			this.dataGridViewCheckBoxColumn1.DataPropertyName = "Enabled";
			this.dataGridViewCheckBoxColumn1.HeaderText = "E.";
			this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
			this.dataGridViewCheckBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridViewCheckBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.dataGridViewCheckBoxColumn1.ToolTipText = "Enabled";
			this.dataGridViewCheckBoxColumn1.Width = 20;
			// 
			// dataGridViewTextBoxColumn1
			// 
			this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.dataGridViewTextBoxColumn1.DataPropertyName = "SignalRUrl";
			this.dataGridViewTextBoxColumn1.HeaderText = "Api Url";
			this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
			// 
			// dataGridViewTextBoxColumn2
			// 
			this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.dataGridViewTextBoxColumn2.DataPropertyName = "ApiKey";
			this.dataGridViewTextBoxColumn2.HeaderText = "Api Key";
			this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
			// 
			// uxEnabledColumn
			// 
			this.uxEnabledColumn.DataPropertyName = "Enabled";
			this.uxEnabledColumn.HeaderText = "E.";
			this.uxEnabledColumn.Name = "uxEnabledColumn";
			this.uxEnabledColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.uxEnabledColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.uxEnabledColumn.ToolTipText = "Enabled";
			this.uxEnabledColumn.Width = 20;
			// 
			// uxApiUrlColumn
			// 
			this.uxApiUrlColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.uxApiUrlColumn.DataPropertyName = "SignalRUrl";
			this.uxApiUrlColumn.HeaderText = "Api Url";
			this.uxApiUrlColumn.Name = "uxApiUrlColumn";
			// 
			// uxApiKeyColumn
			// 
			this.uxApiKeyColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.uxApiKeyColumn.DataPropertyName = "ApiKey";
			this.uxApiKeyColumn.HeaderText = "Api Key";
			this.uxApiKeyColumn.Name = "uxApiKeyColumn";
			// 
			// uxSqlCheckBox
			// 
			this.uxSqlCheckBox.AutoSize = true;
			this.uxSqlCheckBox.Location = new System.Drawing.Point(97, 19);
			this.uxSqlCheckBox.Name = "uxSqlCheckBox";
			this.uxSqlCheckBox.Size = new System.Drawing.Size(41, 17);
			this.uxSqlCheckBox.TabIndex = 0;
			this.uxSqlCheckBox.Text = "Sql";
			this.uxSqlCheckBox.UseVisualStyleBackColor = true;
			// 
			// ConfigurationView
			// 
			this.AcceptButton = this.uxSaveButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.uxCancelButton;
			this.ClientSize = new System.Drawing.Size(555, 253);
			this.Controls.Add(this.uxApplicationsDataGridView);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.uxCancelButton);
			this.Controls.Add(this.uxSaveButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "ConfigurationView";
			this.ShowInTaskbar = false;
			this.Text = "Configuration";
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.uxSettingsBindingSource)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.uxApplicationsDataGridView)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.uxApplicationsBindingSource)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button uxSaveButton;
		private System.Windows.Forms.Button uxCancelButton;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox uxDebugFilterCheckBox;
		private System.Windows.Forms.CheckBox uxInfoFilterCheckBox;
		private System.Windows.Forms.CheckBox uxWarningFilterCheckBox;
		private System.Windows.Forms.CheckBox uxNotificationFilterCheckBox;
		private System.Windows.Forms.CheckBox uxExceptionFilterCheckBox;
		private System.Windows.Forms.CheckBox uxFatalFilterCheckBox;
		private System.Windows.Forms.BindingSource uxSettingsBindingSource;
		private System.Windows.Forms.DataGridView uxApplicationsDataGridView;
		private System.Windows.Forms.BindingSource uxApplicationsBindingSource;
		private System.Windows.Forms.DataGridViewCheckBoxColumn uxEnabledColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn uxApiUrlColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn uxApiKeyColumn;
		private System.Windows.Forms.CheckBox uxSqlCheckBox;
		private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
	}
}