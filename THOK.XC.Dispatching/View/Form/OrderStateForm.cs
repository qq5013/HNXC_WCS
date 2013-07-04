using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using THOK.XC.Process.Dal;
using THOK.XC.Process.Dao;
using THOK.MCP;
using THOK.Util;

namespace THOK.XC.Dispatching.View
{
    public partial class OrderStateForm : THOK.AF.View.ToolbarForm 
    {
        public OrderStateForm()
        {
            InitializeComponent();
            this.Column2.FilteringEnabled = true;
            this.Column3.FilteringEnabled = true;
            this.Column4.FilteringEnabled = true;
            this.Column5.FilteringEnabled = true;
            this.Column6.FilteringEnabled = true;
            this.Column7.FilteringEnabled = true;
            this.Column8.FilteringEnabled = true;
         }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            GetScannerDataSet();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Exit();
        }

        private void GetScannerDataSet()
        {
            try
            {
                using (PersistentManager pm = new PersistentManager())
                {
                    StateDao stateDao=new StateDao();
                    DataTable FindStateQueryType=stateDao.FindScannerListTable();
                    StateQueryDialog stateQueryDialog = new StateQueryDialog(FindStateQueryType);
                    
                    if (stateQueryDialog.ShowDialog() == DialogResult.OK)
                    {
                        string stateCode = "";
                        string indexNo = "";
                        string viewName = "";

                        stateCode = stateQueryDialog.SelectedQueryType.ToString();
                        indexNo = stateDao.FindScannerIndexNoByStateCode(stateCode).Rows[0]["ROW_INDEX"].ToString();
                        viewName = stateDao.FindScannerIndexNoByStateCode(stateCode).Rows[0]["VIEWNAME"].ToString();

                        bsMain.DataSource = stateDao.FindScannerStateByIndexNo(indexNo,viewName);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }

        private void GetLedDataSet()
        {
            try
            {
                using (PersistentManager pm = new PersistentManager())
                {
                    StateDao stateDao = new StateDao();
                    DataTable FindStateQueryType = stateDao.FindLedListTable();
                    StateQueryDialog stateQueryDialog = new StateQueryDialog(FindStateQueryType);

                    if (stateQueryDialog.ShowDialog() == DialogResult.OK)
                    {
                        string stateCode = "";
                        string indexNo = "";
                        string viewName = "";

                        stateCode = stateQueryDialog.SelectedQueryType.ToString();
                        indexNo = stateDao.FindLedIndexNoByStateCode(stateCode).Rows[0]["ROW_INDEX"].ToString();
                        viewName = stateDao.FindLedIndexNoByStateCode(stateCode).Rows[0]["VIEWNAME"].ToString();

                        bsMain.DataSource = stateDao.FindLedStateByIndexNo(indexNo,viewName);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }

        private void GetOrderDataSet()
        {
            try
            {
                using (PersistentManager pm = new PersistentManager())
                {
                    StateDao stateDao = new StateDao();
                    DataTable FindStateQueryType = stateDao.FindOrderListTable();
                    StateQueryDialog stateQueryDialog = new StateQueryDialog(FindStateQueryType);

                    if (stateQueryDialog.ShowDialog() == DialogResult.OK)
                    {
                        string stateCode = "";
                        string indexNo = "";
                        string viewName = "";

                        stateCode = stateQueryDialog.SelectedQueryType.ToString();
                        indexNo = stateDao.FindOrderIndexNoByStateCode(stateCode).Rows[0]["ROW_INDEX"].ToString();
                        viewName = stateDao.FindOrderIndexNoByStateCode(stateCode).Rows[0]["VIEWNAME"].ToString();

                        bsMain.DataSource = stateDao.FindOrderStateByIndexNo(indexNo,viewName);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }

        private void btnLedRefresh_Click(object sender, EventArgs e)
        {
            GetLedDataSet();
        }

        private void btnOrderRefresh_Click(object sender, EventArgs e)
        {
            GetOrderDataSet();
        }

        private void btnExit_Click_1(object sender, EventArgs e)
        {
            Exit();
        }
    }
}