using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;

namespace THOK.XC.Dispatching
{
    public class THOKUtil
    {
        public static string ToDBC(string s)
        {
            char[] c = s.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                c[i] = ToDBC(c[i]);
            }
            return new string(c);
        }

        /// 全角转半角的函数
        public static char ToDBC(char c)
        {
            if (c == 12288)
                return (char)32;
            else if (c > 65280 && c < 65375)
                return (char)(c - 65248);
            else
                return c;
        }

        public static DialogResult ShowInfo(string msg)
        {
            return MessageBox.Show(msg, "消息", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static DialogResult ShowError(string msg)
        {
            return MessageBox.Show(msg, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static DialogResult ShowQuery(string msg)
        {
            return MessageBox.Show(msg, "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        public static DialogResult ShowQuery2(string msg)
        {
            return MessageBox.Show(msg, "询问", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
        }

        public static DialogResult ShowWarning(string msg)
        {
            return MessageBox.Show(msg, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static void EnableFilter(DataGridView gridView)
        {
            if (gridView.DataSource is BindingSource)
                ((BindingSource)gridView.DataSource).Filter = "";

            foreach (DataGridViewColumn column in gridView.Columns)
            {
                if (column is DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn)
                    ((DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn)column).FilteringEnabled = true;
            }
        }

        public static DataTable GetEmptyBillMaster()
        {
            DataTable table = new DataTable();
            table.Columns.Add("BILLNO");
            table.Columns.Add("BILLDATE");
            table.Columns.Add("BILLTYPE");
            table.Columns.Add("BILLNAME");
            table.Columns.Add("SCHEDULENO");
            table.Columns.Add("WAREHOUSECODE");
            table.Columns.Add("WAREHOUSENAME");
            table.Columns.Add("TARGET");
            table.Columns.Add("STATUS");
            table.Columns.Add("STATUSNAME");
            table.Columns.Add("STATE");
            table.Columns.Add("STATEDESC");
            table.Columns.Add("ORIBILLNO");
            table.Columns.Add("OPERATER");
            table.Columns.Add("USERNAME");
            table.Columns.Add("OPERATEDATE");
            table.Columns.Add("CHECKER");
            table.Columns.Add("CHECKDATE");
            table.Columns.Add("SENDER");
            table.Columns.Add("SENDEDATE");
            table.Columns.Add("TASKER");
            table.Columns.Add("TASKDATE");

            return table;
        }

        public static DataTable GetEmptyBillDetail()
        {
            DataTable table = new DataTable();
            table.Columns.Add("BILLNO");
            table.Columns.Add("ITEMNO");
            table.Columns.Add("PRODUCTCODE");
            table.Columns.Add("PRODUCTNAME");
            table.Columns.Add("QUANTITY");
            table.Columns.Add("REALQUANTITY");
            table.Columns.Add("PACKAGECOUNT");
            table.Columns.Add("NCCOUNT");

            return table;
        }

        public static DataTable GetEmptyProductState()
        {
            DataTable table = new DataTable();
            table.Columns.Add("BILLNO");
            table.Columns.Add("ITEMNO");
            table.Columns.Add("SCHEDULENO");
            table.Columns.Add("PRODUCTCODE");
            table.Columns.Add("PRODUCTNAME");
            table.Columns.Add("QUANTITY");
            table.Columns.Add("REALQUANTITY");
            table.Columns.Add("CELLCODE");
            table.Columns.Add("CELLNAME");
            table.Columns.Add("NEWCELLCODE");
            table.Columns.Add("NEWCELL");
            table.Columns.Add("BARCODE");
            table.Columns.Add("PALLETBARCODE");
            return table;
        }

        public static DataTable GetEmptyReportMaster()
        {
            DataTable table = new DataTable();
            table.Columns.Add("BEGINID");
            table.Columns.Add("ENDID");
            table.Columns.Add("WAREHOUSENAME");
            table.Columns.Add("PRODUCTNAME");
            table.Columns.Add("BEGINQUANTITY");
            table.Columns.Add("INQUANTITY");
            table.Columns.Add("OUTQUANTITY");
            table.Columns.Add("QUANTITY");
            table.Columns.Add("ENDQUANTITY");
            return table;
        }

        public static DataTable GetEmptyReportDetail()
        {
            DataTable table = new DataTable();
            table.Columns.Add("BILLDATE");
            table.Columns.Add("WAREHOUSENAME");
            table.Columns.Add("BILLNO");
            table.Columns.Add("PRODUCTCODE");
            table.Columns.Add("PRODUCTNAME");            
            table.Columns.Add("INQUANTITY");
            table.Columns.Add("OUTQUANTITY");
            table.Columns.Add("ENDQUANTITY");
            return table;
        }

        public static DataTable GetEmptyPalletMaster()
        {
            DataTable table = new DataTable();
            table.Columns.Add("BILLNO");
            table.Columns.Add("BILLDATE");
            table.Columns.Add("BILLTYPE");
            table.Columns.Add("BILLNAME");
            table.Columns.Add("WAREHOUSECODE");
            table.Columns.Add("WAREHOUSENAME");
            table.Columns.Add("TARGET");
            table.Columns.Add("STATUS");
            table.Columns.Add("STATUSNAME");
            table.Columns.Add("STATE");
            table.Columns.Add("STATEDESC");
            table.Columns.Add("USERNAME");
            table.Columns.Add("OPERATEDATE");
            table.Columns.Add("CHECKER");
            table.Columns.Add("TASKER");
            table.Columns.Add("TASKDATE");

            return table;
        }

        public static DataTable GetEmptyPalletDetail()
        {
            DataTable table = new DataTable();
            table.Columns.Add("BILLNO");
            table.Columns.Add("ITEMNO");
            table.Columns.Add("PRODUCTCODE");
            table.Columns.Add("PRODUCTNAME");
            table.Columns.Add("QUANTITY");
            table.Columns.Add("PACKAGECOUNT");

            return table;
        }
    }
}
