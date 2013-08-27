using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace THOK.XC.Process.View
{
    public partial class CannelBillSelect : Form
    {
        public string strBadCode;
        public string strBillNo;
        private string TaskID;
        public CannelBillSelect(string strTask)
        {
            InitializeComponent();
            TaskID = strTask;
        }

        private void CannelBillSelect_Load(object sender, EventArgs e)
        {

            Dal.BillDal dal = new Dal.BillDal();
            DataTable dt = dal.GetCancelBillNo(TaskID);
            this.dgvMaster.DataSource = dt;

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.dgvMaster.Rows.Count > 0)
            {
                if (this.dgvMaster.CurrentRow == null)
                {
                    MessageBox.Show("请选择入库单号！");
                }
                else
                {
                    strBillNo = this.dgvMaster.CurrentRow.Cells["BILL_NO"].Value.ToString();
                    this.DialogResult = DialogResult.OK;
                }
            }

        }

    }
}
