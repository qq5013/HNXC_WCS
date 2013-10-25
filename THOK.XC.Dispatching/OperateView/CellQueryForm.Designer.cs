
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
            this.PRODUCTCODE = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.PRODUCTNAME = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.QUANTITY = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.PRODUCTBARCODE = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.PALLETBARCODE = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.pnlChart = new System.Windows.Forms.Panel();
            this.sbShelf = new System.Windows.Forms.VScrollBar();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnChart = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.PColor = new System.Windows.Forms.Panel();
            this.pnlTool.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsMain)).BeginInit();
            this.pnlProgress.SuspendLayout();
            this.pnlData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).BeginInit();
            this.pnlChart.SuspendLayout();
            this.PColor.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTool
            // 
            this.pnlTool.Controls.Add(this.PColor);
            this.pnlTool.Controls.Add(this.btnExit);
            this.pnlTool.Controls.Add(this.btnChart);
            this.pnlTool.Controls.Add(this.btnRefresh);
            this.pnlTool.Size = new System.Drawing.Size(1051, 47);
            // 
            // pnlContent
            // 
            this.pnlContent.Controls.Add(this.pnlChart);
            this.pnlContent.Controls.Add(this.pnlData);
            this.pnlContent.Location = new System.Drawing.Point(0, 47);
            this.pnlContent.Size = new System.Drawing.Size(1051, 368);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(1051, 415);
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
            this.pnlData.Size = new System.Drawing.Size(1051, 132);
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
            dataGridViewCellStyle2.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
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
            dataGridViewCellStyle4.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMain.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvMain.RowHeadersWidth = 30;
            this.dgvMain.RowTemplate.Height = 23;
            this.dgvMain.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMain.Size = new System.Drawing.Size(1051, 132);
            this.dgvMain.TabIndex = 10;
            this.dgvMain.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvMain_RowPostPaint);
            // 
            // SHELFNAME
            // 
            this.SHELFNAME.DataPropertyName = "SHELF_NAME";
            this.SHELFNAME.FilteringEnabled = false;
            this.SHELFNAME.HeaderText = "货架";
            this.SHELFNAME.Name = "SHELFNAME";
            this.SHELFNAME.ReadOnly = true;
            this.SHELFNAME.Width = 70;
            // 
            // CELLCODE
            // 
            this.CELLCODE.DataPropertyName = "CELL_CODE";
            this.CELLCODE.FilteringEnabled = false;
            this.CELLCODE.HeaderText = "货位";
            this.CELLCODE.Name = "CELLCODE";
            this.CELLCODE.ReadOnly = true;
            this.CELLCODE.Width = 70;
            // 
            // BILLNO
            // 
            this.BILLNO.DataPropertyName = "BILL_NO";
            this.BILLNO.FilteringEnabled = false;
            this.BILLNO.HeaderText = "单据号";
            this.BILLNO.Name = "BILLNO";
            this.BILLNO.ReadOnly = true;
            // 
            // PRODUCTCODE
            // 
            this.PRODUCTCODE.DataPropertyName = "PRODUCT_CODE";
            this.PRODUCTCODE.FilteringEnabled = false;
            this.PRODUCTCODE.HeaderText = "产品代码";
            this.PRODUCTCODE.Name = "PRODUCTCODE";
            this.PRODUCTCODE.ReadOnly = true;
            this.PRODUCTCODE.Width = 110;
            // 
            // PRODUCTNAME
            // 
            this.PRODUCTNAME.DataPropertyName = "PRODUCT_NAME";
            this.PRODUCTNAME.FilteringEnabled = false;
            this.PRODUCTNAME.HeaderText = "产品名称";
            this.PRODUCTNAME.Name = "PRODUCTNAME";
            this.PRODUCTNAME.ReadOnly = true;
            this.PRODUCTNAME.Width = 150;
            // 
            // QUANTITY
            // 
            this.QUANTITY.DataPropertyName = "REAL_WEIGHT";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.QUANTITY.DefaultCellStyle = dataGridViewCellStyle3;
            this.QUANTITY.FilteringEnabled = false;
            this.QUANTITY.HeaderText = "重量";
            this.QUANTITY.Name = "QUANTITY";
            this.QUANTITY.ReadOnly = true;
            this.QUANTITY.Width = 80;
            // 
            // PRODUCTBARCODE
            // 
            this.PRODUCTBARCODE.DataPropertyName = "PRODUCT_BARCODE";
            this.PRODUCTBARCODE.FilteringEnabled = false;
            this.PRODUCTBARCODE.HeaderText = "产品条码";
            this.PRODUCTBARCODE.Name = "PRODUCTBARCODE";
            this.PRODUCTBARCODE.ReadOnly = true;
            this.PRODUCTBARCODE.Width = 250;
            // 
            // PALLETBARCODE
            // 
            this.PALLETBARCODE.DataPropertyName = "PALLET_CODE";
            this.PALLETBARCODE.FilteringEnabled = false;
            this.PALLETBARCODE.HeaderText = "托盘条码";
            this.PALLETBARCODE.Name = "PALLETBARCODE";
            this.PALLETBARCODE.ReadOnly = true;
            this.PALLETBARCODE.Width = 150;
            // 
            // pnlChart
            // 
            this.pnlChart.BackColor = System.Drawing.SystemColors.Info;
            this.pnlChart.Controls.Add(this.sbShelf);
            this.pnlChart.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlChart.Location = new System.Drawing.Point(0, 287);
            this.pnlChart.Name = "pnlChart";
            this.pnlChart.Size = new System.Drawing.Size(1051, 81);
            this.pnlChart.TabIndex = 1;
            this.pnlChart.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlChart_Paint);
            this.pnlChart.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pnlChart_MouseClick);
            this.pnlChart.MouseEnter += new System.EventHandler(this.pnlChart_MouseEnter);
            this.pnlChart.Resize += new System.EventHandler(this.pnlChart_Resize);
            // 
            // sbShelf
            // 
            this.sbShelf.Dock = System.Windows.Forms.DockStyle.Right;
            this.sbShelf.LargeChange = 30;
            this.sbShelf.Location = new System.Drawing.Point(1032, 0);
            this.sbShelf.Maximum = 160;
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
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Red;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Location = new System.Drawing.Point(7, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 23);
            this.label1.TabIndex = 37;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(478, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 38;
            this.label2.Text = "锁定的空货位";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(149, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 40;
            this.label3.Text = "有货且未锁定";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Blue;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Location = new System.Drawing.Point(119, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(28, 23);
            this.label4.TabIndex = 39;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(259, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 42;
            this.label5.Text = "有货且锁定";
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Green;
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.Location = new System.Drawing.Point(229, 7);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(28, 23);
            this.label6.TabIndex = 41;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(367, 12);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 44;
            this.label7.Text = "禁用货位";
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.Gray;
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label8.Location = new System.Drawing.Point(337, 7);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(28, 23);
            this.label8.TabIndex = 43;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(38, 13);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 46;
            this.label9.Text = "异常货位";
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.Yellow;
            this.label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label10.Location = new System.Drawing.Point(447, 7);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(28, 23);
            this.label10.TabIndex = 45;
            // 
            // PColor
            // 
            this.PColor.Controls.Add(this.label2);
            this.PColor.Controls.Add(this.label9);
            this.PColor.Controls.Add(this.label1);
            this.PColor.Controls.Add(this.label10);
            this.PColor.Controls.Add(this.label4);
            this.PColor.Controls.Add(this.label7);
            this.PColor.Controls.Add(this.label3);
            this.PColor.Controls.Add(this.label8);
            this.PColor.Controls.Add(this.label6);
            this.PColor.Controls.Add(this.label5);
            this.PColor.Location = new System.Drawing.Point(173, 4);
            this.PColor.Name = "PColor";
            this.PColor.Size = new System.Drawing.Size(559, 38);
            this.PColor.TabIndex = 47;
            // 
            // CellQueryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(1051, 415);
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
            this.PColor.ResumeLayout(false);
            this.PColor.PerformLayout();
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
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn PRODUCTCODE;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn PRODUCTNAME;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn QUANTITY;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn PRODUCTBARCODE;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn PALLETBARCODE;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel PColor;
    }
}
