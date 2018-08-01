using System;

namespace tnxqsoClient
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FMain));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.miLogin = new System.Windows.Forms.ToolStripMenuItem();
            this.miSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miExportRDA = new System.Windows.Forms.ToolStripMenuItem();
            this.miExportRAFA = new System.Windows.Forms.ToolStripMenuItem();
            this.tbCSFilter = new System.Windows.Forms.ToolStripTextBox();
            this.miFilter = new System.Windows.Forms.ToolStripMenuItem();
            this.miStats = new System.Windows.Forms.ToolStripMenuItem();
            this.miStatsRDA = new System.Windows.Forms.ToolStripMenuItem();
            this.miStatsRAFA = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvQSO = new System.Windows.Forms.DataGridView();
            this.no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ts = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.myCS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cs = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.snt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.myRST = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Freq = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Mode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RDA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RAFA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WFF = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Locator = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.slConnection = new System.Windows.Forms.ToolStripStatusLabel();
            this.slCoords = new System.Windows.Forms.ToolStripStatusLabel();
            this.slLoc = new System.Windows.Forms.ToolStripStatusLabel();
            this.slLoggedIn = new System.Windows.Forms.ToolStripStatusLabel();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQSO)).BeginInit();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miLogin,
            this.miSettings,
            this.exportToolStripMenuItem,
            this.tbCSFilter,
            this.miFilter,
            this.miStats});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1179, 27);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip";
            // 
            // miLogin
            // 
            this.miLogin.Name = "miLogin";
            this.miLogin.Size = new System.Drawing.Size(49, 23);
            this.miLogin.Text = "Login";
            this.miLogin.Click += new System.EventHandler(this.miLogin_Click);
            // 
            // miSettings
            // 
            this.miSettings.Name = "miSettings";
            this.miSettings.Size = new System.Drawing.Size(61, 23);
            this.miSettings.Text = "Settings";
            this.miSettings.Click += new System.EventHandler(this.miSettings_Click);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miExportRDA,
            this.miExportRAFA});
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(52, 23);
            this.exportToolStripMenuItem.Text = "Export";
            // 
            // miExportRDA
            // 
            this.miExportRDA.Name = "miExportRDA";
            this.miExportRDA.Size = new System.Drawing.Size(102, 22);
            this.miExportRDA.Text = "RDA";
            this.miExportRDA.Click += new System.EventHandler(this.miExportRDA_Click);
            // 
            // miExportRAFA
            // 
            this.miExportRAFA.Name = "miExportRAFA";
            this.miExportRAFA.Size = new System.Drawing.Size(102, 22);
            this.miExportRAFA.Text = "RAFA";
            this.miExportRAFA.Click += new System.EventHandler(this.miExportRAFA_Click);
            // 
            // tbCSFilter
            // 
            this.tbCSFilter.AutoToolTip = true;
            this.tbCSFilter.Name = "tbCSFilter";
            this.tbCSFilter.Size = new System.Drawing.Size(100, 23);
            this.tbCSFilter.ToolTipText = "Callsign filter";
            // 
            // miFilter
            // 
            this.miFilter.CheckOnClick = true;
            this.miFilter.Name = "miFilter";
            this.miFilter.Size = new System.Drawing.Size(45, 23);
            this.miFilter.Text = "Filter";
            this.miFilter.Click += new System.EventHandler(this.tbCSFilter_TextChanged);
            // 
            // miStats
            // 
            this.miStats.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miStatsRDA,
            this.miStatsRAFA});
            this.miStats.Name = "miStats";
            this.miStats.Size = new System.Drawing.Size(44, 23);
            this.miStats.Text = "Stats";
            // 
            // miStatsRDA
            // 
            this.miStatsRDA.Name = "miStatsRDA";
            this.miStatsRDA.Size = new System.Drawing.Size(102, 22);
            this.miStatsRDA.Text = "RDA";
            this.miStatsRDA.Click += new System.EventHandler(this.miStatsRDA_Click);
            // 
            // miStatsRAFA
            // 
            this.miStatsRAFA.Name = "miStatsRAFA";
            this.miStatsRAFA.Size = new System.Drawing.Size(102, 22);
            this.miStatsRAFA.Text = "RAFA";
            this.miStatsRAFA.Click += new System.EventHandler(this.miStatsRAFA_Click);
            // 
            // dgvQSO
            // 
            this.dgvQSO.AllowUserToAddRows = false;
            this.dgvQSO.AllowUserToResizeRows = false;
            this.dgvQSO.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvQSO.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvQSO.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.no,
            this.ts,
            this.myCS,
            this.cs,
            this.snt,
            this.myRST,
            this.Freq,
            this.Mode,
            this.RDA,
            this.RAFA,
            this.WFF,
            this.Locator});
            this.dgvQSO.Location = new System.Drawing.Point(0, 27);
            this.dgvQSO.Name = "dgvQSO";
            this.dgvQSO.Size = new System.Drawing.Size(1179, 374);
            this.dgvQSO.TabIndex = 1;
            this.dgvQSO.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.DgvQSO_ColumnWidthChanged);
            // 
            // no
            // 
            this.no.DataPropertyName = "no";
            this.no.HeaderText = "Nr";
            this.no.Name = "no";
            // 
            // ts
            // 
            this.ts.DataPropertyName = "ts";
            this.ts.HeaderText = "Time";
            this.ts.Name = "ts";
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
            // RDA
            // 
            this.RDA.DataPropertyName = "rda";
            this.RDA.HeaderText = "RDA";
            this.RDA.Name = "RDA";
            // 
            // RAFA
            // 
            this.RAFA.DataPropertyName = "rafa";
            this.RAFA.HeaderText = "RAFA";
            this.RAFA.Name = "RAFA";
            // 
            // WFF
            // 
            this.WFF.DataPropertyName = "wff";
            this.WFF.HeaderText = "WFF";
            this.WFF.Name = "WFF";
            // 
            // Locator
            // 
            this.Locator.DataPropertyName = "loc";
            this.Locator.HeaderText = "Locator";
            this.Locator.Name = "Locator";
            // 
            // statusStrip
            // 
            this.statusStrip.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.slConnection,
            this.slCoords,
            this.slLoc,
            this.slLoggedIn});
            this.statusStrip.Location = new System.Drawing.Point(0, 403);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1179, 26);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "statusStrip1";
            // 
            // slConnection
            // 
            this.slConnection.ForeColor = System.Drawing.Color.Red;
            this.slConnection.Name = "slConnection";
            this.slConnection.Size = new System.Drawing.Size(111, 21);
            this.slConnection.Text = "No connection";
            // 
            // slCoords
            // 
            this.slCoords.Name = "slCoords";
            this.slCoords.Size = new System.Drawing.Size(98, 21);
            this.slCoords.Text = "No GPS data";
            // 
            // slLoc
            // 
            this.slLoc.ForeColor = System.Drawing.Color.Red;
            this.slLoc.Name = "slLoc";
            this.slLoc.Size = new System.Drawing.Size(0, 21);
            // 
            // slLoggedIn
            // 
            this.slLoggedIn.Name = "slLoggedIn";
            this.slLoggedIn.Size = new System.Drawing.Size(105, 21);
            this.slLoggedIn.Text = "Not logged in";
            // 
            // FMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1179, 429);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.dgvQSO);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "FMain";
            this.Text = "QSO Client";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FMain_FormClosing);
            this.Load += new System.EventHandler(this.FMain_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FMain_KeyDown);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQSO)).EndInit();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.DataGridView dgvQSO;
        private System.Windows.Forms.ToolStripMenuItem miSettings;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel slConnection;
        private System.Windows.Forms.ToolStripStatusLabel slCoords;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.DataGridViewTextBoxColumn no;
        private System.Windows.Forms.DataGridViewTextBoxColumn ts;
        private System.Windows.Forms.DataGridViewTextBoxColumn myCS;
        private System.Windows.Forms.DataGridViewTextBoxColumn cs;
        private System.Windows.Forms.DataGridViewTextBoxColumn snt;
        private System.Windows.Forms.DataGridViewTextBoxColumn myRST;
        private System.Windows.Forms.DataGridViewTextBoxColumn Freq;
        private System.Windows.Forms.DataGridViewTextBoxColumn Mode;
        private System.Windows.Forms.ToolStripStatusLabel slLoc;
        private System.Windows.Forms.ToolStripTextBox tbCSFilter;
        private System.Windows.Forms.ToolStripMenuItem miFilter;
        private System.Windows.Forms.ToolStripMenuItem miLogin;
        private System.Windows.Forms.ToolStripStatusLabel slLoggedIn;
        private System.Windows.Forms.ToolStripMenuItem miExportRDA;
        private System.Windows.Forms.ToolStripMenuItem miExportRAFA;
        private System.Windows.Forms.ToolStripMenuItem miStats;
        private System.Windows.Forms.ToolStripMenuItem miStatsRDA;
        private System.Windows.Forms.ToolStripMenuItem miStatsRAFA;
        private System.Windows.Forms.DataGridViewTextBoxColumn RDA;
        private System.Windows.Forms.DataGridViewTextBoxColumn RAFA;
        private System.Windows.Forms.DataGridViewTextBoxColumn WFF;
        private System.Windows.Forms.DataGridViewTextBoxColumn Locator;
    }
}

