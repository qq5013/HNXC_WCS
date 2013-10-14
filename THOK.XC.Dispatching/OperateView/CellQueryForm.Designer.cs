
namespace THOK.XC.Dispatching.OperateView
{
    partial class CellQueryForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CellQueryForm));
            this.bsMain = new System.Windows.Forms.BindingSource(this.components);
            this.pnlProgress = new System.Windows.Forms.Panel();
            this.lblInfo = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.pnlData = new System.Windows.Forms.Panel();
            this.dgvMain = new System.Windows.Forms.DataGridView();
            this.SHELFNAME = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.CELLCODE = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.BILLNO = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.SCHEDULENO = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.PRODUCTCODE = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.PRODUCTNAME = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.QUANTITY = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.PRODUCTBARCODE = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.PALLETBARCODE = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.pnlChart = new System.Windows.Forms.Panel();
            this.sbShelf = new System.Windows.Forms.VScrollBar();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnChart = new System.Windows.Forms.Button();
            this.pnlTool.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsMain)).BeginInit();
            this.pnlProgress.SuspendLayout();
            this.pnlData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).BeginInit();
            this.pnlChart.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTool
            // 
            this.pnlTool.Controls.Add(this.btnExit);
            this.pnlTool.Controls.Add(this.btnChart);
            this.pnlTool.Controls.Add(this.btnRefresh);
            this.pnlTool.Size = new System.Drawing.Size(662, 47);
            // 
            // pnlContent
            // 
            this.pnlContent.Controls.Add(this.pnlChart);
            this.pnlContent.Controls.Add(this.pnlData);
            this.pnlContent.Location = new System.Drawing.Point(0, 47);
            this.pnlContent.Size = new System.Drawing.Size(662, 235);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(662, 282);
            // 
            // pnlProgress
            // 
            this.pnlProgress.Controls.Add(this.lblInfo);
            this.pnlProgress.Location = new System.Drawing.Point(250, 18);
            this.pnlProgress.Name = "pnlProgress";
            this.pnlProgress.Size = new System.Drawing.Size(238, 79);
            this.pnlProgress.TabIndex = 10;
            this.pnlProgress.Visible = false;
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(32, 34);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(167, 12);
            this.lblInfo.TabIndex = 1;
            this.lblInfo.Text = "正在准备货位数据，请稍候...";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnRefresh.Image = global::THOK.XC.Dispatching.Properties.Resources.Find;
            this.btnRefresh.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnRefresh.Location = new System.Drawing.Point(0, 0);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(48, 45);
            this.btnRefresh.TabIndex = 30;
            this.btnRefresh.Text = "查询";
            this.btnRefresh.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // pnlData
            // 
            this.pnlData.Controls.Add(this.pnlProgress);
            this.pnlData.Controls.Add(this.dgvMain);
            this.pnlData.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlData.Location = new System.Drawing.Point(0, 0);
            this.pnlData.Name = "pnlData";
            this.pnlData.Size = new System.Drawing.Size(662, 132);
            this.pnlData.TabIndex = 0;
            // 
            // dgvMain
            // 
            this.dgvMain.AllowUserToAddRows = false;
            this.dgvMain.AllowUserToDeleteRows = false;
            this.dgvMain.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.dgvMain.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvMain.AutoGenerateColumns = false;
            this.dgvMain.BackgroundColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMain.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMain.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SHELFNAME,
            this.CELLCODE,
            this.BILLNO,
            this.SCHEDULENO,
            this.PRODUCTCODE,
            this.PRODUCTNAME,
            this.QUANTITY,
            this.PRODUCTBARCODE,
            this.PALLETBARCODE});
            this.dgvMain.DataSource = this.bsMain;
            this.dgvMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMain.Location = new System.Drawing.Point(0, 0);
            this.dgvMain.MultiSelect = false;
            this.dgvMain.Name = "dgvMain";
            this.dgvMain.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMain.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvMain.RowHeadersWidth = 30;
            this.dgvMain.RowTemplate.Height = 23;
            this.dgvMain.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMain.Size = new System.Drawing.Size(662, 132);
            this.dgvMain.TabIndex = 10;
            this.dgvMain.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvMain_RowPostPaint);
            // 
            // SHELFNAME
            // 
            this.SHELFNAME.DataPropertyName = "SHELFNAME";
            this.SHELFNAME.FilteringEnabled = false;
            this.SHELFNAME.HeaderText = "货架";
            this.SHELFNAME.Name = "SHELFNAME";
            this.SHELFNAME.ReadOnly = true;
            this.SHELFNAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.SHELFNAME.Width = 70;
            // 
            // CELLCODE
            // 
            this.CELLCODE.DataPropertyName = "CELLCODE";
            this.CELLCODE.FilteringEnabled = false;
            this.CELLCODE.HeaderText = "货位";
            this.CELLCODE.Name = "CELLCODE";
            this.CELLCODE.ReadOnly = true;
            this.CELLCODE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.CELLCODE.Width = 70;
            // 
            // BILLNO
            // 
            this.BILLNO.DataPropertyName = "BILLNO";
            this.BILLNO.FilteringEnabled = false;
            this.BILLNO.HeaderText = "单据号";
            this.BILLNO.Name = "BILLNO";
            this.BILLNO.ReadOnly = true;
            this.BILLNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // SCHEDULENO
            // 
            this.SCHEDULENO.DataPropertyName = "SCHEDULENO";
            this.SCHEDULENO.FilteringEnabled = false;
            this.SCHEDULENO.HeaderText = "生产批次";
            this.SCHEDULENO.Name = "SCHEDULENO";
            this.SCHEDULENO.ReadOnly = true;
            this.SCHEDULENO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // PRODUCTCODE
            // 
            this.PRODUCTCODE.DataPropertyName = "PRODUCTCODE";
            this.PRODUCTCODE.FilteringEnabled = false;
            this.PRODUCTCODE.HeaderText = "产品代码";
            this.PRODUCTCODE.Name = "PRODUCTCODE";
            this.PRODUCTCODE.ReadOnly = true;
            this.PRODUCTCODE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.PRODUCTCODE.Width = 110;
            // 
            // PRODUCTNAME
            // 
            this.PRODUCTNAME.DataPropertyName = "PRODUCTNAME";
            this.PRODUCTNAME.FilteringEnabled = false;
            this.PRODUCTNAME.HeaderText = "产品名称";
            this.PRODUCTNAME.Name = "PRODUCTNAME";
            this.PRODUCTNAME.ReadOnly = true;
            this.PRODUCTNAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.PRODUCTNAME.Width = 150;
            // 
            // QUANTITY
            // 
            this.QUANTITY.DataPropertyName = "QUANTITY";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.QUANTITY.DefaultCellStyle = dataGridViewCellStyle3;
            this.QUANTITY.FilteringEnabled = false;
            this.QUANTITY.HeaderText = "重量";
            this.QUANTITY.Name = "QUANTITY";
            this.QUANTITY.ReadOnly = true;
            this.QUANTITY.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.QUANTITY.Width = 80;
            // 
            // PRODUCTBARCODE
            // 
            this.PRODUCTBARCODE.DataPropertyName = "PRODUCTBARCODE";
            this.PRODUCTBARCODE.FilteringEnabled = false;
            this.PRODUCTBARCODE.HeaderText = "产品条码";
            this.PRODUCTBARCODE.Name = "PRODUCTBARCODE";
            this.PRODUCTBARCODE.ReadOnly = true;
            this.PRODUCTBARCODE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.PRODUCTBARCODE.Width = 250;
            // 
            // PALLETBARCODE
            // 
            this.PALLETBARCODE.DataPropertyName = "PALLETBARCODE";
            this.PALLETBARCODE.FilteringEnabled = false;
            this.PALLETBARCODE.HeaderText = "托盘条码";
            this.PALLETBARCODE.Name = "PALLETBARCODE";
            this.PALLETBARCODE.ReadOnly = true;
            this.PALLETBARCODE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.PALLETBARCODE.Width = 150;
            // 
            // pnlChart
            // 
            this.pnlChart.BackColor = System.Drawing.SystemColors.Info;
            this.pnlChart.Controls.Add(this.sbShelf);
            this.pnlChart.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlChart.Location = new System.Drawing.Point(0, 154);
            this.pnlChart.Name = "pnlChart";
            this.pnlChart.Size = new System.Drawing.Size(662, 81);
            this.pnlChart.TabIndex = 1;
            this.pnlChart.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlChart_Paint);
            this.pnlChart.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pnlChart_MouseClick);
            this.pnlChart.Resize += new System.EventHandler(this.pnlChart_Resize);
            this.pnlChart.MouseEnter += new System.EventHandler(this.pnlChart_MouseEnter);
            // 
            // sbShelf
            // 
            this.sbShelf.Dock = System.Windows.Forms.DockStyle.Right;
            this.sbShelf.LargeChange = 30;
            this.sbShelf.Location = new System.Drawing.Point(643, 0);
            this.sbShelf.Maximum = 90;
            this.sbShelf.Name = "sbShelf";
            this.sbShelf.Size = new System.Drawing.Size(19, 81);
            this.sbShelf.SmallChange = 30;
            this.sbShelf.TabIndex = 0;
            this.sbShelf.Value = 1;
            this.sbShelf.ValueChanged += new System.EventHandler(this.sbShelf_ValueChanged);
            // 
            // btnExit
            // 
            this.btnExit.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnExit.Location = new System.Drawing.Point(96, 0);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(48, 45);
            this.btnExit.TabIndex = 36;
            this.btnExit.Text = "退出";
            this.btnExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnChart
            // 
            this.btnChart.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnChart.Image = global::THOK.XC.Dispatching.Properties.Resources.PieChart;
            this.btnChart.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnChart.Location = new System.Drawing.Point(48, 0);
            this.btnChart.Name = "btnChart";
            this.btnChart.Size = new System.Drawing.Size(48, 45);
            this.btnChart.TabIndex = 35;
            this.btnChart.Text = "图形";
            this.btnChart.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnChart.UseVisualStyleBackColor = true;
            this.btnChart.Click += new System.EventHandler(this.btnChart_Click);
            // 
            // CellQueryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(662, 282);
            this.Name = "CellQueryForm";
            this.pnlTool.ResumeLayout(false);
            this.pnlContent.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bsMain)).EndInit();
            this.pnlProgress.ResumeLayout(false);
            this.pnlProgress.PerformLayout();
            this.pnlData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).EndInit();
            this.pnlChart.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.BindingSource bsMain;
        private System.Windows.Forms.Panel pnlProgress;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Panel pnlChart;
        private System.Windows.Forms.Panel pnlData;
        protected System.Windows.Forms.DataGridView dgvMain;
        private System.Windows.Forms.VScrollBar sbShelf;
        protected System.Windows.Forms.Button btnExit;
        protected System.Windows.Forms.Button btnChart;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn SHELFNAME;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn CELLCODE;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn BILLNO;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn SCHEDULENO;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn PRODUCTCODE;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn PRODUCTNAME;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn QUANTITY;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn PRODUCTBARCODE;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn PALLETBARCODE;
    }
}
