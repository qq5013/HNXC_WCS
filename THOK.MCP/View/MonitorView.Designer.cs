namespace THOK.MCP.View
{
    partial class MonitorView
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
            this.progressPanel = new System.Windows.Forms.Panel();
            this.lblProgress = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.progressPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // progressPanel
            // 
            this.progressPanel.BackColor = System.Drawing.SystemColors.Control;
            this.progressPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.progressPanel.Controls.Add(this.lblProgress);
            this.progressPanel.Controls.Add(this.progressBar);
            this.progressPanel.Location = new System.Drawing.Point(101, 112);
            this.progressPanel.Name = "progressPanel";
            this.progressPanel.Size = new System.Drawing.Size(393, 80);
            this.progressPanel.TabIndex = 0;
            this.progressPanel.Visible = false;
            // 
            // lblProgress
            // 
            this.lblProgress.AutoSize = true;
            this.lblProgress.Location = new System.Drawing.Point(11, 20);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(53, 12);
            this.lblProgress.TabIndex = 1;
            this.lblProgress.Text = "处理进程";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(13, 46);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(366, 23);
            this.progressBar.TabIndex = 0;
            // 
            // MonitorView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.BackColor = System.Drawing.SystemColors.GrayText;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.progressPanel);
            this.Name = "MonitorView";
            this.Size = new System.Drawing.Size(567, 245);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MonitorView_MouseDown);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MonitorView_Paint);
            this.progressPanel.ResumeLayout(false);
            this.progressPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel progressPanel;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.ProgressBar progressBar;
    }
}
