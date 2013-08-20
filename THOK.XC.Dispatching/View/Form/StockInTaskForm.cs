using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using THOK.XC.Process.Dal;
using THOK.XC.Dispatching.Util;

namespace THOK.XC.Dispatching.View
{
    public partial class StockInTaskForm : THOK.AF.View.ToolbarForm
    {
        public StockInTaskForm()
        {
            InitializeComponent();
            this.CHANNELNAME.FilteringEnabled = true;
            this.CIGARETTECODE.FilteringEnabled = true;
            this.CIGARETTENAME.FilteringEnabled = true;
            this.STATE.FilteringEnabled = true;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            StockInBatchDal batchDal = new StockInBatchDal();
            DataTable table = batchDal.FindAll();
            bsMain.DataSource = table;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Exit();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                string ProductInfo = dgvMain.CurrentRow.Cells["CIGARETTECODE"].Value.ToString()
                    + dgvMain.CurrentRow.Cells["CIGARETTENAME"].Value.ToString()
                    + dgvMain.CurrentRow.Cells["QUANTITY"].Value.ToString();
                string BarCode = dgvMain.CurrentRow.Cells["BARCODE"].Value.ToString();

                zebraPrint z = new zebraPrint();
                z.setPrinter("ZDesigner 105SL 203DPI (1)");
                z.addCharacter(10, 10, ProductInfo, 20, 10, "ËÎÌו", 0, 0, 2, 1);
                z.addBarCode(10, 40, BarCode, 230, "CODE128", "320", "A", 0, "B", 6);
                z.Print(Convert.ToInt32(dgvMain.CurrentRow.Cells["QUANTITY"].Value));
            }
            catch (Exception E)
            {
                throw new Exception(E.Message);
            }

        }
    }
}

