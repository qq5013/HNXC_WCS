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
            this.btnMoveOut = new System.Windows.Forms.Button();
            this.btnInspect = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnSimulate = new System.Windows.Forms.Button();
            this.btnOperate = new System.Windows.Forms.Button();
            this.btnPalletIn = new System.Windows.Forms.Button();
            this.btnCheckScan = new System.Windows.Forms.Button();
            this.btnHelp = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnVerficate = new System.Windows.Forms.Button();
            this.btnBarcodeScan = new System.Windows.Forms.Button();
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
            this.imgList.Images.SetKeyName(11, "05.ico");
            this.imgList.Images.SetKeyName(12, "pic.png");
            this.imgList.Images.SetKeyName(13, "开始出库.BMP");
            this.imgList.Images.SetKeyName(14, "停止出库.gif");
            this.imgList.Images.SetKeyName(15, "恢复出库.gif");
            this.imgList.Images.SetKeyName(16, "校验.gif");
            this.imgList.Images.SetKeyName(17, "抽检.gif");
            this.imgList.Images.SetKeyName(18, "操作.gif");
            this.imgList.Images.SetKeyName(19, "出.gif");
            this.imgList.Images.SetKeyName(20, "故障.gif");
            this.imgList.Images.SetKeyName(21, "倒库.gif");
            this.imgList.Images.SetKeyName(22, "入.gif");
            this.imgList.Images.SetKeyName(23, "扫码1.gif");
            this.imgList.Images.SetKeyName(24, "退出.gif");
            this.imgList.Images.SetKeyName(25, "托盘入库.gif");
            this.imgList.Images.SetKeyName(26, "play.png");
            this.imgList.Images.SetKeyName(27, "stop.png");
            this.imgList.Images.SetKeyName(28, "settings.png");
            // 
            // pnlButton
            // 
            this.pnlButton.ColumnCount = 6;
            this.pnlButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17F));
            this.pnlButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.6F));
            this.pnlButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.6F));
            this.pnlButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.6F));
            this.pnlButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.6F));
            this.pnlButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.6F));
            this.pnlButton.Controls.Add(this.btnMoveOut, 0, 1);
            this.pnlButton.Controls.Add(this.btnInspect, 0, 1);
            this.pnlButton.Controls.Add(this.btnStart, 0, 0);
            this.pnlButton.Controls.Add(this.btnStop, 1, 0);
            this.pnlButton.Controls.Add(this.btnSimulate, 2, 0);
            this.pnlButton.Controls.Add(this.btnOperate, 3, 0);
            this.pnlButton.Controls.Add(this.btnPalletIn, 2, 1);
            this.pnlButton.Controls.Add(this.btnCheckScan, 3, 1);
            this.pnlButton.Controls.Add(this.btnHelp, 5, 0);
            this.pnlButton.Controls.Add(this.btnExit, 5, 1);
            this.pnlButton.Controls.Add(this.btnVerficate, 4, 0);
            this.pnlButton.Controls.Add(this.btnBarcodeScan, 4, 1);
            this.pnlButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlButton.Location = new System.Drawing.Point(0, 0);
            this.pnlButton.Name = "pnlButton";
            this.pnlButton.RowCount = 2;
            this.pnlButton.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.pnlButton.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.pnlButton.Size = new System.Drawing.Size(665, 130);
            this.pnlButton.TabIndex = 0;
            // 
            // btnMoveOut
            // 
            this.btnMoveOut.BackColor = System.Drawing.Color.Wheat;
            this.btnMoveOut.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnMoveOut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMoveOut.Font = new System.Drawing.Font("YouYuan", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnMoveOut.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnMoveOut.ImageIndex = 21;
            this.btnMoveOut.ImageList = this.imgList;
            this.btnMoveOut.Location = new System.Drawing.Point(3, 68);
            this.btnMoveOut.Name = "btnMoveOut";
            this.btnMoveOut.Size = new System.Drawing.Size(107, 59);
            this.btnMoveOut.TabIndex = 17;
            this.btnMoveOut.Text = "倒库出库";
            this.btnMoveOut.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnMoveOut.UseVisualStyleBackColor = false;
            this.btnMoveOut.Click += new System.EventHandler(this.btnMoveOut_Click);
            // 
            // btnInspect
            // 
            this.btnInspect.BackColor = System.Drawing.Color.Wheat;
            this.btnInspect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnInspect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInspect.Font = new System.Drawing.Font("YouYuan", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnInspect.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnInspect.ImageIndex = 17;
            this.btnInspect.ImageList = this.imgList;
            this.btnInspect.Location = new System.Drawing.Point(116, 68);
            this.btnInspect.Name = "btnInspect";
            this.btnInspect.Size = new System.Drawing.Size(104, 59);
            this.btnInspect.TabIndex = 16;
            this.btnInspect.Text = "抽检补料";
            this.btnInspect.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnInspect.UseVisualStyleBackColor = false;
            this.btnInspect.Click += new System.EventHandler(this.btnSpotCheck_Click);
            // 
            // btnStart
            // 
            this.btnStart.BackColor = System.Drawing.Color.Wheat;
            this.btnStart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStart.Font = new System.Drawing.Font("YouYuan", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnStart.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnStart.ImageIndex = 26;
            this.btnStart.ImageList = this.imgList;
            this.btnStart.Location = new System.Drawing.Point(3, 3);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(107, 59);
            this.btnStart.TabIndex = 6;
            this.btnStart.Text = "开始出库";
            this.btnStart.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.BackColor = System.Drawing.Color.Wheat;
            this.btnStop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnStop.Enabled = false;
            this.btnStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStop.Font = new System.Drawing.Font("YouYuan", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnStop.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnStop.ImageIndex = 27;
            this.btnStop.ImageList = this.imgList;
            this.btnStop.Location = new System.Drawing.Point(116, 3);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(104, 59);
            this.btnStop.TabIndex = 11;
            this.btnStop.Text = "停止出库";
            this.btnStop.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnStop.UseVisualStyleBackColor = false;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnSimulate
            // 
            this.btnSimulate.BackColor = System.Drawing.Color.Wheat;
            this.btnSimulate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSimulate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSimulate.Font = new System.Drawing.Font("YouYuan", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSimulate.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSimulate.ImageIndex = 15;
            this.btnSimulate.ImageList = this.imgList;
            this.btnSimulate.Location = new System.Drawing.Point(226, 3);
            this.btnSimulate.Name = "btnSimulate";
            this.btnSimulate.Size = new System.Drawing.Size(104, 59);
            this.btnSimulate.TabIndex = 12;
            this.btnSimulate.Text = "恢复出库";
            this.btnSimulate.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSimulate.UseVisualStyleBackColor = false;
            this.btnSimulate.Click += new System.EventHandler(this.btnSimulate_Click);
            // 
            // btnOperate
            // 
            this.btnOperate.BackColor = System.Drawing.Color.Wheat;
            this.btnOperate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnOperate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOperate.Font = new System.Drawing.Font("YouYuan", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOperate.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnOperate.ImageIndex = 18;
            this.btnOperate.ImageList = this.imgList;
            this.btnOperate.Location = new System.Drawing.Point(336, 3);
            this.btnOperate.Name = "btnOperate";
            this.btnOperate.Size = new System.Drawing.Size(104, 59);
            this.btnOperate.TabIndex = 13;
            this.btnOperate.Text = "操作";
            this.btnOperate.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnOperate.UseVisualStyleBackColor = false;
            this.btnOperate.Click += new System.EventHandler(this.btnOperate_Click);
            // 
            // btnPalletIn
            // 
            this.btnPalletIn.BackColor = System.Drawing.Color.Wheat;
            this.btnPalletIn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPalletIn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPalletIn.Font = new System.Drawing.Font("YouYuan", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPalletIn.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnPalletIn.ImageIndex = 25;
            this.btnPalletIn.ImageList = this.imgList;
            this.btnPalletIn.Location = new System.Drawing.Point(226, 68);
            this.btnPalletIn.Name = "btnPalletIn";
            this.btnPalletIn.Size = new System.Drawing.Size(104, 59);
            this.btnPalletIn.TabIndex = 5;
            this.btnPalletIn.Text = "托盘入库";
            this.btnPalletIn.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnPalletIn.UseVisualStyleBackColor = false;
            this.btnPalletIn.Click += new System.EventHandler(this.btnPalletIn_Click);
            // 
            // btnCheckScan
            // 
            this.btnCheckScan.BackColor = System.Drawing.Color.Wheat;
            this.btnCheckScan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCheckScan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCheckScan.Font = new System.Drawing.Font("YouYuan", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCheckScan.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnCheckScan.ImageIndex = 23;
            this.btnCheckScan.ImageList = this.imgList;
            this.btnCheckScan.Location = new System.Drawing.Point(336, 68);
            this.btnCheckScan.Name = "btnCheckScan";
            this.btnCheckScan.Size = new System.Drawing.Size(104, 59);
            this.btnCheckScan.TabIndex = 18;
            this.btnCheckScan.Text = "盘点扫码";
            this.btnCheckScan.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnCheckScan.UseVisualStyleBackColor = false;
            this.btnCheckScan.Click += new System.EventHandler(this.btnCheckScan_Click);
            // 
            // btnHelp
            // 
            this.btnHelp.BackColor = System.Drawing.Color.Wheat;
            this.btnHelp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnHelp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHelp.Font = new System.Drawing.Font("YouYuan", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnHelp.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnHelp.ImageIndex = 7;
            this.btnHelp.ImageList = this.imgList;
            this.btnHelp.Location = new System.Drawing.Point(556, 3);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(106, 59);
            this.btnHelp.TabIndex = 14;
            this.btnHelp.Text = "帮助";
            this.btnHelp.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnHelp.UseVisualStyleBackColor = false;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Wheat;
            this.btnExit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("YouYuan", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnExit.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnExit.ImageIndex = 24;
            this.btnExit.ImageList = this.imgList;
            this.btnExit.Location = new System.Drawing.Point(556, 68);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(106, 59);
            this.btnExit.TabIndex = 15;
            this.btnExit.Text = "退出";
            this.btnExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnVerficate
            // 
            this.btnVerficate.BackColor = System.Drawing.Color.Wheat;
            this.btnVerficate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnVerficate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVerficate.Font = new System.Drawing.Font("YouYuan", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnVerficate.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnVerficate.ImageIndex = 16;
            this.btnVerficate.ImageList = this.imgList;
            this.btnVerficate.Location = new System.Drawing.Point(446, 3);
            this.btnVerficate.Name = "btnVerficate";
            this.btnVerficate.Size = new System.Drawing.Size(104, 59);
            this.btnVerficate.TabIndex = 19;
            this.btnVerficate.Text = "校验处理";
            this.btnVerficate.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnVerficate.UseVisualStyleBackColor = false;
            this.btnVerficate.Click += new System.EventHandler(this.btnVerficate_Click);
            // 
            // btnBarcodeScan
            // 
            this.btnBarcodeScan.BackColor = System.Drawing.Color.Wheat;
            this.btnBarcodeScan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnBarcodeScan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBarcodeScan.Font = new System.Drawing.Font("YouYuan", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnBarcodeScan.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnBarcodeScan.ImageIndex = 20;
            this.btnBarcodeScan.ImageList = this.imgList;
            this.btnBarcodeScan.Location = new System.Drawing.Point(446, 68);
            this.btnBarcodeScan.Name = "btnBarcodeScan";
            this.btnBarcodeScan.Size = new System.Drawing.Size(104, 59);
            this.btnBarcodeScan.TabIndex = 20;
            this.btnBarcodeScan.Text = "条码故障";
            this.btnBarcodeScan.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnBarcodeScan.UseVisualStyleBackColor = false;
            this.btnBarcodeScan.Click += new System.EventHandler(this.btnBarcodeScan_Click);
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
            this.Size = new System.Drawing.Size(665, 130);
            this.pnlButton.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imgList;
        private System.Windows.Forms.TableLayoutPanel pnlButton;
        private System.Windows.Forms.Button btnPalletIn;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnSimulate;
        private System.Windows.Forms.Button btnOperate;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnMoveOut;
        private System.Windows.Forms.Button btnInspect;
        private System.Windows.Forms.Button btnCheckScan;
        private System.Windows.Forms.Button btnVerficate;
        private System.Windows.Forms.Button btnBarcodeScan;
    }
}
