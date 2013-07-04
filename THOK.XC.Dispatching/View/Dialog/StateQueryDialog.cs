using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace THOK.XC.Dispatching.View
{
    public partial class StateQueryDialog : Form
    {
        public string SelectedQueryType
        {
            get { return cbQueryStateType.SelectedValue.ToString(); }
        }

        public StateQueryDialog(DataTable table)
        {
            InitializeComponent();
            cbQueryStateType.DataSource = table;
            cbQueryStateType.ValueMember = "STATECODE";
            cbQueryStateType.DisplayMember = "STATENAME";
        }
    }
}