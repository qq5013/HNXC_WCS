using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace THOK.XC.Dispatching.View
{
    public partial class CheckScan : Form
    {
        /// <summary>
        /// 
        /// </summary>
        private int Flag;
        public string strValue;
        private DataTable dtProductInfo;
        public CheckScan()
        {
            InitializeComponent();
        }
        public CheckScan(int flag, DataTable dtInfo)
        {
            InitializeComponent();
            Flag = flag;
            dtProductInfo = dtInfo;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.txtProductBarCode.Text.Trim() != this.txtScanCode.Text.Trim())
            {
                strValue = this.txtScanCode.Text.Trim();
            }
            else
                strValue = "1";
            this.DialogResult = DialogResult.OK;

        }

        private void txtScanCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                THOK.XC.Process.Dal.ProductStateDal dal = new Process.Dal.ProductStateDal();
                DataTable dt = dal.GetProductInfoByBarCode(this.txtScanCode.Text.Trim());
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    this.txtBill_No2.Text = dr["BILL_NO"].ToString();
                    this.txtCIGARETTE_NAME2.Text = dr["CIGARETTE_NAME"].ToString();
                    this.txtGRADE_NAME2.Text = dr["GRADE_NAME"].ToString();
                    this.txtORIGINAL_NAME2.Text = dr["ORIGINAL_NAME"].ToString();
                    this.txtSTYLE_NAME2.Text = dr["STYLE_NAME"].ToString();
                    this.txtWeight2.Text = dr["WEIGHT"].ToString();
 
                }
            }
            
        }

        private void CheckScan_Load(object sender, EventArgs e)
        {
            DataRow dr = dtProductInfo.Rows[0];
            this.txtBill_No.Text = dr["BILL_NO"].ToString();
            this.txtCIGARETTE_NAME.Text = dr["CIGARETTE_NAME"].ToString();
            this.txtGRADE_NAME.Text = dr["GRADE_NAME"].ToString();
            this.txtORIGINAL_NAME.Text = dr["ORIGINAL_NAME"].ToString();
            this.txtProductBarCode.Text = dr["PRODUCT_BARCODE"].ToString();
            this.txtSTYLE_NAME.Text = dr["STYLE_NAME"].ToString();
            this.txtWeight.Text = dr["WEIGHT"].ToString();
        }

        private void CheckScan_Activated(object sender, EventArgs e)
        {
            this.txtScanCode.SelectAll();
            this.txtScanCode.Focus();
        }
    }
}
