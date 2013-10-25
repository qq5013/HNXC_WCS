using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace THOK.XC.Dispatching.OperateView
{
    public partial class StockInWorkQuery :THOK.AF.View.ToolbarForm
    {
        public StockInWorkQuery()
        {
            InitializeComponent();
            this.dgSub.AutoGenerateColumns = false;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            StockInWorkQueryDialog frm = new StockInWorkQueryDialog();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                string strWhere = frm.strWhere;

                Process.Dal.BillDal dal = new Process.Dal.BillDal();
                DataTable dt = dal.GetBillInTask(strWhere);
                bsMain.DataSource = dt;
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
