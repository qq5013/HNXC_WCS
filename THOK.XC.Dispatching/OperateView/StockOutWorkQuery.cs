using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace THOK.XC.Dispatching.OperateView
{
    public partial class StockOutWorkQuery :THOK.AF.View.ToolbarForm
    {
        public StockOutWorkQuery()
        {
            InitializeComponent();
            this.dgSub.AutoGenerateColumns = false;
            this.dgvMain.AutoGenerateColumns = false;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            StockOutWorkQueryDialog frm = new StockOutWorkQueryDialog();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                string strWhere = frm.strWhere;

                Process.Dal.BillDal dal = new Process.Dal.BillDal();
                DataTable dt = dal.GetBillOutTask(strWhere);
                this.dgvMain.DataSource = dt;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Exit();
        }

        

        private void dgvMain_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (e.Row.Index > -1)
            {
                if (e.StateChanged == DataGridViewElementStates.Selected)
                {
                    string TaskID = this.dgvMain.Rows[e.Row.Index].Cells["colTaskID"].Value.ToString();

                    Process.Dal.BillDal dal = new Process.Dal.BillDal();
                    DataTable dt = dal.GetBillTaskDetail(TaskID);
                    this.dgSub.DataSource = dt;
                }
            }
        }
    }
}
