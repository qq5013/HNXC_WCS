using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace THOK.XC.Dispatching.View
{
    public partial class PalletSelect : Form
    {
        public int Flag = 0;
        public PalletSelect()
        {
            InitializeComponent();
        }

        private void btnPallet_Click(object sender, EventArgs e)
        {
            Flag = 1;
            this.DialogResult = DialogResult.OK;

        }

        private void btnpallets_Click(object sender, EventArgs e)
        {
            Flag = 2;
            this.DialogResult = DialogResult.OK;
        }
    }
}
