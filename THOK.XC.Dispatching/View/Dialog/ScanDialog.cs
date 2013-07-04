using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using THOK.XC.Process;

namespace THOK.XC.Dispatching.View
{
    public partial class ScanDialog : Form
    {
        private string text = "";

        public ScanDialog(DataTable table)
        {
            InitializeComponent();
            cbCigarette.DataSource = table;
            cbCigarette.ValueMember = "CIGARETTECODE";
            cbCigarette.DisplayMember = "CIGARETTENAME";
        }

        private void ScanDialog_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = THOK.XC.Process.Util.GraphicsUtil.CreateRandomBitmap(out text);
        }

        public bool IsPass
        {
            get { return textBox1.Text == text; }
        }

        public string SelectedCigaretteCode
        {
            get { return cbCigarette.SelectedValue.ToString(); }
        }

        public string Barcode
        {
            get { return textBox3.Text.ToString(); }
        }

        internal void setInformation(string text, string barcode)
        {
            textBox2.ReadOnly = false;
            textBox2.Text = text;
            textBox3.Text = barcode;
            textBox2.ReadOnly = true;
        }
    }
}