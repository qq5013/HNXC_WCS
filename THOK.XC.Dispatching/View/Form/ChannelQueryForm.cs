using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using THOK.XC.Process.Dal;

namespace THOK.XC.Dispatching.View
{
    public partial class ChannelQueryForm : THOK.AF.View.ToolbarForm
    {
        private ChannelDal channelDal = new ChannelDal();

        public ChannelQueryForm()
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

        private void btnExchange_Click(object sender, EventArgs e)
        {
           
        }
    }
}

