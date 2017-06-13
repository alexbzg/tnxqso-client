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
            this.loc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rafa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.wff = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.slConnection = new System.Windows.Forms.ToolStripStatusLabel();
            this.slCoords = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQSO)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miSettings});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1179, 24);
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
            this.dgvQSO.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.no,
            this.myCS,
            this.cs,
            this.snt,
            this.myRST,
            this.Freq,
            this.Mode,
            this.rda,
            this.loc,
            this.rafa,
            this.wff});
            this.dgvQSO.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvQSO.Location = new System.Drawing.Point(0, 24);
            this.dgvQSO.Name = "dgvQSO";
            this.dgvQSO.Size = new System.Drawing.Size(1179, 405);
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
            this.rda.HeaderText = "RDA";
            this.rda.Name = "rda";
            // 
            // loc
            // 
            this.loc.DataPropertyName = "loc";
            this.loc.HeaderText = "Loc";
            this.loc.Name = "loc";
            // 
            // rafa
            // 
            this.rafa.DataPropertyName = "rafa";
            this.rafa.HeaderText = "RAFA";
            this.rafa.Name = "rafa";
            // 
            // wff
            // 
            this.wff.DataPropertyName = "wff";
            this.wff.HeaderText = "WFF";
            this.wff.Name = "wff";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.slConnection,
            this.slCoords});
            this.statusStrip1.Location = new System.Drawing.Point(0, 407);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1179, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // slConnection
            // 
            this.slConnection.ForeColor = System.Drawing.Color.Red;
            this.slConnection.Name = "slConnection";
            this.slConnection.Size = new System.Drawing.Size(86, 17);
            this.slConnection.Text = "No connection";
            // 
            // slCoords
            // 
            this.slCoords.Name = "slCoords";
            this.slCoords.Size = new System.Drawing.Size(73, 17);
            this.slCoords.Text = "No GPS data";
            // 
            // FMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1179, 429);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.dgvQSO);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "FMain";
            this.Text = "DXpedition";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FMain_FormClosing);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQSO)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
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
        private System.Windows.Forms.DataGridViewTextBoxColumn loc;
        private System.Windows.Forms.DataGridViewTextBoxColumn rafa;
        private System.Windows.Forms.DataGridViewTextBoxColumn wff;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel slConnection;
        private System.Windows.Forms.ToolStripStatusLabel slCoords;
    }
}

