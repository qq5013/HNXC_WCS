using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace THOK.XC.Dispatching.View
{
    public partial class ChannelDialog : Form
    {
        public string SelectedChannelCode
        {
            get { return cbChannel.SelectedValue.ToString(); }
        }

        public ChannelDialog(DataTable table)
        {
            InitializeComponent();
            cbChannel.DataSource = table;
            cbChannel.ValueMember = "CHANNELCODE";
            cbChannel.DisplayMember = "CHANNELNAME";            
        }
    }
}