using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using THOK.XC.Process.Dal;
using THOK.MCP;

namespace THOK.XC.Dispatching.View
{
    public partial class ChannelForm : THOK.AF.View.ToolbarForm
    {
        public ChannelForm()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Exit();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            
        }
    }
}

