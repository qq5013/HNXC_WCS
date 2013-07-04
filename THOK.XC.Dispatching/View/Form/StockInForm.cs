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
    public partial class StockInForm : THOK.AF.View.ToolbarForm
    {
        public StockInForm()
        {
            InitializeComponent();
            this.Column3.FilteringEnabled = true;
            this.Column4.FilteringEnabled = true;
            this.Column5.FilteringEnabled = true;
            this.Column6.FilteringEnabled = true;
            this.Column7.FilteringEnabled = true;
            this.Column8.FilteringEnabled = true;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Exit();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            StockInBatchDal dal = new StockInBatchDal();
            DataTable dt = dal.FindAll();
            bsMain.DataSource = dt;
            
        }
    }
}

