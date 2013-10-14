using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using THOK.XC.Process.Dal;

namespace THOK.XC.Dispatching.OperateView
{
    public partial class CellQueryForm :THOK.AF.View.ToolbarForm
    {
        private Dictionary<int, DataRow[]> shelf = new Dictionary<int, DataRow[]>();

        private DataTable cellTable = null;
        private CellDal cellDal = new CellDal();
        private bool needDraw = false;
        private bool filtered = false;

        private int columns = 38;
        private int rows = 13;
        private int cellWidth = 0;
        private int cellHeight = 0;
        private int currentPage = 1;
        private int[] top = new int[2];
        private int left = 5;

        public CellQueryForm()
        {
            InitializeComponent();

            //设置双缓冲
            SetStyle(ControlStyles.DoubleBuffer |
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint, true);

           THOKUtil.EnableFilter(dgvMain);
            pnlData.Visible = true;
            pnlData.Dock = DockStyle.Fill;

            pnlChart.Visible = false;
            pnlChart.Dock = DockStyle.Fill;

            pnlChart.MouseWheel += new MouseEventHandler(pnlChart_MouseWheel);
        }

        
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                if (bsMain.Filter.Trim().Length != 0)
                {
                    DialogResult result = MessageBox.Show("重新读入数据请选择'是(Y)',清除过滤条件请选择'否(N)'", "询问", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    switch (result)
                    {
                        case DialogResult.No:
                            DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn.RemoveFilter(dgvMain);
                            return;
                        case DialogResult.Cancel:
                            return;

                    }
                }

                btnRefresh.Enabled = false;
                btnChart.Enabled = false;

                pnlProgress.Top = (pnlMain.Height - pnlProgress.Height) / 3;
                pnlProgress.Left = (pnlMain.Width - pnlProgress.Width) / 2;
                pnlProgress.Visible = true;
                Application.DoEvents();

                cellTable = cellDal.GetCell();
                bsMain.DataSource = cellTable;

                pnlProgress.Visible = false;
                btnRefresh.Enabled = true;
                btnChart.Enabled = true;
            }
            catch (Exception exp)
            {
                THOKUtil.ShowInfo("读入数据失败，原因：" + exp.Message);
            }
        }

