using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dxpClient
{
    public partial class FSettings : Form
    {
        public string rda {  get { return tbRDA.Text; } }
        public string loc { get { return tbLoc.Text; } }
        public string wwf { get { return tbWWF.Text; } }

        public FSettings( DXpConfig data )
        {
            InitializeComponent();
            tbRDA.Text = data.rda;
            tbLoc.Text = data.loc;
            tbWWF.Text = data.wwf;
        }
    }
}
