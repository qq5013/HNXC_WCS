namespace THOK.XC.Process.View
{
    partial class ReadBarcode
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
            this.lblBadFlag = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtRightBarcode = new System.Windows.Forms.TextBox();
            this.txtLeftBarcode = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.lblLeftProductInfo = new System.Windows.Forms.Label();
            this.lblRightProductInfo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblBadFlag
            // 
            this.lblBadFlag.AutoSize = true;
            this.lblBadFlag.Location = new System.Drawing.Point(12, 18);
            this.lblBadFlag.Name = "lblBadFlag";
            this.lblBadFlag.Size = new System.Drawing.Size(89, 12);
            this.lblBadFlag.TabIndex = 0;
            this.lblBadFlag.Text = "错误条码类型：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "左";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(321, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "右";
            // 
            // txtRightBarcode
            // 
            this.txtRightBarcode.Location = new System.Drawing.Point(344, 77);
            this.txtRightBarcode.Name = "txtRightBarcode";
            this.txtRightBarcode.Size = new System.Drawing.Size(229, 21);
            this.txtRightBarcode.TabIndex = 3;
            this.txtRightBarcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRightBarcode_KeyDown);
            // 
            // txtLeftBarcode
            // 
            this.txtLeftBarcode.Location = new System.Drawing.Point(35, 77);
            this.txtLeftBarcode.Name = "txtLeftBarcode";
            this.txtLeftBarcode.Size = new System.Drawing.Size(217, 21);
            this.txtLeftBarcode.TabIndex = 4;
            this.txtLeftBarcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtLeftBarcode_KeyDown);
            // 
            // label4
            // 
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Location = new System.Drawing.Point(287, 68);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1, 38);
            this.label4.TabIndex = 5;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(465, 272);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lblLeftProductInfo
            // 
            this.lblLeftProductInfo.AutoSize = true;
            this.lblLeftProductInfo.Location = new System.Drawing.Point(12, 141);
            this.lblLeftProductInfo.Name = "lblLeftProductInfo";
            this.lblLeftProductInfo.Size = new System.Drawing.Size(89, 12);
            this.lblLeftProductInfo.TabIndex = 7;
            this.lblLeftProductInfo.Text = "错误条码类型：";
            // 
            // lblRightProductInfo
            // 
            this.lblRightProductInfo.AutoSize = true;
            this.lblRightProductInfo.Location = new System.Drawing.Point(321, 141);
            this.lblRightProductInfo.Name = "lblRightProductInfo";
            this.lblRightProductInfo.Size = new System.Drawing.Size(89, 12);
            this.lblRightProductInfo.TabIndex = 8;
            this.lblRightProductInfo.Text = "错误条码类型：";
            // 
            // ReadBarcode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(585, 335);
            this.Controls.Add(this.lblRightProductInfo);
            this.Controls.Add(this.lblLeftProductInfo);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtLeftBarcode);
            this.Controls.Add(this.txtRightBarcode);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblBadFlag);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ReadBarcode";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "错误条码处理";
            this.Activated += new System.EventHandler(this.ReadBarcode_Activated);
            this.Load += new System.EventHandler(this.ReadBarcode_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblBadFlag;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtRightBarcode;
        private System.Windows.Forms.TextBox txtLeftBarcode;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label lblLeftProductInfo;
        private System.Windows.Forms.Label lblRightProductInfo;
    }
}