namespace dxpClient
{
    partial class FMain
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
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.miSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvQSO = new System.Windows.Forms.DataGridView();
            this.no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.myCS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cs = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.snt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.myRST = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Freq = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Mode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rda = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.wwf = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQSO)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miSettings});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(954, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip";
            // 
            // miSettings
            // 
            this.miSettings.Name = "miSettings";
            this.miSettings.Size = new System.Drawing.Size(61, 20);
            this.miSettings.Text = "Settings";
            this.miSettings.Click += new System.EventHandler(this.miSettings_Click);
            // 
            // dgvQSO
            // 
            this.dgvQSO.AllowUserToAddRows = false;
            this.dgvQSO.AllowUserToDeleteRows = false;
            this.dgvQSO.AllowUserToResizeRows = false;
            this.dgvQSO.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvQSO.ColumnWidthChanged += DgvQSO_ColumnWidthChanged;
            this.dgvQSO.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.no,
            this.myCS,
            this.cs,
            this.snt,
            this.myRST,
            this.Freq,
            this.Mode,
            this.rda,
            this.wwf});
            this.dgvQSO.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvQSO.Location = new System.Drawing.Point(0, 24);
            this.dgvQSO.Name = "dgvQSO";
            this.dgvQSO.Size = new System.Drawing.Size(954, 355);
            this.dgvQSO.TabIndex = 1;
            // 
            // no
            // 
            this.no.DataPropertyName = "no";
            this.no.HeaderText = "no";
            this.no.Name = "no";
            // 
            // myCS
            // 
            this.myCS.DataPropertyName = "myCS";
            this.myCS.HeaderText = "My Call";
            this.myCS.Name = "myCS";
            // 
            // cs
            // 
            this.cs.DataPropertyName = "cs";
            this.cs.HeaderText = "Ur Call";
            this.cs.Name = "cs";
            // 
            // snt
            // 
            this.snt.DataPropertyName = "snt";
            this.snt.HeaderText = "Ur RST";
            this.snt.Name = "snt";
            // 
            // myRST
            // 
            this.myRST.DataPropertyName = "rcv";
            this.myRST.HeaderText = "My RST";
            this.myRST.Name = "myRST";
            // 
            // Freq
            // 
            this.Freq.DataPropertyName = "freq";
            this.Freq.HeaderText = "Freq";
            this.Freq.Name = "Freq";
            // 
            // Mode
            // 
            this.Mode.DataPropertyName = "mode";
            this.Mode.HeaderText = "Mode";
            this.Mode.Name = "Mode";
            // 
            // rda
            // 
            this.rda.DataPropertyName = "rda";
            this.rda.HeaderText = "rda";
            this.rda.Name = "rda";
            // 
            // wwf
            // 
            this.wwf.DataPropertyName = "wwf";
            this.wwf.HeaderText = "wwf";
            this.wwf.Name = "wwf";
            // 
            // FMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(954, 379);
            this.Controls.Add(this.dgvQSO);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "FMain";
            this.Text = "DXpedition";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FMain_FormClosing);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQSO)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.DataGridView dgvQSO;
        private System.Windows.Forms.ToolStripMenuItem miSettings;
        private System.Windows.Forms.DataGridViewTextBoxColumn no;
        private System.Windows.Forms.DataGridViewTextBoxColumn myCS;
        private System.Windows.Forms.DataGridViewTextBoxColumn cs;
        private System.Windows.Forms.DataGridViewTextBoxColumn snt;
        private System.Windows.Forms.DataGridViewTextBoxColumn myRST;
        private System.Windows.Forms.DataGridViewTextBoxColumn Freq;
        private System.Windows.Forms.DataGridViewTextBoxColumn Mode;
        private System.Windows.Forms.DataGridViewTextBoxColumn rda;
        private System.Windows.Forms.DataGridViewTextBoxColumn wwf;
    }
}