        private void btnChart_Click(object sender, EventArgs e)
        {
            if (cellTable != null && cellTable.Rows.Count != 0)
            {
                if (pnlData.Visible)
                {
                    filtered = bsMain.Filter != null;
                    needDraw = true;
                    btnRefresh.Enabled = false;
                    pnlData.Visible = false;
                    pnlChart.Visible = true;
                    btnChart.Text = "列表";
                }
                else
                {
                    needDraw = false;
                    btnRefresh.Enabled = true;
                    pnlData.Visible = true;
                    pnlChart.Visible = false;
                    btnChart.Text = "图形";
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Exit();
        }

        private void pnlChart_Paint(object sender, PaintEventArgs e)
        {
            if (needDraw)
            {
                Font font = new Font("宋体", 9);
                SizeF size = e.Graphics.MeasureString("第1排第13层", font);
                float adjustHeight = Math.Abs(size.Height - cellHeight) / 2;
                size = e.Graphics.MeasureString("13", font);
                float adjustWidth = (cellWidth - size.Width) / 2;

                for (int i = 0; i <= 1; i++)
                {
                    int key = currentPage * 2 + i - 1;
                    if (!shelf.ContainsKey(key))
                    {
                        DataRow[] rows = cellTable.Select(string.Format("SHELF='{0}'", key), "CELLCODE");
                        shelf.Add(key, rows);
                    }

                    DrawShelf(shelf[key], e.Graphics, top[i], font, adjustWidth);

                    int tmpLeft = left + columns * cellWidth + 5;

                    for (int j = 0; j < rows; j++)
                    {
                        string s = string.Format("第{0}排第{1}层", key, Convert.ToString(rows - j).PadLeft(2, '0'));
                        e.Graphics.DrawString(s, font, Brushes.DarkCyan, tmpLeft, top[i] + (j + 1) * cellHeight + adjustHeight);
                    }                    
                }

                if (filtered)
                {
                    int i = currentPage * 2;
                    foreach (DataGridViewRow gridRow in dgvMain.Rows)
                    {                        
                        DataRowView cellRow = (DataRowView)gridRow.DataBoundItem;
                        int shelf = Convert.ToInt32(cellRow["SHELF"]);

                        if (shelf == i || shelf == i - 1)
                        {
                            int top = 0;
                            if (shelf % 2 == 0)
                                top = pnlContent.Height / 2;

                            int column = Convert.ToInt32(cellRow["CELLCOLUMN"]) - 1;
                            int row = rows - Convert.ToInt32(cellRow["CELLROW"]) + 1;
                            int quantity = Convert.ToInt32(cellRow["QUANTITY"]);
                            FillCell(e.Graphics, top, row, column, quantity);
                        }
                    }
                }
            }
        }

        private void DrawShelf(DataRow[] cellRows, Graphics g, int top, Font font, float adjustWidth)
        {
            for (int j = 0; j < columns; j++)
            {
                g.DrawString(Convert.ToString(j + 1), font, Brushes.DarkCyan, left + j * cellWidth + adjustWidth, top);
            }

            foreach (DataRow cellRow in cellRows)
            {
                int column = Convert.ToInt32(cellRow["CELLCOLUMN"]) - 1;
                int row = rows - Convert.ToInt32(cellRow["CELLROW"]) + 1;
                int quantity = Convert.ToInt32(cellRow["QUANTITY"]);

                int x = left + column * cellWidth;
                int y = top + row * cellHeight;

                g.DrawRectangle(Pens.DarkCyan, new Rectangle(x, y, cellWidth, cellHeight));

                if (!filtered)
                {                  
                    FillCell(g, top, row, column, quantity);                        
                }
            }
        }

        private void FillCell(Graphics g, int top, int row, int column, int quantity)
        {
            int x = left + column * cellWidth;
            int y = top + row * cellHeight;

            if (quantity >= 200)
                g.FillRectangle(Brushes.Blue, new Rectangle(x + 2, y + 2, cellWidth - 3, cellHeight - 3));
            else if (quantity > 0)
                g.FillRectangle(Brushes.Tomato, new Rectangle(x + 2, y + 2, cellWidth - 3, cellHeight - 3));  
        }

        private void pnlChart_Resize(object sender, EventArgs e)
        {
            cellWidth = (pnlContent.Width - 90 - sbShelf.Width - 20) / columns;
            cellHeight = (pnlContent.Height / 2) / (rows + 2);

            top[0] = 0;
            top[1] = pnlContent.Height / 2;
        }

        private void pnlChart_MouseClick(object sender, MouseEventArgs e)
        {
            int i = e.Y < top[1] ? 0 : 1;
            int shelf = currentPage * 2 + i- 1;

            int column = (e.X - left) / cellWidth + 1;
            int row = rows - (e.Y - top[i]) / cellHeight + 1;

            if (column <= columns && row <= rows)
            {
                DataRow[] cellRows = cellTable.Select(string.Format("SHELF='{0}' AND CELLCOLUMN='{1}' AND CELLROW='{2}'", shelf, column, row));
                if (cellRows.Length != 0)
                {
                    Dictionary<string, Dictionary<string, object>> properties = new Dictionary<string, Dictionary<string, object>>();
                    Dictionary<string, object> property = new Dictionary<string, object>();
                    property.Add("产品名称", cellRows[0]["PRODUCTNAME"]);
                    property.Add("重量", cellRows[0]["QUANTITY"]);
                    property.Add("条码", cellRows[0]["PRODUCTBARCODE"]);
                    property.Add("托盘", cellRows[0]["PALLETBARCODE"]);
                    property.Add("批次号", cellRows[0]["SCHEDULENO"]);
                    property.Add("单据号", cellRows[0]["BILLNO"]);
                    property.Add("入库时间", cellRows[0]["INDATE"]);
                    properties.Add("产品信息", property);
                    
                    property = new Dictionary<string, object>();
                    property.Add("库区名称", cellRows[0]["AREANAME"]);
                    property.Add("货架名称", cellRows[0]["SHELFNAME"]);
                    property.Add("列", column);
                    property.Add("层", row);
                    property.Add("状态", cellRows[0]["ISLOCK"].ToString() == "0" ? "正常" : "锁定");
                    properties.Add("货位信息", property);

                    CellDialog cellDialog = new CellDialog(properties);
                    cellDialog.ShowDialog();
                }
            }
        }

        private void pnlChart_MouseEnter(object sender, EventArgs e)
        {
            pnlChart.Focus();
        }

        private void pnlChart_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta < 0 && currentPage + 1 <= 3)
                sbShelf.Value = (currentPage) * 30;
            else if (e.Delta > 0 && currentPage - 1 >= 1)
                sbShelf.Value = (currentPage - 2) * 30;         
        }

        private void sbShelf_ValueChanged(object sender, EventArgs e)
        {
            int pos = sbShelf.Value / 30 + 1;
            if (pos != currentPage)
            {
                currentPage = pos;
                pnlChart.Invalidate();
            }
        }

        private void dgvMain_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X, e.RowBounds.Location.Y, dgvMain.RowHeadersWidth - 4, e.RowBounds.Height);
            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), dgvMain.RowHeadersDefaultCellStyle.Font, rectangle, dgvMain.RowHeadersDefaultCellStyle.ForeColor, TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }
    }
}

