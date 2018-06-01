namespace tnxqsoClient
{
    partial class FSettings
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.bOK = new System.Windows.Forms.Button();
            this.bCancel = new System.Windows.Forms.Button();
            this.cbGPSPort = new System.Windows.Forms.ComboBox();
            this.gbGPS = new System.Windows.Forms.GroupBox();
            this.rbGPSWirelessGW = new System.Windows.Forms.RadioButton();
            this.rbGPSSerial = new System.Windows.Forms.RadioButton();
            this.dgvOptionalColumns = new System.Windows.Forms.DataGridView();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Show = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvUserColumns = new System.Windows.Forms.DataGridView();
            this.ucName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ucShow = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ucValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gbGPS.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOptionalColumns)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUserColumns)).BeginInit();
            this.SuspendLayout();
            // 
            // bOK
            // 
            this.bOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.bOK.Location = new System.Drawing.Point(269, 462);
            this.bOK.Name = "bOK";
            this.bOK.Size = new System.Drawing.Size(75, 30);
            this.bOK.TabIndex = 0;
            this.bOK.Text = "OK";
            this.bOK.UseVisualStyleBackColor = true;
            // 
            // bCancel
            // 
            this.bCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bCancel.Location = new System.Drawing.Point(350, 462);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(75, 30);
            this.bCancel.TabIndex = 1;
            this.bCancel.Text = "Cancel";
            this.bCancel.UseVisualStyleBackColor = true;
            // 
            // cbGPSPort
            // 
            this.cbGPSPort.FormattingEnabled = true;
            this.cbGPSPort.Location = new System.Drawing.Point(108, 21);
            this.cbGPSPort.Name = "cbGPSPort";
            this.cbGPSPort.Size = new System.Drawing.Size(302, 28);
            this.cbGPSPort.TabIndex = 8;
            // 
            // gbGPS
            // 
            this.gbGPS.Controls.Add(this.rbGPSWirelessGW);
            this.gbGPS.Controls.Add(this.cbGPSPort);
            this.gbGPS.Controls.Add(this.rbGPSSerial);
            this.gbGPS.Location = new System.Drawing.Point(9, 6);
            this.gbGPS.Name = "gbGPS";
            this.gbGPS.Size = new System.Drawing.Size(416, 88);
            this.gbGPS.TabIndex = 10;
            this.gbGPS.TabStop = false;
            this.gbGPS.Text = "GPS Settings";
            // 
            // rbGPSWirelessGW
            // 
            this.rbGPSWirelessGW.AutoSize = true;
            this.rbGPSWirelessGW.Location = new System.Drawing.Point(6, 56);
            this.rbGPSWirelessGW.Name = "rbGPSWirelessGW";
            this.rbGPSWirelessGW.Size = new System.Drawing.Size(154, 24);
            this.rbGPSWirelessGW.TabIndex = 1;
            this.rbGPSWirelessGW.TabStop = true;
            this.rbGPSWirelessGW.Text = "Wireless Gateway";
            this.rbGPSWirelessGW.UseVisualStyleBackColor = true;
            this.rbGPSWirelessGW.Click += new System.EventHandler(this.rbGPSSource_Click);
            // 
            // rbGPSSerial
            // 
            this.rbGPSSerial.AutoSize = true;
            this.rbGPSSerial.Location = new System.Drawing.Point(6, 22);
            this.rbGPSSerial.Name = "rbGPSSerial";
            this.rbGPSSerial.Size = new System.Drawing.Size(99, 24);
            this.rbGPSSerial.TabIndex = 0;
            this.rbGPSSerial.TabStop = true;
            this.rbGPSSerial.Text = "Serial port";
            this.rbGPSSerial.UseVisualStyleBackColor = true;
            this.rbGPSSerial.Click += new System.EventHandler(this.rbGPSSource_Click);
            // 
            // dgvOptionalColumns
            // 
            this.dgvOptionalColumns.AllowUserToAddRows = false;
            this.dgvOptionalColumns.AllowUserToDeleteRows = false;
            this.dgvOptionalColumns.AllowUserToResizeColumns = false;
            this.dgvOptionalColumns.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOptionalColumns.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.name,
            this.Show,
            this.Value});
            this.dgvOptionalColumns.Location = new System.Drawing.Point(9, 125);
            this.dgvOptionalColumns.Name = "dgvOptionalColumns";
            this.dgvOptionalColumns.Size = new System.Drawing.Size(416, 150);
            this.dgvOptionalColumns.TabIndex = 9;
            this.dgvOptionalColumns.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvColumns_CellContentClick);
            // 
            // name
            // 
            this.name.DataPropertyName = "name";
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.name.DefaultCellStyle = dataGridViewCellStyle5;
            this.name.HeaderText = "";
            this.name.Name = "name";
            this.name.ReadOnly = true;
            // 
            // Show
            // 
            this.Show.DataPropertyName = "show";
            this.Show.HeaderText = "Show";
            this.Show.Name = "Show";
            // 
            // Value
            // 
            this.Value.DataPropertyName = "value";
            this.Value.HeaderText = "Value";
            this.Value.Name = "Value";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 102);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 20);
            this.label1.TabIndex = 11;
            this.label1.Text = "Optional columns";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 290);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 20);
            this.label2.TabIndex = 12;
            this.label2.Text = "User columns";
            // 
            // dgvUserColumns
            // 
            this.dgvUserColumns.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUserColumns.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ucName,
            this.ucShow,
            this.ucValue});
            this.dgvUserColumns.Location = new System.Drawing.Point(9, 313);
            this.dgvUserColumns.Name = "dgvUserColumns";
            this.dgvUserColumns.Size = new System.Drawing.Size(416, 143);
            this.dgvUserColumns.TabIndex = 13;
            this.dgvUserColumns.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dgvUserColumns_RowsAdded);
            this.dgvUserColumns.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dgvUserColumns_RowsRemoved);
            this.dgvUserColumns.RowValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvUserColumns_RowValidated);
            // 
            // ucName
            // 
            this.ucName.DataPropertyName = "name";
            this.ucName.HeaderText = "Name";
            this.ucName.Name = "ucName";
            // 
            // ucShow
            // 
            this.ucShow.DataPropertyName = "show";
            this.ucShow.HeaderText = "Show";
            this.ucShow.Name = "ucShow";
            this.ucShow.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ucShow.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // ucValue
            // 
            this.ucValue.DataPropertyName = "value";
            this.ucValue.HeaderText = "Value";
            this.ucValue.Name = "ucValue";
            // 
            // FSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(437, 503);
            this.Controls.Add(this.dgvUserColumns);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvOptionalColumns);
            this.Controls.Add(this.gbGPS);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.bOK);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FSettings";
            this.Text = "Settings";
            this.gbGPS.ResumeLayout(false);
            this.gbGPS.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOptionalColumns)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUserColumns)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bOK;
        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.ComboBox cbGPSPort;
        private System.Windows.Forms.GroupBox gbGPS;
        private System.Windows.Forms.RadioButton rbGPSWirelessGW;
        private System.Windows.Forms.RadioButton rbGPSSerial;
        private System.Windows.Forms.DataGridView dgvOptionalColumns;
        private System.Windows.Forms.DataGridViewTextBoxColumn ocColumnName;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ocColumnShow;
        private System.Windows.Forms.DataGridViewTextBoxColumn ocColumnValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Show;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvUserColumns;
        private System.Windows.Forms.DataGridViewTextBoxColumn ucName;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ucShow;
        private System.Windows.Forms.DataGridViewTextBoxColumn ucValue;
    }
}