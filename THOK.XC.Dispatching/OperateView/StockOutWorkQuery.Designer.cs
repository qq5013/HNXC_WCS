namespace THOK.XC.Dispatching.OperateView
{
    partial class StockOutWorkQuery
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvMain = new System.Windows.Forms.DataGridView();
            this.colBILL_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBTYPE_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTASKNAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTASK_DATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCIGARETTE_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFORMULA_CODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFORMULA_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBATCH_WEIGHT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PRODUCT_CODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRowID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPRODUCT_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPRODUCT_BARCODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCELL_CODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGRADE_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colORIGINAL_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colYEARS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSTYLE_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCATEGORY_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTASKSTATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFINISH_DATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTaskID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.pnlSub = new System.Windows.Forms.Panel();
            this.dgSub = new System.Windows.Forms.DataGridView();
            this.colITEM_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDESCRIPTION = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCRANE_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCAR_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFROM_STATION = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTO_STATION = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSTATE_DESC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlTool.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).BeginInit();
            this.pnlSub.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgSub)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlTool
            // 
            this.pnlTool.Controls.Add(this.btnExit);
            this.pnlTool.Controls.Add(this.btnRefresh);
            this.pnlTool.Size = new System.Drawing.Size(1062, 53);
            // 
            // pnlContent
            // 
            this.pnlContent.Controls.Add(this.dgvMain);
            this.pnlContent.Size = new System.Drawing.Size(1062, 132);
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.pnlSub);
            this.pnlMain.Size = new System.Drawing.Size(1062, 302);
            this.pnlMain.Controls.SetChildIndex(this.pnlSub, 0);
            this.pnlMain.Controls.SetChildIndex(this.pnlTool, 0);
            this.pnlMain.Controls.SetChildIndex(this.pnlContent, 0);
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
            dataGridViewCellStyle2.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMain.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMain.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colBILL_NO,
            this.colBTYPE_NAME,
            this.colTASKNAME,
            this.colTASK_DATE,
            this.colCIGARETTE_NAME,
            this.colFORMULA_CODE,
            this.colFORMULA_NAME,
            this.colBATCH_WEIGHT,
            this.PRODUCT_CODE,
            this.colRowID,
            this.colPRODUCT_NAME,
            this.colPRODUCT_BARCODE,
            this.colCELL_CODE,
            this.colGRADE_NAME,
            this.colORIGINAL_NAME,
            this.colYEARS,
            this.colSTYLE_NAME,
            this.colCATEGORY_NAME,
            this.colTASKSTATE,
            this.colFINISH_DATE,
            this.colTaskID});
            this.dgvMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMain.Location = new System.Drawing.Point(0, 0);
            this.dgvMain.MultiSelect = false;
            this.dgvMain.Name = "dgvMain";
            this.dgvMain.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMain.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvMain.RowHeadersWidth = 30;
            this.dgvMain.RowTemplate.Height = 23;
            this.dgvMain.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMain.Size = new System.Drawing.Size(1062, 132);
            this.dgvMain.TabIndex = 11;
            
            this.dgvMain.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(this.dgvMain_RowStateChanged);
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
            // colTASKNAME
            // 
            this.colTASKNAME.DataPropertyName = "TASKNAME";
            this.colTASKNAME.HeaderText = "作业人员";
            this.colTASKNAME.Name = "colTASKNAME";
            this.colTASKNAME.ReadOnly = true;
            // 
            // colTASK_DATE
            // 
            this.colTASK_DATE.DataPropertyName = "TASK_DATE";
            this.colTASK_DATE.HeaderText = "作业日期";
            this.colTASK_DATE.Name = "colTASK_DATE";
            this.colTASK_DATE.ReadOnly = true;
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
            // colRowID
            // 
            this.colRowID.DataPropertyName = "ROWIDSTRING";
            this.colRowID.HeaderText = "序号";
            this.colRowID.Name = "colRowID";
            this.colRowID.ReadOnly = true;
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
            // colCATEGORY_NAME
            // 
            this.colCATEGORY_NAME.DataPropertyName = "CATEGORY_NAME";
            this.colCATEGORY_NAME.HeaderText = "产品类别";
            this.colCATEGORY_NAME.Name = "colCATEGORY_NAME";
            this.colCATEGORY_NAME.ReadOnly = true;
            // 
            // colTASKSTATE
            // 
            this.colTASKSTATE.DataPropertyName = "TASKSTATE";
            this.colTASKSTATE.HeaderText = "作业状态";
            this.colTASKSTATE.Name = "colTASKSTATE";
            this.colTASKSTATE.ReadOnly = true;
            // 
            // colFINISH_DATE
            // 
            this.colFINISH_DATE.DataPropertyName = "FINISH_DATE";
            this.colFINISH_DATE.HeaderText = "入库时间";
            this.colFINISH_DATE.Name = "colFINISH_DATE";
            this.colFINISH_DATE.ReadOnly = true;
            // 
            // colTaskID
            // 
            this.colTaskID.DataPropertyName = "TASK_ID";
            this.colTaskID.HeaderText = "作业ID";
            this.colTaskID.Name = "colTaskID";
            this.colTaskID.ReadOnly = true;
            this.colTaskID.Visible = false;
            // 
            // btnExit
            // 
            this.btnExit.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnExit.Image = global::THOK.XC.Dispatching.Properties.Resources.Exit;
            this.btnExit.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnExit.Location = new System.Drawing.Point(48, 0);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(48, 51);
            this.btnExit.TabIndex = 38;
            this.btnExit.Text = "退出";
            this.btnExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnRefresh.Image = global::THOK.XC.Dispatching.Properties.Resources.Find;
            this.btnRefresh.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnRefresh.Location = new System.Drawing.Point(0, 0);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(48, 51);
            this.btnRefresh.TabIndex = 37;
            this.btnRefresh.Text = "查询";
            this.btnRefresh.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // pnlSub
            // 
            this.pnlSub.Controls.Add(this.dgSub);
            this.pnlSub.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlSub.Location = new System.Drawing.Point(0, 185);
            this.pnlSub.Name = "pnlSub";
            this.pnlSub.Size = new System.Drawing.Size(1062, 117);
            this.pnlSub.TabIndex = 2;
            // 
            // dgSub
            // 
            this.dgSub.AllowUserToAddRows = false;
            this.dgSub.AllowUserToDeleteRows = false;
            this.dgSub.AllowUserToResizeRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.dgSub.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgSub.BackgroundColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgSub.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgSub.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgSub.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colITEM_NO,
            this.colDESCRIPTION,
            this.colCRANE_NAME,
            this.colCAR_NAME,
            this.colFROM_STATION,
            this.colTO_STATION,
            this.colSTATE_DESC});
            this.dgSub.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgSub.Location = new System.Drawing.Point(0, 0);
            this.dgSub.MultiSelect = false;
            this.dgSub.Name = "dgSub";
            this.dgSub.ReadOnly = true;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgSub.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgSub.RowHeadersWidth = 30;
            this.dgSub.RowTemplate.Height = 23;
            this.dgSub.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgSub.Size = new System.Drawing.Size(1062, 117);
            this.dgSub.TabIndex = 13;
            // 
            // colITEM_NO
            // 
            this.colITEM_NO.DataPropertyName = "ITEM_NO";
            this.colITEM_NO.HeaderText = "序号";
            this.colITEM_NO.Name = "colITEM_NO";
            this.colITEM_NO.ReadOnly = true;
            // 
            // colDESCRIPTION
            // 
            this.colDESCRIPTION.DataPropertyName = "DESCRIPTION";
            this.colDESCRIPTION.HeaderText = "作业内容";
            this.colDESCRIPTION.Name = "colDESCRIPTION";
            this.colDESCRIPTION.ReadOnly = true;
            // 
            // colCRANE_NAME
            // 
            this.colCRANE_NAME.DataPropertyName = "CRANE_NAME";
            this.colCRANE_NAME.HeaderText = "堆垛机";
            this.colCRANE_NAME.Name = "colCRANE_NAME";
            this.colCRANE_NAME.ReadOnly = true;
            // 
            // colCAR_NAME
            // 
            this.colCAR_NAME.DataPropertyName = "CAR_NAME";
            this.colCAR_NAME.HeaderText = "穿梭车";
            this.colCAR_NAME.Name = "colCAR_NAME";
            this.colCAR_NAME.ReadOnly = true;
            // 
            // colFROM_STATION
            // 
            this.colFROM_STATION.DataPropertyName = "FROM_STATION";
            this.colFROM_STATION.HeaderText = "起始地址";
            this.colFROM_STATION.Name = "colFROM_STATION";
            this.colFROM_STATION.ReadOnly = true;
            // 
            // colTO_STATION
            // 
            this.colTO_STATION.DataPropertyName = "TO_STATION";
            this.colTO_STATION.HeaderText = "目的地址";
            this.colTO_STATION.Name = "colTO_STATION";
            this.colTO_STATION.ReadOnly = true;
            // 
            // colSTATE_DESC
            // 
            this.colSTATE_DESC.DataPropertyName = "STATE_DESC";
            this.colSTATE_DESC.HeaderText = "状态";
            this.colSTATE_DESC.Name = "colSTATE_DESC";
            this.colSTATE_DESC.ReadOnly = true;
            // 
            // StockOutWorkQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1062, 302);
            this.Name = "StockOutWorkQuery";
            this.Text = "出库作业查询";
            this.pnlTool.ResumeLayout(false);
            this.pnlContent.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).EndInit();
            this.pnlSub.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgSub)).EndInit();
            this.ResumeLayout(false);

        }
        protected System.Windows.Forms.Button btnExit;
        protected System.Windows.Forms.Button btnRefresh;
        #endregion

        private System.Windows.Forms.DataGridView dgvMain;
        private System.Windows.Forms.Panel pnlSub;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBILL_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBTYPE_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTASKNAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTASK_DATE;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCIGARETTE_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFORMULA_CODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFORMULA_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBATCH_WEIGHT;
        private System.Windows.Forms.DataGridViewTextBoxColumn PRODUCT_CODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRowID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPRODUCT_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPRODUCT_BARCODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCELL_CODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGRADE_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn colORIGINAL_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn colYEARS;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSTYLE_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCATEGORY_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTASKSTATE;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFINISH_DATE;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTaskID;
        protected System.Windows.Forms.DataGridView dgSub;
        private System.Windows.Forms.DataGridViewTextBoxColumn colITEM_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDESCRIPTION;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCRANE_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCAR_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFROM_STATION;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTO_STATION;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSTATE_DESC;
    }
}