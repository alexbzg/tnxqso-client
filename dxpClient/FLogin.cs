﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tnxqsoClient
{
    public partial class FLogin : Form
    {
        private DXpConfig config;
        private HTTPService httpService;

        public FLogin( DXpConfig _config, HTTPService _httpService)
        {
            InitializeComponent();
            config = _config;
            httpService = _httpService;
            tbCallsign.Text = config.callsign;
        }

        private async void bLogin_Click(object sender, EventArgs e)
        {
            Enabled = false;
            UseWaitCursor = true;
            System.Net.HttpStatusCode? sc = await httpService.login(tbCallsign.Text, tbPassword.Text);
            if (sc == System.Net.HttpStatusCode.OK)
            {
                DialogResult = DialogResult.OK;
            } else if (sc != System.Net.HttpStatusCode.BadRequest)
            {
                MessageBox.Show("Login failed. Please try again later.");
                DialogResult = DialogResult.Cancel;
            }
            Enabled = true;
            UseWaitCursor = false;
        }
    }
}
