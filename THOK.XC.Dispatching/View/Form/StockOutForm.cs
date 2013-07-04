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
    public partial class StockOutForm : THOK.AF.View.ToolbarForm
    {
        public StockOutForm()
        {
            InitializeComponent();
            this.Column2.FilteringEnabled = true;
            this.Column3.FilteringEnabled = true;
            this.Column4.FilteringEnabled = true;
            this.Column5.FilteringEnabled = true;
            this.Column6.FilteringEnabled = true;
            this.Column7.FilteringEnabled = true;
            this.Column8.FilteringEnabled = true;
            this.Column9.FilteringEnabled = true;
            this.Column10.FilteringEnabled = true;
            this.Column11.FilteringEnabled = true;
            this.State.FilteringEnabled = true;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Exit();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            StockOutDal outDal = new StockOutDal();
            DataTable table = outDal.FindAll();
            bsMain.DataSource = table;
        }
    }
}

