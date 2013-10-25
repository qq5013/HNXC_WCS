namespace THOK.XC.Dispatching.View
{
    partial class StockToStation
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
            this.btnOK = new System.Windows.Forms.Button();
            this.lblMessage = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtWeight = new System.Windows.Forms.TextBox();
            this.txtGRADE_NAME = new System.Windows.Forms.TextBox();
            this.txtBill_No = new System.Windows.Forms.TextBox();
            this.txtSTYLE_NAME = new System.Windows.Forms.TextBox();
            this.txtORIGINAL_NAME = new System.Windows.Forms.TextBox();
            this.txtCIGARETTE_NAME = new System.Windows.Forms.TextBox();
            this.txtProductBarCode = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(170, 264);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 27);
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(145, 20);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(161, 12);
            this.lblMessage.TabIndex = 8;
            this.lblMessage.Text = "抽检货物已到达，请人工处理";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(66, 228);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(293, 12);
            this.label1.TabIndex = 9;
            this.label1.Text = "如货物已经抱走，请点击确定按钮，执行空托盘入库！";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtWeight);
            this.groupBox1.Controls.Add(this.txtGRADE_NAME);
            this.groupBox1.Controls.Add(this.txtBill_No);
            this.groupBox1.Controls.Add(this.txtSTYLE_NAME);
            this.groupBox1.Controls.Add(this.txtORIGINAL_NAME);
            this.groupBox1.Controls.Add(this.txtCIGARETTE_NAME);
            this.groupBox1.Controls.Add(this.txtProductBarCode);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(26, 55);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(408, 158);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "货物信息";
            // 
            // txtWeight
            // 
            this.txtWeight.Location = new System.Drawing.Point(276, 123);
            this.txtWeight.Name = "txtWeight";
            this.txtWeight.ReadOnly = true;
            this.txtWeight.Size = new System.Drawing.Size(123, 21);
            this.txtWeight.TabIndex = 13;
            this.txtWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtGRADE_NAME
            // 
            this.txtGRADE_NAME.Location = new System.Drawing.Point(276, 88);
            this.txtGRADE_NAME.Name = "txtGRADE_NAME";
            this.txtGRADE_NAME.ReadOnly = true;
            this.txtGRADE_NAME.Size = new System.Drawing.Size(123, 21);
            this.txtGRADE_NAME.TabIndex = 12;
            // 
            // txtBill_No
            // 
            this.txtBill_No.Location = new System.Drawing.Point(276, 56);
            this.txtBill_No.Name = "txtBill_No";
            this.txtBill_No.ReadOnly = true;
            this.txtBill_No.Size = new System.Drawing.Size(123, 21);
            this.txtBill_No.TabIndex = 11;
            // 
            // txtSTYLE_NAME
            // 
            this.txtSTYLE_NAME.Location = new System.Drawing.Point(68, 123);
            this.txtSTYLE_NAME.Name = "txtSTYLE_NAME";
            this.txtSTYLE_NAME.ReadOnly = true;
            this.txtSTYLE_NAME.Size = new System.Drawing.Size(123, 21);
            this.txtSTYLE_NAME.TabIndex = 10;
            // 
            // txtORIGINAL_NAME
            // 
            this.txtORIGINAL_NAME.Location = new System.Drawing.Point(68, 88);
            this.txtORIGINAL_NAME.Name = "txtORIGINAL_NAME";
            this.txtORIGINAL_NAME.ReadOnly = true;
            this.txtORIGINAL_NAME.Size = new System.Drawing.Size(123, 21);
            this.txtORIGINAL_NAME.TabIndex = 9;
            // 
            // txtCIGARETTE_NAME
            // 
            this.txtCIGARETTE_NAME.Location = new System.Drawing.Point(68, 56);
            this.txtCIGARETTE_NAME.Name = "txtCIGARETTE_NAME";
            this.txtCIGARETTE_NAME.ReadOnly = true;
            this.txtCIGARETTE_NAME.Size = new System.Drawing.Size(123, 21);
            this.txtCIGARETTE_NAME.TabIndex = 8;
            // 
            // txtProductBarCode
            // 
            this.txtProductBarCode.Location = new System.Drawing.Point(68, 24);
            this.txtProductBarCode.Name = "txtProductBarCode";
            this.txtProductBarCode.ReadOnly = true;
            this.txtProductBarCode.Size = new System.Drawing.Size(334, 21);
            this.txtProductBarCode.TabIndex = 7;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(215, 125);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 12);
            this.label8.TabIndex = 6;
            this.label8.Text = "重    量：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(215, 93);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 5;
            this.label7.Text = "等    级：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(215, 61);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 4;
            this.label6.Text = "入库批次：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 125);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 3;
            this.label5.Text = "形    态：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "产    地：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "牌    号：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "产品条码：";
            // 
            // StockToStation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(446, 303);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.btnOK);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StockToStation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "提示";
            this.Load += new System.EventHandler(this.StockToStation_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtWeight;
        private System.Windows.Forms.TextBox txtGRADE_NAME;
        private System.Windows.Forms.TextBox txtBill_No;
        private System.Windows.Forms.TextBox txtSTYLE_NAME;
        private System.Windows.Forms.TextBox txtORIGINAL_NAME;
        private System.Windows.Forms.TextBox txtCIGARETTE_NAME;
        private System.Windows.Forms.TextBox txtProductBarCode;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
    }
}