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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.bOK = new System.Windows.Forms.Button();
            this.bCancel = new System.Windows.Forms.Button();
            this.cbGPSPort = new System.Windows.Forms.ComboBox();
            this.gbGPS = new System.Windows.Forms.GroupBox();
            this.rbGPSWirelessGW = new System.Windows.Forms.RadioButton();
            this.rbGPSSerial = new System.Windows.Forms.RadioButton();
            this.dgvColumns = new System.Windows.Forms.DataGridView();
            this.Show = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.rbGPSServerLoad = new System.Windows.Forms.RadioButton();
            this.gbGPS.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvColumns)).BeginInit();
            this.SuspendLayout();
            // 
            // bOK
            // 
            this.bOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.bOK.Location = new System.Drawing.Point(407, 354);
            this.bOK.Name = "bOK";
            this.bOK.Size = new System.Drawing.Size(75, 30);
            this.bOK.TabIndex = 5;
            this.bOK.Text = "OK";
            this.bOK.UseVisualStyleBackColor = true;
            // 
            // bCancel
            // 
            this.bCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bCancel.Location = new System.Drawing.Point(488, 354);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(75, 30);
            this.bCancel.TabIndex = 6;
            this.bCancel.Text = "Cancel";
            this.bCancel.UseVisualStyleBackColor = true;
            // 
            // cbGPSPort
            // 
            this.cbGPSPort.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbGPSPort.FormattingEnabled = true;
            this.cbGPSPort.Location = new System.Drawing.Point(108, 21);
            this.cbGPSPort.Name = "cbGPSPort";
            this.cbGPSPort.Size = new System.Drawing.Size(438, 28);
            this.cbGPSPort.TabIndex = 2;
            // 
            // gbGPS
            // 
            this.gbGPS.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbGPS.Controls.Add(this.rbGPSServerLoad);
            this.gbGPS.Controls.Add(this.rbGPSWirelessGW);
            this.gbGPS.Controls.Add(this.cbGPSPort);
            this.gbGPS.Controls.Add(this.rbGPSSerial);
            this.gbGPS.Location = new System.Drawing.Point(9, 6);
            this.gbGPS.Name = "gbGPS";
            this.gbGPS.Size = new System.Drawing.Size(555, 120);
            this.gbGPS.TabIndex = 0;
            this.gbGPS.TabStop = false;
            this.gbGPS.Text = "GPS settings";
            // 
            // rbGPSWirelessGW
            // 
            this.rbGPSWirelessGW.AutoSize = true;
            this.rbGPSWirelessGW.Location = new System.Drawing.Point(6, 56);
            this.rbGPSWirelessGW.Name = "rbGPSWirelessGW";
            this.rbGPSWirelessGW.Size = new System.Drawing.Size(104, 24);
            this.rbGPSWirelessGW.TabIndex = 3;
            this.rbGPSWirelessGW.TabStop = true;
            this.rbGPSWirelessGW.Text = "ShareGPS";
            this.rbGPSWirelessGW.UseVisualStyleBackColor = true;
            this.rbGPSWirelessGW.Click += new System.EventHandler(this.rbGPSSource_Click);
            // 
            // rbGPSSerial
            // 
            this.rbGPSSerial.AutoSize = true;
            this.rbGPSSerial.Location = new System.Drawing.Point(6, 22);
            this.rbGPSSerial.Name = "rbGPSSerial";
            this.rbGPSSerial.Size = new System.Drawing.Size(99, 24);
            this.rbGPSSerial.TabIndex = 1;
            this.rbGPSSerial.TabStop = true;
            this.rbGPSSerial.Text = "Serial port";
            this.rbGPSSerial.UseVisualStyleBackColor = true;
            this.rbGPSSerial.Click += new System.EventHandler(this.rbGPSSource_Click);
            // 
            // dgvColumns
            // 
            this.dgvColumns.AllowUserToAddRows = false;
            this.dgvColumns.AllowUserToDeleteRows = false;
            this.dgvColumns.AllowUserToResizeColumns = false;
            this.dgvColumns.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvColumns.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvColumns.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvColumns.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Show,
            this.name,
            this.Value});
            this.dgvColumns.Location = new System.Drawing.Point(9, 157);
            this.dgvColumns.Name = "dgvColumns";
            this.dgvColumns.RowHeadersVisible = false;
            this.dgvColumns.Size = new System.Drawing.Size(554, 193);
            this.dgvColumns.TabIndex = 4;
            this.dgvColumns.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvColumns_DataBindingComplete);
            // 
            // Show
            // 
            this.Show.DataPropertyName = "show";
            this.Show.HeaderText = "Show";
            this.Show.Name = "Show";
            this.Show.Width = 50;
            // 
            // name
            // 
            this.name.DataPropertyName = "name";
            this.name.HeaderText = "Column";
            this.name.Name = "name";
            this.name.Width = 150;
            // 
            // Value
            // 
            this.Value.DataPropertyName = "value";
            this.Value.HeaderText = "Value";
            this.Value.Name = "Value";
            this.Value.Width = 350;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 133);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 20);
            this.label1.TabIndex = 11;
            this.label1.Text = "Columns settings";
            // 
            // rbGPSServerLoad
            // 
            this.rbGPSServerLoad.AutoSize = true;
            this.rbGPSServerLoad.Location = new System.Drawing.Point(6, 90);
            this.rbGPSServerLoad.Name = "rbGPSServerLoad";
            this.rbGPSServerLoad.Size = new System.Drawing.Size(194, 24);
            this.rbGPSServerLoad.TabIndex = 4;
            this.rbGPSServerLoad.TabStop = true;
            this.rbGPSServerLoad.Text = "Get from TNXQSO.com";
            this.rbGPSServerLoad.UseVisualStyleBackColor = true;
            this.rbGPSServerLoad.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // FSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(575, 395);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvColumns);
            this.Controls.Add(this.gbGPS);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.bOK);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.gbGPS.ResumeLayout(false);
            this.gbGPS.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvColumns)).EndInit();
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
        private System.Windows.Forms.DataGridView dgvColumns;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colShow;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colValue;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Show;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
        private System.Windows.Forms.RadioButton rbGPSServerLoad;
    }
}