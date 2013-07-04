using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using THOK.XC.Process.Dal;

namespace THOK.XC.Dispatching.View
{
    public partial class ChannelQueryForm : THOK.AF.View.ToolbarForm
    {
        private ChannelDal channelDal = new ChannelDal();

        public ChannelQueryForm()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Exit();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (bsMain.DataSource == null)
                bsMain.DataSource = channelDal.GetChannelUSED();
            else
                DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn.RemoveFilter(dgvMain);
        }

        private void btnExchange_Click(object sender, EventArgs e)
        {
            if (dgvMain.SelectedRows.Count > 0 && dgvMain.SelectedRows[0].Cells["QUANTITY"].Value.ToString() != "0")
            {
                string channelCode = dgvMain.SelectedRows[0].Cells["CHANNELCODE"].Value.ToString();
                string lineCode = dgvMain.SelectedRows[0].Cells["LINECODE"].Value.ToString();

                DataTable channeltable = channelDal.GetChannelUSED(lineCode, channelCode);
                if (channeltable.Rows[0]["CHANNELTYPE"].ToString() == "5")
                    return;

                int channelGroup = Convert.ToInt32(channeltable.Rows[0]["CHANNELGROUP"]);
                string channelType = channeltable.Rows[0]["CHANNELTYPE"].ToString();

                DataTable table = channelDal.GetEmptyChannelUSED(lineCode,channelCode,channelGroup,channelType);

                if (table.Rows.Count != 0)
                {
                    ChannelDialog channelDailog = new ChannelDialog(table);
                    if (channelDailog.ShowDialog() == DialogResult.OK)
                    {
                        channelDal.ExechangeChannelUSED(lineCode,channelCode,channelDailog.SelectedChannelCode);

                        DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn.RemoveFilter(dgvMain);
                        bsMain.DataSource = channelDal.GetChannelUSED();
                    }
                }
                else
                    MessageBox.Show("无法调整烟道，原因：没有可用烟道。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}

