namespace THOK.XC.Dispatching.View
{
    partial class ButtonArea
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ButtonArea));
            this.imgList = new System.Windows.Forms.ImageList(this.components);
            this.pnlButton = new System.Windows.Forms.TableLayoutPanel();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnHelp = new System.Windows.Forms.Button();
            this.btnOperate = new System.Windows.Forms.Button();
            this.btnSimulate = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnUpload = new System.Windows.Forms.Button();
            this.btnDownload = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.pnlButton.SuspendLayout();
            this.SuspendLayout();
            // 
            // imgList
            // 
            this.imgList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgList.ImageStream")));
            this.imgList.TransparentColor = System.Drawing.Color.Transparent;
            this.imgList.Images.SetKeyName(0, "");
            this.imgList.Images.SetKeyName(1, "");
            this.imgList.Images.SetKeyName(2, "");
            this.imgList.Images.SetKeyName(3, "");
            this.imgList.Images.SetKeyName(4, "");
            this.imgList.Images.SetKeyName(5, "");
            this.imgList.Images.SetKeyName(6, "");
            this.imgList.Images.SetKeyName(7, "");
            this.imgList.Images.SetKeyName(8, "swap32.gif");
            this.imgList.Images.SetKeyName(9, "down32.gif");
            this.imgList.Images.SetKeyName(10, "setup32.gif");
            // 
            // pnlButton
            // 
            this.pnlButton.ColumnCount = 4;
            this.pnlButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnlButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnlButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnlButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnlButton.Controls.Add(this.btnExit, 3, 1);
            this.pnlButton.Controls.Add(this.btnHelp, 2, 1);
            this.pnlButton.Controls.Add(this.btnOperate, 1, 1);
            this.pnlButton.Controls.Add(this.btnSimulate, 0, 1);
            this.pnlButton.Controls.Add(this.btnStop, 3, 0);
            this.pnlButton.Controls.Add(this.btnStart, 2, 0);
            this.pnlButton.Controls.Add(this.btnUpload, 1, 0);
            this.pnlButton.Controls.Add(this.btnDownload, 0, 0);
            this.pnlButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlButton.Location = new System.Drawing.Point(0, 0);
            this.pnlButton.Name = "pnlButton";
            this.pnlButton.RowCount = 2;
            this.pnlButton.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.pnlButton.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.pnlButton.Size = new System.Drawing.Size(498, 130);
            this.pnlButton.TabIndex = 0;
            // 
            // btnExit
            // 
            this.btnExit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnExit.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnExit.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnExit.ImageIndex = 4;
            this.btnExit.ImageList = this.imgList;
            this.btnExit.Location = new System.Drawing.Point(375, 68);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(120, 59);
            this.btnExit.TabIndex = 15;
            this.btnExit.Text = "退出";
            this.btnExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnHelp
            // 
            this.btnHelp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnHelp.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnHelp.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnHelp.ImageIndex = 7;
            this.btnHelp.ImageList = this.imgList;
            this.btnHelp.Location = new System.Drawing.Point(251, 68);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(118, 59);
            this.btnHelp.TabIndex = 14;
            this.btnHelp.Text = "帮助";
            this.btnHelp.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnHelp.UseVisualStyleBackColor = true;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // btnOperate
            // 
            this.btnOperate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnOperate.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOperate.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnOperate.ImageIndex = 3;
            this.btnOperate.ImageList = this.imgList;
            this.btnOperate.Location = new System.Drawing.Point(127, 68);
            this.btnOperate.Name = "btnOperate";
            this.btnOperate.Size = new System.Drawing.Size(118, 59);
            this.btnOperate.TabIndex = 13;
            this.btnOperate.Text = "操作";
            this.btnOperate.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnOperate.UseVisualStyleBackColor = true;
            this.btnOperate.Click += new System.EventHandler(this.btnOperate_Click);
            // 
            // btnSimulate
            // 
            this.btnSimulate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSimulate.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSimulate.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSimulate.ImageIndex = 9;
            this.btnSimulate.ImageList = this.imgList;
            this.btnSimulate.Location = new System.Drawing.Point(3, 68);
            this.btnSimulate.Name = "btnSimulate";
            this.btnSimulate.Size = new System.Drawing.Size(118, 59);
            this.btnSimulate.TabIndex = 12;
            this.btnSimulate.Text = "恢复出库";
            this.btnSimulate.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSimulate.UseVisualStyleBackColor = true;
            this.btnSimulate.Click += new System.EventHandler(this.btnSimulate_Click);
            // 
            // btnStop
            // 
            this.btnStop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnStop.Enabled = false;
            this.btnStop.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnStop.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnStop.ImageIndex = 1;
            this.btnStop.ImageList = this.imgList;
            this.btnStop.Location = new System.Drawing.Point(375, 3);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(120, 59);
            this.btnStop.TabIndex = 11;
            this.btnStop.Text = "停止出库";
            this.btnStop.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnStart
            // 
            this.btnStart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnStart.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnStart.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnStart.ImageIndex = 0;
            this.btnStart.ImageList = this.imgList;
            this.btnStart.Location = new System.Drawing.Point(251, 3);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(118, 59);
            this.btnStart.TabIndex = 6;
            this.btnStart.Text = "开始出库";
            this.btnStart.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnUpload
            // 
            this.btnUpload.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnUpload.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnUpload.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnUpload.ImageIndex = 6;
            this.btnUpload.ImageList = this.imgList;
            this.btnUpload.Location = new System.Drawing.Point(127, 3);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(118, 59);
            this.btnUpload.TabIndex = 5;
            this.btnUpload.Text = "更新条码";
            this.btnUpload.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // btnDownload
            // 
            this.btnDownload.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDownload.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDownload.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnDownload.ImageIndex = 5;
            this.btnDownload.ImageList = this.imgList;
            this.btnDownload.Location = new System.Drawing.Point(3, 3);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(118, 59);
            this.btnDownload.TabIndex = 2;
            this.btnDownload.Text = "数据下载";
            this.btnDownload.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 300000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // ButtonArea
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlButton);
            this.Name = "ButtonArea";
            this.Size = new System.Drawing.Size(498, 130);
            this.pnlButton.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imgList;
        private System.Windows.Forms.TableLayoutPanel pnlButton;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnSimulate;
        private System.Windows.Forms.Button btnOperate;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Timer timer1;
    }
}
