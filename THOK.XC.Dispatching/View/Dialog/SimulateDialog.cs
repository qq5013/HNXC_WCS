using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace THOK.XC.Dispatching.View
{
    public partial class SimulateDialog : Form
    {
        public string Barcode
        {
            get { return txtBarcode.Text; }
            set { txtBarcode.Text = value; }
        }

        public SimulateDialog()
        {
            InitializeComponent();
        }
    }
}