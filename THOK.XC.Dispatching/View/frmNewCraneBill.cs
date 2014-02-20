using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace THOK.XC.Dispatching.View
{
    public partial class frmNewCraneBill : Form
    {
        public string strBillNo;
        private string TaskID;
        private DataTable dtProductInfo;
        private string CraneNo;
        private string ErrMsg;
        public frmNewCraneBill()
        {
            InitializeComponent();
        }
        public frmNewCraneBill(string strTask, string strErrMsg, DataTable dt,string strCraneNo)
        {
            InitializeComponent();
            TaskID = strTask;
            dtProductInfo = dt;
            CraneNo = strCraneNo;
            ErrMsg = strErrMsg;
         }
        

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.cmbBill.Items.Count > 0)
            {
                this.strBillNo = this.cmbBill.SelectedText;
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                this.strBillNo = "1";
                this.DialogResult = DialogResult.OK;
            }

        }

        private void frmNewCraneBill_Load(object sender, EventArgs e)
        {
            this.lblMsg.Text = "堆垛机返回错误，" + ErrMsg;
            if (dtProductInfo.Rows.Count > 0)
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
            THOK.XC.Process.Dal.BillDal Bdal = new THOK.XC.Process.Dal.BillDal();
            DataTable dtBill = Bdal.GetCancelBillNo(TaskID, CraneNo);
            this.cmbBill.ValueMember = "BILL_NO";
            this.cmbBill.DisplayMember = "BILL_NO";
            this.cmbBill.DataSource = dtBill;
        }
    }
}
