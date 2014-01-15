using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace THOK.XC.Dispatching.OperateView
{
    public partial class frmCraneTaskOption :THOK.AF.View.ToolbarForm
    {
        private DataTable dt;
        public frmCraneTaskOption()
        {
            InitializeComponent();
            this.dgvMain.AutoGenerateColumns = false;
            this.cmbCrane.SelectedIndex = 0;
        }
        
        private void btnRefresh_Click(object sender, EventArgs e)
        {

            XC.Process.Dal.BillDal dal = new Process.Dal.BillDal();
            dt= dal.GetCranTaskByCraneNo(this.cmbCrane.Text);
            this.dgvMain.DataSource = dt;

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Exit();
        }

        private void btnOption_Click(object sender, EventArgs e)
        {
            if (this.dgvMain.SelectedRows.Count == 0)
            {
                MessageBox.Show("请选择要操作的数据行！", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            CraneTaskOptionDialog frm = new CraneTaskOptionDialog();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                DataRow dr = dt.Select(string.Format("TASK_ID='{0}'", this.dgvMain.SelectedRows[0].Cells["colTaskID"].Value.ToString()))[0];
                if (frm.OptionCode == "DER") //删除指定
                {
                    THOK.CRANE.TelegramData tgd = new CRANE.TelegramData();
                    tgd.CraneNo = dr["CRANE_NO"].ToString();
                    tgd.AssignmenID = dr["ASSIGNMENT_ID"].ToString();

                    THOK.CRANE.TelegramFraming tf = new CRANE.TelegramFraming();
                    string QuenceNo = GetNextSQuenceNo();
                    string str = tf.DataFraming("1" + QuenceNo, tgd, tf.TelegramDER);
                    this.mainFrame.Context.ProcessDispatcher.WriteToService("Crane", "DER", str);

                    MessageBox.Show("操作成功", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    THOK.CRANE.TelegramData tgd = new CRANE.TelegramData();
                    tgd.CraneNo = dr["CRANE_NO"].ToString();
                    tgd.AssignmenID = dr["ASSIGNMENT_ID"].ToString();

                    string TaskType = dr["TASK_TYPE"].ToString();
                    string ItemNo = dr["ITEM_NO"].ToString();


                    if (TaskType.Substring(1, 1) == "4" && ItemNo == "1" && dr["CRANE_NO"].ToString() == dr["NEW_CRANE_NO"].ToString())
                    {
                        tgd.StartPosition = dr["CRANESTATION"].ToString();
                        tgd.DestinationPosition = dr["NEW_TO_STATION"].ToString();
                    }
                    else
                    {
                        if (TaskType.Substring(1, 1) == "1" || (TaskType.Substring(1, 1) == "4" && ItemNo == "3") || TaskType.Substring(1, 1) == "3" && ItemNo == "4") //入库
                        {
                            tgd.StartPosition = dr["CRANESTATION"].ToString();
                            tgd.DestinationPosition = dr["CELLSTATION"].ToString();
                        }
                        else //出库
                        {
                            tgd.StartPosition = dr["CELLSTATION"].ToString();
                            tgd.DestinationPosition = dr["CRANESTATION"].ToString();
                        }
                    }

                    THOK.CRANE.TelegramFraming tf = new CRANE.TelegramFraming();
                    string QuenceNo = GetNextSQuenceNo();
                    string str = tf.DataFraming("1" + QuenceNo, tgd, tf.TelegramARQ);
                    this.mainFrame.Context.ProcessDispatcher.WriteToService("Crane", "ARQ", str);

                    MessageBox.Show("操作成功", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
        }

        private string GetNextSQuenceNo()
        {
            XC.Process.Dal.SysStationDal dal = new XC.Process.Dal.SysStationDal();
            return dal.GetTaskNo("S");
        }

       

       
    }
}
