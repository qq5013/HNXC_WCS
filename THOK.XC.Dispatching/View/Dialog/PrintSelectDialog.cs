using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace THOK.XC.Dispatching.View
{
    public partial class PrintSelectDialog : Form
    {
        public string SelectedPrintBatch
        {
            get { return cbPrintBatch.SelectedValue.ToString(); }
        }

        public PrintSelectDialog(DataTable table)
        {
            InitializeComponent();
            cbPrintBatch.DataSource = table;
            cbPrintBatch.ValueMember = "BATCHINFO";
            cbPrintBatch.DisplayMember = "BATCHNAME";            
        }
    }
}