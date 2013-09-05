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
    public partial class ReadBarcode : Form
    {
        private string strBadFlag;//错误类型
        public string strBarCode;//烟包条码
        public ReadBarcode()
        {
            InitializeComponent();
        }
        public ReadBarcode(string badFlag)
        {
            InitializeComponent();
            strBadFlag = badFlag;

        }

        private void ReadBarcode_Load(object sender, EventArgs e)
        {
            this.lblBadFlag.Text = "不合格品已到达，故障信息：" + strBadFlag + "，请确认条码位置或条码的正确性";
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


                THOK.XC.Process.Dal.ProductStateDal dal = new THOK.XC.Process.Dal.ProductStateDal();
                DataTable dt = dal.GetProductInfoByBarCode(this.txtLeftBarcode.Text.Trim());
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    this.txtBill_No.Text = dr["BILL_NO"].ToString();
                    this.txtCIGARETTE_NAME.Text = dr["CIGARETTE_NAME"].ToString();
                    this.txtGRADE_NAME.Text = dr["GRADE_NAME"].ToString();
                    this.txtORIGINAL_NAME.Text = dr["ORIGINAL_NAME"].ToString();
                    this.txtProductBarCode.Text = dr["PRODUCT_BARCODE"].ToString();
                    this.txtSTYLE_NAME.Text = dr["STYLE_NAME"].ToString();
                    this.txtWeight.Text = dr["WEIGHT"].ToString();
                }

                
                this.txtRightBarcode.SelectAll();
                this.txtRightBarcode.Focus();
            }

        }

        private void txtRightBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                THOK.XC.Process.Dal.ProductStateDal dal = new THOK.XC.Process.Dal.ProductStateDal();
                DataTable dt = dal.GetProductInfoByBarCode(this.txtRightBarcode.Text.Trim());
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    this.txtBill_No2.Text = dr["BILL_NO"].ToString();
                    this.txtCIGARETTE_NAME2.Text = dr["CIGARETTE_NAME"].ToString();
                    this.txtGRADE_NAME2.Text = dr["GRADE_NAME"].ToString();
                    this.txtORIGINAL_NAME2.Text = dr["ORIGINAL_NAME"].ToString();
                    this.txtScanCode.Text = dr["PRODUCT_BARCODE"].ToString();
                    this.txtSTYLE_NAME2.Text = dr["STYLE_NAME"].ToString();
                    this.txtWeight2.Text = dr["WEIGHT"].ToString();
                }

                this.txtLeftBarcode.SelectAll();
                this.txtLeftBarcode.Focus();
            }
        }

        private void ReadBarcode_Activated(object sender, EventArgs e)
        {
           
            this.txtLeftBarcode.Focus();
        }
    }
}
