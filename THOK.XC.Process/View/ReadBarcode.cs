using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace THOK.XC.Process.View
{
    public partial class ReadBarcode : Form
    {
        public string strBadFlag;//错误类型
        public string strBarCode;//烟包条码
        public ReadBarcode()
        {
            InitializeComponent();
        }

        private void ReadBarcode_Load(object sender, EventArgs e)
        {
            


        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.txtLeftBarcode.Text.Trim() != "" && this.txtRightBarcode.Text.Trim()!="")
            {
                if (this.txtLeftBarcode.Text.Trim() == this.txtRightBarcode.Text.Trim())
                {
                    strBarCode = this.txtLeftBarcode.Text.Trim();
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show("左右两边条码不一致，请重新扫描！");
                }
            }
            else
            {
                MessageBox.Show("请扫描条码！");
            }
        }

        private void txtLeftBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {


                Dal.ProductStateDal dal = new Dal.ProductStateDal();
                this.lblLeftProductInfo.Text = dal.GetProductInfo(this.txtLeftBarcode.Text.TrimEnd());
                
                this.txtRightBarcode.SelectAll();
                this.txtRightBarcode.Focus();
            }

        }

        private void txtRightBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Dal.ProductStateDal dal = new Dal.ProductStateDal();
                this.lblLeftProductInfo.Text = dal.GetProductInfo(this.txtRightBarcode.Text.TrimEnd());

                this.txtLeftBarcode.SelectAll();
                this.txtLeftBarcode.Focus();
            }
        }

        private void ReadBarcode_Activated(object sender, EventArgs e)
        {
            this.lblBadFlag.Text = "错误条码类型：" + strBadFlag;
            this.txtLeftBarcode.Focus();
        }
    }
}
