namespace THOK.XC.Dispatching.View
{
    partial class OrderStateForm
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
            this.dgvMain = new System.Windows.Forms.DataGridView();
            this.bsMain = new System.Windows.Forms.BindingSource(this.components);
            this.btnScannerRefresh = new System.Windows.Forms.Button();
            this.btnLedRefresh = new System.Windows.Forms.Button();
            this.btnOrderRefresh = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.Column1 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.Column7 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.Column2 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.Column3 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.Column4 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.Column6 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.Column5 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.Column8 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
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
            this.pnlTool.Controls.Add(this.btnOrderRefresh);
            this.pnlTool.Controls.Add(this.btnLedRefresh);
            this.pnlTool.Controls.Add(this.btnScannerRefresh);
            this.pnlTool.Size = new System.Drawing.Size(953, 53);
            // 
            // pnlContent
            // 
            this.pnlContent.Controls.Add(this.dgvMain);
            this.pnlContent.Size = new System.Drawing.Size(953, 293);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(953, 346);
            // 
            // dgvMain
            // 
            this.dgvMain.AllowUserToAddRows = false;
            this.dgvMain.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
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
            this.Column1,
            this.Column7,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column6,
            this.Column5,
            this.Column8});
            this.dgvMain.DataSource = this.bsMain;
            this.dgvMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMain.Location = new System.Drawing.Point(0, 0);
            this.dgvMain.MultiSelect = false;
            this.dgvMain.Name = "dgvMain";
            this.dgvMain.ReadOnly = true;
            this.dgvMain.RowHeadersWidth = 30;
            this.dgvMain.RowTemplate.Height = 23;
            this.dgvMain.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMain.Size = new System.Drawing.Size(953, 293);
            this.dgvMain.TabIndex = 3;
            // 
            // btnScannerRefresh
            // 
            this.btnScannerRefresh.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnScannerRefresh.Image = global::THOK.XC.Dispatching.Properties.Resources.Chart;
            this.btnScannerRefresh.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnScannerRefresh.Location = new System.Drawing.Point(0, 0);
            this.btnScannerRefresh.Name = "btnScannerRefresh";
            this.btnScannerRefresh.Size = new System.Drawing.Size(48, 51);
            this.btnScannerRefresh.TabIndex = 17;
            this.btnScannerRefresh.Text = "扫码";
            this.btnScannerRefresh.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnScannerRefresh.UseVisualStyleBackColor = true;
            this.btnScannerRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnLedRefresh
            // 
            this.btnLedRefresh.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnLedRefresh.Image = global::THOK.XC.Dispatching.Properties.Resources.Chart;
            this.btnLedRefresh.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnLedRefresh.Location = new System.Drawing.Point(48, 0);
            this.btnLedRefresh.Name = "btnLedRefresh";
            this.btnLedRefresh.Size = new System.Drawing.Size(48, 51);
            this.btnLedRefresh.TabIndex = 19;
            this.btnLedRefresh.Text = "LED";
            this.btnLedRefresh.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnLedRefresh.UseVisualStyleBackColor = true;
            this.btnLedRefresh.Click += new System.EventHandler(this.btnLedRefresh_Click);
            // 
            // btnOrderRefresh
            // 
            this.btnOrderRefresh.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnOrderRefresh.Image = global::THOK.XC.Dispatching.Properties.Resources.Chart;
            this.btnOrderRefresh.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnOrderRefresh.Location = new System.Drawing.Point(96, 0);
            this.btnOrderRefresh.Name = "btnOrderRefresh";
            this.btnOrderRefresh.Size = new System.Drawing.Size(48, 51);
            this.btnOrderRefresh.TabIndex = 20;
            this.btnOrderRefresh.Text = "订单";
            this.btnOrderRefresh.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnOrderRefresh.UseVisualStyleBackColor = true;
            this.btnOrderRefresh.Click += new System.EventHandler(this.btnOrderRefresh_Click);
            // 
            // btnExit
            // 
            this.btnExit.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnExit.Image = global::THOK.XC.Dispatching.Properties.Resources.Exit;
            this.btnExit.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnExit.Location = new System.Drawing.Point(144, 0);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(48, 51);
            this.btnExit.TabIndex = 21;
            this.btnExit.Text = "退出";
            this.btnExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click_1);
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "ROW_INDEX";
            this.Column1.FilteringEnabled = false;
            this.Column1.HeaderText = "流水号";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 75;
            // 
            // Column7
            // 
            this.Column7.DataPropertyName = "STOCKOUTID";
            this.Column7.FilteringEnabled = false;
            this.Column7.HeaderText = "补货ID";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "LINECODE";
            this.Column2.FilteringEnabled = false;
            this.Column2.HeaderText = "生产线";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 80;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "CIGARETTECODE";
            this.Column3.FilteringEnabled = false;
            this.Column3.HeaderText = "卷烟代码";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "CIGARETTENAME";
            this.Column4.FilteringEnabled = false;
            this.Column4.HeaderText = "卷烟名称";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "SORTCHANNELCODE";
            this.Column6.FilteringEnabled = false;
            this.Column6.HeaderText = "分拣烟道";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "CHANNELNAME";
            this.Column5.FilteringEnabled = false;
            this.Column5.HeaderText = "补货烟道";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // Column8
            // 
            this.Column8.DataPropertyName = "STATE";
            this.Column8.FilteringEnabled = false;
            this.Column8.HeaderText = "状态";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column8.Width = 120;
            // 
            // OrderStateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(953, 346);
            this.Name = "OrderStateForm";
            this.Text = "OrderStateForm";
            this.pnlTool.ResumeLayout(false);
            this.pnlContent.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnScannerRefresh;
        private System.Windows.Forms.DataGridView dgvMain;
        private System.Windows.Forms.BindingSource bsMain;
        private System.Windows.Forms.Button btnOrderRefresh;
        private System.Windows.Forms.Button btnLedRefresh;
        private System.Windows.Forms.Button btnExit;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn Column1;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn Column7;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn Column2;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn Column3;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn Column4;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn Column6;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn Column5;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn Column8;

    }
}