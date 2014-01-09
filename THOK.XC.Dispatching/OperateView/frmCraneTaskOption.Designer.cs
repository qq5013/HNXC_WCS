namespace THOK.XC.Dispatching.OperateView
{
    partial class frmCraneTaskOption
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCraneTaskOption));
            this.label1 = new System.Windows.Forms.Label();
            this.cmbCrane = new System.Windows.Forms.ComboBox();
            this.dgvMain = new System.Windows.Forms.DataGridView();
            this.btnOption = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.colBILL_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBTYPE_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCIGARETTE_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFORMULA_CODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFORMULA_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBATCH_WEIGHT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PRODUCT_CODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPRODUCT_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPRODUCT_BARCODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCELL_CODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGRADE_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colORIGINAL_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colYEARS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSTYLE_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colErrDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTaskID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlTool.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlTool
            // 
            this.pnlTool.Controls.Add(this.btnOption);
            this.pnlTool.Controls.Add(this.cmbCrane);
            this.pnlTool.Controls.Add(this.label1);
            this.pnlTool.Controls.Add(this.btnExit);
            this.pnlTool.Controls.Add(this.btnRefresh);
            this.pnlTool.Size = new System.Drawing.Size(800, 53);
            // 
            // pnlContent
            // 
            this.pnlContent.Controls.Add(this.dgvMain);
            this.pnlContent.Size = new System.Drawing.Size(800, 267);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(800, 320);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 40;
            this.label1.Text = "堆垛机编号";
            // 
            // cmbCrane
            // 
            this.cmbCrane.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCrane.FormattingEnabled = true;
            this.cmbCrane.Items.AddRange(new object[] {
            "01",
            "02",
            "03",
            "04",
            "05",
            "06"});
            this.cmbCrane.Location = new System.Drawing.Point(74, 15);
            this.cmbCrane.Name = "cmbCrane";
            this.cmbCrane.Size = new System.Drawing.Size(142, 20);
            this.cmbCrane.TabIndex = 41;
            // 
            // dgvMain
            // 
            this.dgvMain.AllowUserToAddRows = false;
            this.dgvMain.AllowUserToDeleteRows = false;
            this.dgvMain.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.dgvMain.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
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
            this.colBILL_NO,
            this.colBTYPE_NAME,
            this.colCIGARETTE_NAME,
            this.colFORMULA_CODE,
            this.colFORMULA_NAME,
            this.colBATCH_WEIGHT,
            this.PRODUCT_CODE,
            this.colPRODUCT_NAME,
            this.colPRODUCT_BARCODE,
            this.colCELL_CODE,
            this.colGRADE_NAME,
            this.colORIGINAL_NAME,
            this.colYEARS,
            this.colSTYLE_NAME,
            this.colErrDesc,
            this.colTaskID});
            this.dgvMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMain.Location = new System.Drawing.Point(0, 0);
            this.dgvMain.MultiSelect = false;
            this.dgvMain.Name = "dgvMain";
            this.dgvMain.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMain.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvMain.RowHeadersWidth = 30;
            this.dgvMain.RowTemplate.Height = 23;
            this.dgvMain.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMain.Size = new System.Drawing.Size(800, 267);
            this.dgvMain.TabIndex = 12;
            // 
            // btnOption
            // 
            this.btnOption.Image = global::THOK.XC.Dispatching.Properties.Resources.Modify;
            this.btnOption.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnOption.Location = new System.Drawing.Point(270, 2);
            this.btnOption.Name = "btnOption";
            this.btnOption.Size = new System.Drawing.Size(48, 45);
            this.btnOption.TabIndex = 42;
            this.btnOption.Text = "操作";
            this.btnOption.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnOption.UseVisualStyleBackColor = true;
            this.btnOption.Click += new System.EventHandler(this.btnOption_Click);
            // 
            // btnExit
            // 
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnExit.Location = new System.Drawing.Point(318, 2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(48, 45);
            this.btnExit.TabIndex = 38;
            this.btnExit.Text = "退出";
            this.btnExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Image = global::THOK.XC.Dispatching.Properties.Resources.Find;
            this.btnRefresh.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnRefresh.Location = new System.Drawing.Point(222, 2);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(48, 45);
            this.btnRefresh.TabIndex = 37;
            this.btnRefresh.Text = "查询";
            this.btnRefresh.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // colBILL_NO
            // 
            this.colBILL_NO.DataPropertyName = "BILL_NO";
            this.colBILL_NO.HeaderText = "单号";
            this.colBILL_NO.Name = "colBILL_NO";
            this.colBILL_NO.ReadOnly = true;
            // 
            // colBTYPE_NAME
            // 
            this.colBTYPE_NAME.DataPropertyName = "BTYPE_NAME";
            this.colBTYPE_NAME.HeaderText = "单据类型";
            this.colBTYPE_NAME.Name = "colBTYPE_NAME";
            this.colBTYPE_NAME.ReadOnly = true;
            // 
            // colCIGARETTE_NAME
            // 
            this.colCIGARETTE_NAME.DataPropertyName = "CIGARETTE_NAME";
            this.colCIGARETTE_NAME.HeaderText = "牌号";
            this.colCIGARETTE_NAME.Name = "colCIGARETTE_NAME";
            this.colCIGARETTE_NAME.ReadOnly = true;
            // 
            // colFORMULA_CODE
            // 
            this.colFORMULA_CODE.DataPropertyName = "FORMULA_CODE";
            this.colFORMULA_CODE.HeaderText = "配方编码";
            this.colFORMULA_CODE.Name = "colFORMULA_CODE";
            this.colFORMULA_CODE.ReadOnly = true;
            // 
            // colFORMULA_NAME
            // 
            this.colFORMULA_NAME.DataPropertyName = "FORMULA_NAME";
            this.colFORMULA_NAME.HeaderText = "配方名称";
            this.colFORMULA_NAME.Name = "colFORMULA_NAME";
            this.colFORMULA_NAME.ReadOnly = true;
            // 
            // colBATCH_WEIGHT
            // 
            this.colBATCH_WEIGHT.DataPropertyName = "BATCH_WEIGHT";
            this.colBATCH_WEIGHT.HeaderText = "配方重量";
            this.colBATCH_WEIGHT.Name = "colBATCH_WEIGHT";
            this.colBATCH_WEIGHT.ReadOnly = true;
            // 
            // PRODUCT_CODE
            // 
            this.PRODUCT_CODE.DataPropertyName = "PRODUCT_CODE";
            this.PRODUCT_CODE.HeaderText = "产品编码";
            this.PRODUCT_CODE.Name = "PRODUCT_CODE";
            this.PRODUCT_CODE.ReadOnly = true;
            // 
            // colPRODUCT_NAME
            // 
            this.colPRODUCT_NAME.DataPropertyName = "PRODUCT_NAME";
            this.colPRODUCT_NAME.HeaderText = "产品名称";
            this.colPRODUCT_NAME.Name = "colPRODUCT_NAME";
            this.colPRODUCT_NAME.ReadOnly = true;
            // 
            // colPRODUCT_BARCODE
            // 
            this.colPRODUCT_BARCODE.DataPropertyName = "PRODUCT_BARCODE";
            this.colPRODUCT_BARCODE.HeaderText = "产品条码";
            this.colPRODUCT_BARCODE.Name = "colPRODUCT_BARCODE";
            this.colPRODUCT_BARCODE.ReadOnly = true;
            // 
            // colCELL_CODE
            // 
            this.colCELL_CODE.DataPropertyName = "CELL_CODE";
            this.colCELL_CODE.HeaderText = "货位编号";
            this.colCELL_CODE.Name = "colCELL_CODE";
            this.colCELL_CODE.ReadOnly = true;
            // 
            // colGRADE_NAME
            // 
            this.colGRADE_NAME.DataPropertyName = "GRADE_NAME";
            this.colGRADE_NAME.HeaderText = "产品等级";
            this.colGRADE_NAME.Name = "colGRADE_NAME";
            this.colGRADE_NAME.ReadOnly = true;
            // 
            // colORIGINAL_NAME
            // 
            this.colORIGINAL_NAME.HeaderText = "原产地";
            this.colORIGINAL_NAME.Name = "colORIGINAL_NAME";
            this.colORIGINAL_NAME.ReadOnly = true;
            // 
            // colYEARS
            // 
            this.colYEARS.DataPropertyName = "YEARS";
            this.colYEARS.HeaderText = "产品年份";
            this.colYEARS.Name = "colYEARS";
            this.colYEARS.ReadOnly = true;
            // 
            // colSTYLE_NAME
            // 
            this.colSTYLE_NAME.DataPropertyName = "STYLE_NAME";
            this.colSTYLE_NAME.HeaderText = "产品形态";
            this.colSTYLE_NAME.Name = "colSTYLE_NAME";
            this.colSTYLE_NAME.ReadOnly = true;
            // 
            // colErrDesc
            // 
            this.colErrDesc.DataPropertyName = "DESCRIPTION";
            this.colErrDesc.HeaderText = "错误描述";
            this.colErrDesc.Name = "colErrDesc";
            this.colErrDesc.ReadOnly = true;
            // 
            // colTaskID
            // 
            this.colTaskID.DataPropertyName = "TASK_ID";
            this.colTaskID.HeaderText = "作业ID";
            this.colTaskID.Name = "colTaskID";
            this.colTaskID.ReadOnly = true;
            this.colTaskID.Visible = false;
            // 
            // frmCraneTaskOption
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 320);
            this.Name = "frmCraneTaskOption";
            this.Text = "堆垛机任务操作";
            this.pnlTool.ResumeLayout(false);
            this.pnlTool.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.Button btnExit;
        protected System.Windows.Forms.Button btnRefresh;
        protected System.Windows.Forms.Button btnOption;
        private System.Windows.Forms.ComboBox cmbCrane;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvMain;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBILL_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBTYPE_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCIGARETTE_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFORMULA_CODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFORMULA_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBATCH_WEIGHT;
        private System.Windows.Forms.DataGridViewTextBoxColumn PRODUCT_CODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPRODUCT_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPRODUCT_BARCODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCELL_CODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGRADE_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn colORIGINAL_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn colYEARS;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSTYLE_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn colErrDesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTaskID;
    }
}