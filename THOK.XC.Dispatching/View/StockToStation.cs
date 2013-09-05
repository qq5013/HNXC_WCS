using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace THOK.XC.Dispatching.View
{
    public partial class StockToStation : Form
    {

        /// <summary>
        /// 
        /// </summary>
        private int Flag;
        public string strValue;
        private DataTable dtProductInfo;

        public StockToStation()
        {
            InitializeComponent();
        }
        public StockToStation(int flag,DataTable dtInfo)
        {
            InitializeComponent();
            Flag = flag;
            dtProductInfo = dtInfo;
        }

        private void StockToStation_Load(object sender, EventArgs e)
        {
            if (Flag == 1)//抽检，
            {
                this.lblMessage.Text = "抽检货物已到达，请人工处理";

            }
            else if (Flag == 2)
            {
                this.lblMessage.Text = "补料货物已到达，请人工处理";
            }
            else if (Flag == 4)
            {
                this.lblMessage.Text = "倒库货物已到达，请人工处理";
            }
            DataRow dr = dtProductInfo.Rows[0];
            this.txtBill_No.Text = dr["BILL_NO"].ToString();
            this.txtCIGARETTE_NAME.Text = dr["CIGARETTE_NAME"].ToString();
            this.txtGRADE_NAME.Text = dr["GRADE_NAME"].ToString();
            this.txtORIGINAL_NAME.Text = dr["ORIGINAL_NAME"].ToString();
            this.txtProductBarCode.Text = dr["PRODUCT_BARCODE"].ToString();
            this.txtSTYLE_NAME.Text = dr["STYLE_NAME"].ToString();
            this.txtWeight.Text = dr["WEIGHT"].ToString();


        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.strValue = "1";
            this.DialogResult = DialogResult.OK;
        }
    }
}
