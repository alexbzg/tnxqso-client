namespace dxpClient
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
            this.bOK = new System.Windows.Forms.Button();
            this.bCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbRDA = new System.Windows.Forms.TextBox();
            this.tbWWF = new System.Windows.Forms.TextBox();
            this.cbGPSPort = new System.Windows.Forms.ComboBox();
            this.gbGPS = new System.Windows.Forms.GroupBox();
            this.rbGPSSerial = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.gbGPS.SuspendLayout();
            this.SuspendLayout();
            // 
            // bOK
            // 
            this.bOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.bOK.Location = new System.Drawing.Point(100, 260);
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
            this.bCancel.Location = new System.Drawing.Point(181, 260);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(75, 30);
            this.bCancel.TabIndex = 1;
            this.bCancel.Text = "Cancel";
            this.bCancel.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "RDA";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "WWF";
            // 
            // tbRDA
            // 
            this.tbRDA.Location = new System.Drawing.Point(69, 5);
            this.tbRDA.Name = "tbRDA";
            this.tbRDA.Size = new System.Drawing.Size(187, 26);
            this.tbRDA.TabIndex = 5;
            // 
            // tbWWF
            // 
            this.tbWWF.Location = new System.Drawing.Point(69, 78);
            this.tbWWF.Name = "tbWWF";
            this.tbWWF.Size = new System.Drawing.Size(187, 26);
            this.tbWWF.TabIndex = 7;
            // 
            // cbGPSPort
            // 
            this.cbGPSPort.FormattingEnabled = true;
            this.cbGPSPort.Location = new System.Drawing.Point(6, 52);
            this.cbGPSPort.Name = "cbGPSPort";
            this.cbGPSPort.Size = new System.Drawing.Size(162, 28);
            this.cbGPSPort.TabIndex = 8;
            // 
            // gbGPS
            // 
            this.gbGPS.Controls.Add(this.radioButton2);
            this.gbGPS.Controls.Add(this.cbGPSPort);
            this.gbGPS.Controls.Add(this.rbGPSSerial);
            this.gbGPS.Location = new System.Drawing.Point(4, 110);
            this.gbGPS.Name = "gbGPS";
            this.gbGPS.Size = new System.Drawing.Size(252, 124);
            this.gbGPS.TabIndex = 10;
            this.gbGPS.TabStop = false;
            this.gbGPS.Text = "GPS Settings";
            // 
            // rbGPSSerial
            // 
            this.rbGPSSerial.AutoSize = true;
            this.rbGPSSerial.Location = new System.Drawing.Point(3, 22);
            this.rbGPSSerial.Name = "rbGPSSerial";
            this.rbGPSSerial.Size = new System.Drawing.Size(99, 24);
            this.rbGPSSerial.TabIndex = 0;
            this.rbGPSSerial.TabStop = true;
            this.rbGPSSerial.Text = "Serial port";
            this.rbGPSSerial.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(3, 86);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(154, 24);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Wireless Gateway";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // FSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(268, 301);
            this.Controls.Add(this.gbGPS);
            this.Controls.Add(this.tbWWF);
            this.Controls.Add(this.tbRDA);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.bOK);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FSettings";
            this.Text = "Settings";
            this.gbGPS.ResumeLayout(false);
            this.gbGPS.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bOK;
        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbRDA;
        private System.Windows.Forms.TextBox tbWWF;
        private System.Windows.Forms.ComboBox cbGPSPort;
        private System.Windows.Forms.GroupBox gbGPS;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton rbGPSSerial;
    }
}