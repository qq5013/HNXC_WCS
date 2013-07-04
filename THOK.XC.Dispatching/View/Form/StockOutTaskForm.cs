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
    public partial class StockOutTaskForm : THOK.AF.View.ToolbarForm
    {
        public StockOutTaskForm()
        {
            InitializeComponent();
            this.Column2.FilteringEnabled = true;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            StockOutBatchDal batchDal = new StockOutBatchDal();
            DataTable table = batchDal.FindAll();
            bsMain.DataSource = table;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Exit();
        }
    }
}

