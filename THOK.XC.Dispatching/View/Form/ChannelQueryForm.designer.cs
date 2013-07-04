namespace THOK.XC.Dispatching.View
{
    partial class ChannelQueryForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvMain = new System.Windows.Forms.DataGridView();
            this.LINECODE = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.CHANNELCODE = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.CHANNELNAME = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.CHANNELTYPE = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.CIGARETTECODE = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.CIGARETTENAME = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.QUANTITY = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.bsMain = new System.Windows.Forms.BindingSource(this.components);
            this.btnExit = new System.Windows.Forms.Button();
            this.btnExchange = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
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
            this.pnlTool.Controls.Add(this.btnExchange);
            this.pnlTool.Controls.Add(this.btnRefresh);
            this.pnlTool.Size = new System.Drawing.Size(733, 53);
            // 
            // pnlContent
            // 
            this.pnlContent.Controls.Add(this.dgvMain);
            this.pnlContent.Size = new System.Drawing.Size(733, 229);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(733, 282);
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
            this.dgvMain.ColumnHeadersHeight = 20;
            this.dgvMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvMain.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.LINECODE,
            this.CHANNELCODE,
            this.CHANNELNAME,
            this.CHANNELTYPE,
            this.CIGARETTECODE,
            this.CIGARETTENAME,
            this.QUANTITY});
            this.dgvMain.DataSource = this.bsMain;
            this.dgvMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMain.Location = new System.Drawing.Point(0, 0);
            this.dgvMain.MultiSelect = false;
            this.dgvMain.Name = "dgvMain";
            this.dgvMain.ReadOnly = true;
            this.dgvMain.RowHeadersWidth = 30;
            this.dgvMain.RowTemplate.Height = 23;
            this.dgvMain.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMain.Size = new System.Drawing.Size(733, 229);
            this.dgvMain.TabIndex = 0;
            // 
            // LINECODE
            // 
            this.LINECODE.DataPropertyName = "LINECODE";
            this.LINECODE.FilteringEnabled = false;
            this.LINECODE.HeaderText = "生产线";
            this.LINECODE.Name = "LINECODE";
            this.LINECODE.ReadOnly = true;
            this.LINECODE.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // CHANNELCODE
            // 
            this.CHANNELCODE.DataPropertyName = "CHANNELCODE";
            this.CHANNELCODE.FilteringEnabled = false;
            this.CHANNELCODE.HeaderText = "烟道代码";
            this.CHANNELCODE.Name = "CHANNELCODE";
            this.CHANNELCODE.ReadOnly = true;
            this.CHANNELCODE.Resizable = System.Windows.Forms.DataGridViewTriState.True;
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
            // CHANNELTYPE
            // 
            this.CHANNELTYPE.DataPropertyName = "CHANNELTYPE";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.CHANNELTYPE.DefaultCellStyle = dataGridViewCellStyle3;
            this.CHANNELTYPE.FilteringEnabled = false;
            this.CHANNELTYPE.HeaderText = "烟道类别";
            this.CHANNELTYPE.Name = "CHANNELTYPE";
            this.CHANNELTYPE.ReadOnly = true;
            this.CHANNELTYPE.Resizable = System.Windows.Forms.DataGridViewTriState.True;
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
            this.CIGARETTENAME.Width = 200;
            // 
            // QUANTITY
            // 
            this.QUANTITY.DataPropertyName = "QUANTITY";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.QUANTITY.DefaultCellStyle = dataGridViewCellStyle4;
            this.QUANTITY.FilteringEnabled = false;
            this.QUANTITY.HeaderText = "数量";
            this.QUANTITY.Name = "QUANTITY";
            this.QUANTITY.ReadOnly = true;
            this.QUANTITY.Width = 60;
            // 
            // btnExit
            // 
            this.btnExit.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnExit.Image = global::THOK.XC.Dispatching.Properties.Resources.Exit;
            this.btnExit.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnExit.Location = new System.Drawing.Point(96, 0);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(48, 51);
            this.btnExit.TabIndex = 15;
            this.btnExit.Text = "退出";
            this.btnExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnExchange
            // 
            this.btnExchange.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnExchange.Image = global::THOK.XC.Dispatching.Properties.Resources.Info;
            this.btnExchange.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnExchange.Location = new System.Drawing.Point(48, 0);
            this.btnExchange.Name = "btnExchange";
            this.btnExchange.Size = new System.Drawing.Size(48, 51);
            this.btnExchange.TabIndex = 14;
            this.btnExchange.Text = "交换";
            this.btnExchange.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnExchange.UseVisualStyleBackColor = true;
            this.btnExchange.Click += new System.EventHandler(this.btnExchange_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnRefresh.Image = global::THOK.XC.Dispatching.Properties.Resources.Chart;
            this.btnRefresh.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnRefresh.Location = new System.Drawing.Point(0, 0);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(48, 51);
            this.btnRefresh.TabIndex = 13;
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // ChannelQueryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(733, 282);
            this.Name = "ChannelQueryForm";
            this.pnlTool.ResumeLayout(false);
            this.pnlContent.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnExchange;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.DataGridView dgvMain;
        private System.Windows.Forms.BindingSource bsMain;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn LINECODE;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn CHANNELCODE;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn CHANNELNAME;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn CHANNELTYPE;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn CIGARETTECODE;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn CIGARETTENAME;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn QUANTITY;
    }
}
