namespace THOK.XC.Dispatching.View
{
    partial class StockInTaskForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.dgvMain = new System.Windows.Forms.DataGridView();
            this.BATCHNO = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.CHANNELCODE = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.CHANNELNAME = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.CIGARETTECODE = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.CIGARETTENAME = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.QUANTITY = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.INQUANTITY = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.T1 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.T2 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.T3 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.STATE = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.BARCODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bsMain = new System.Windows.Forms.BindingSource(this.components);
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.pnlTool.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsMain)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlTool
            // 
            this.pnlTool.Controls.Add(this.btnExit);
            this.pnlTool.Controls.Add(this.btnPrint);
            this.pnlTool.Controls.Add(this.btnRefresh);
            this.pnlTool.Size = new System.Drawing.Size(686, 53);
            // 
            // pnlContent
            // 
            this.pnlContent.Controls.Add(this.dgvMain);
            this.pnlContent.Size = new System.Drawing.Size(686, 221);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(686, 274);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnRefresh.Image = global::THOK.XC.Dispatching.Properties.Resources.Chart;
            this.btnRefresh.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnRefresh.Location = new System.Drawing.Point(0, 0);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(48, 51);
            this.btnRefresh.TabIndex = 17;
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // dgvMain
            // 
            this.dgvMain.AllowUserToAddRows = false;
            this.dgvMain.AllowUserToDeleteRows = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dgvMain.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvMain.AutoGenerateColumns = false;
            this.dgvMain.BackgroundColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMain.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMain.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.BATCHNO,
            this.CHANNELCODE,
            this.CHANNELNAME,
            this.CIGARETTECODE,
            this.CIGARETTENAME,
            this.QUANTITY,
            this.INQUANTITY,
            this.T1,
            this.T2,
            this.T3,
            this.STATE,
            this.BARCODE});
            this.dgvMain.DataSource = this.bsMain;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvMain.DefaultCellStyle = dataGridViewCellStyle7;
            this.dgvMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMain.Location = new System.Drawing.Point(0, 0);
            this.dgvMain.MultiSelect = false;
            this.dgvMain.Name = "dgvMain";
            this.dgvMain.ReadOnly = true;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMain.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.dgvMain.RowHeadersWidth = 30;
            this.dgvMain.RowTemplate.Height = 23;
            this.dgvMain.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMain.Size = new System.Drawing.Size(686, 221);
            this.dgvMain.TabIndex = 3;
            // 
            // BATCHNO
            // 
            this.BATCHNO.DataPropertyName = "BATCHNO";
            this.BATCHNO.FilteringEnabled = false;
            this.BATCHNO.HeaderText = "入库批次";
            this.BATCHNO.Name = "BATCHNO";
            this.BATCHNO.ReadOnly = true;
            this.BATCHNO.Width = 80;
            // 
            // CHANNELCODE
            // 
            this.CHANNELCODE.DataPropertyName = "CHANNELCODE";
            this.CHANNELCODE.FilteringEnabled = false;
            this.CHANNELCODE.HeaderText = "烟道代码";
            this.CHANNELCODE.Name = "CHANNELCODE";
            this.CHANNELCODE.ReadOnly = true;
            this.CHANNELCODE.Visible = false;
            // 
            // CHANNELNAME
            // 
            this.CHANNELNAME.DataPropertyName = "CHANNELNAME";
            this.CHANNELNAME.FilteringEnabled = false;
            this.CHANNELNAME.HeaderText = "烟道名称";
            this.CHANNELNAME.Name = "CHANNELNAME";
            this.CHANNELNAME.ReadOnly = true;
            this.CHANNELNAME.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // CIGARETTECODE
            // 
            this.CIGARETTECODE.DataPropertyName = "CIGARETTECODE";
            this.CIGARETTECODE.FilteringEnabled = false;
            this.CIGARETTECODE.HeaderText = "卷烟代码";
            this.CIGARETTECODE.Name = "CIGARETTECODE";
            this.CIGARETTECODE.ReadOnly = true;
            // 
            // CIGARETTENAME
            // 
            this.CIGARETTENAME.DataPropertyName = "CIGARETTENAME";
            this.CIGARETTENAME.FilteringEnabled = false;
            this.CIGARETTENAME.HeaderText = "卷烟名称";
            this.CIGARETTENAME.Name = "CIGARETTENAME";
            this.CIGARETTENAME.ReadOnly = true;
            // 
            // QUANTITY
            // 
            this.QUANTITY.DataPropertyName = "QUANTITY";
            this.QUANTITY.FilteringEnabled = false;
            this.QUANTITY.HeaderText = "请求数量";
            this.QUANTITY.Name = "QUANTITY";
            this.QUANTITY.ReadOnly = true;
            // 
            // INQUANTITY
            // 
            this.INQUANTITY.DataPropertyName = "INQUANTITY";
            this.INQUANTITY.FilteringEnabled = false;
            this.INQUANTITY.HeaderText = "入库数量";
            this.INQUANTITY.Name = "INQUANTITY";
            this.INQUANTITY.ReadOnly = true;
            // 
            // T1
            // 
            this.T1.DataPropertyName = "T1";
            this.T1.FilteringEnabled = false;
            this.T1.HeaderText = "请求时间";
            this.T1.Name = "T1";
            this.T1.ReadOnly = true;
            // 
            // T2
            // 
            this.T2.DataPropertyName = "T2";
            this.T2.FilteringEnabled = false;
            this.T2.HeaderText = "入库时间";
            this.T2.Name = "T2";
            this.T2.ReadOnly = true;
            // 
            // T3
            // 
            this.T3.DataPropertyName = "T3";
            this.T3.FilteringEnabled = false;
            this.T3.HeaderText = "完成时间";
            this.T3.Name = "T3";
            this.T3.ReadOnly = true;
            // 
            // STATE
            // 
            this.STATE.DataPropertyName = "STRSTATE";
            this.STATE.FilteringEnabled = false;
            this.STATE.HeaderText = "状态";
            this.STATE.Name = "STATE";
            this.STATE.ReadOnly = true;
            this.STATE.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // BARCODE
            // 
            this.BARCODE.DataPropertyName = "BARCODE";
            this.BARCODE.HeaderText = "Column1";
            this.BARCODE.Name = "BARCODE";
            this.BARCODE.ReadOnly = true;
            this.BARCODE.Visible = false;
            // 
            // btnPrint
            // 
            this.btnPrint.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnPrint.Image = global::THOK.XC.Dispatching.Properties.Resources.Info;
            this.btnPrint.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnPrint.Location = new System.Drawing.Point(48, 0);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(48, 51);
            this.btnPrint.TabIndex = 23;
            this.btnPrint.Text = "打印";
            this.btnPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnExit
            // 
            this.btnExit.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnExit.Image = global::THOK.XC.Dispatching.Properties.Resources.Exit;
            this.btnExit.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnExit.Location = new System.Drawing.Point(96, 0);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(48, 51);
            this.btnExit.TabIndex = 24;
            this.btnExit.Text = "退出";
            this.btnExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // StockInTaskForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(686, 274);
            this.Name = "StockInTaskForm";
            this.pnlTool.ResumeLayout(false);
            this.pnlContent.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.DataGridView dgvMain;
        private System.Windows.Forms.BindingSource bsMain;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn BATCHNO;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn CHANNELCODE;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn CHANNELNAME;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn CIGARETTECODE;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn CIGARETTENAME;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn QUANTITY;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn INQUANTITY;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn T1;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn T2;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn T3;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn STATE;
        private System.Windows.Forms.DataGridViewTextBoxColumn BARCODE;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnPrint;
    }
}
