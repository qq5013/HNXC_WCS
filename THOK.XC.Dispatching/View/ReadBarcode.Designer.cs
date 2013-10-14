namespace THOK.XC.Dispatching.View
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
            this.txtRightBarcode = new System.Windows.Forms.TextBox();
            this.txtLeftBarcode = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtWeight2 = new System.Windows.Forms.TextBox();
            this.txtGRADE_NAME2 = new System.Windows.Forms.TextBox();
            this.txtBill_No2 = new System.Windows.Forms.TextBox();
            this.txtSTYLE_NAME2 = new System.Windows.Forms.TextBox();
            this.txtORIGINAL_NAME2 = new System.Windows.Forms.TextBox();
            this.txtCIGARETTE_NAME2 = new System.Windows.Forms.TextBox();
            this.txtScanCode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
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
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblBadFlag
            // 
            this.lblBadFlag.AutoSize = true;
            this.lblBadFlag.Location = new System.Drawing.Point(239, 23);
            this.lblBadFlag.Name = "lblBadFlag";
            this.lblBadFlag.Size = new System.Drawing.Size(437, 12);
            this.lblBadFlag.TabIndex = 0;
            this.lblBadFlag.Text = "不合格品已到达，故障信息：左侧条码无法读取，请确认条码位置或条码的正确性";
            // 
            // txtRightBarcode
            // 
            this.txtRightBarcode.Location = new System.Drawing.Point(519, 77);
            this.txtRightBarcode.Name = "txtRightBarcode";
            this.txtRightBarcode.Size = new System.Drawing.Size(310, 21);
            this.txtRightBarcode.TabIndex = 3;
            this.txtRightBarcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRightBarcode_KeyDown);
            // 
            // txtLeftBarcode
            // 
            this.txtLeftBarcode.Location = new System.Drawing.Point(92, 75);
            this.txtLeftBarcode.Name = "txtLeftBarcode";
            this.txtLeftBarcode.Size = new System.Drawing.Size(306, 21);
            this.txtLeftBarcode.TabIndex = 4;
            this.txtLeftBarcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtLeftBarcode_KeyDown);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(389, 310);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtWeight2);
            this.groupBox2.Controls.Add(this.txtGRADE_NAME2);
            this.groupBox2.Controls.Add(this.txtBill_No2);
            this.groupBox2.Controls.Add(this.txtSTYLE_NAME2);
            this.groupBox2.Controls.Add(this.txtORIGINAL_NAME2);
            this.groupBox2.Controls.Add(this.txtCIGARETTE_NAME2);
            this.groupBox2.Controls.Add(this.txtScanCode);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Location = new System.Drawing.Point(441, 104);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(408, 158);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "右侧条码信息";
            // 
            // txtWeight2
            // 
            this.txtWeight2.Location = new System.Drawing.Point(276, 123);
            this.txtWeight2.Name = "txtWeight2";
            this.txtWeight2.ReadOnly = true;
            this.txtWeight2.Size = new System.Drawing.Size(123, 21);
            this.txtWeight2.TabIndex = 13;
            this.txtWeight2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtGRADE_NAME2
            // 
            this.txtGRADE_NAME2.Location = new System.Drawing.Point(276, 88);
            this.txtGRADE_NAME2.Name = "txtGRADE_NAME2";
            this.txtGRADE_NAME2.ReadOnly = true;
            this.txtGRADE_NAME2.Size = new System.Drawing.Size(123, 21);
            this.txtGRADE_NAME2.TabIndex = 12;
            // 
            // txtBill_No2
            // 
            this.txtBill_No2.Location = new System.Drawing.Point(276, 56);
            this.txtBill_No2.Name = "txtBill_No2";
            this.txtBill_No2.ReadOnly = true;
            this.txtBill_No2.Size = new System.Drawing.Size(123, 21);
            this.txtBill_No2.TabIndex = 11;
            // 
            // txtSTYLE_NAME2
            // 
            this.txtSTYLE_NAME2.Location = new System.Drawing.Point(68, 123);
            this.txtSTYLE_NAME2.Name = "txtSTYLE_NAME2";
            this.txtSTYLE_NAME2.ReadOnly = true;
            this.txtSTYLE_NAME2.Size = new System.Drawing.Size(123, 21);
            this.txtSTYLE_NAME2.TabIndex = 10;
            // 
            // txtORIGINAL_NAME2
            // 
            this.txtORIGINAL_NAME2.Location = new System.Drawing.Point(68, 88);
            this.txtORIGINAL_NAME2.Name = "txtORIGINAL_NAME2";
            this.txtORIGINAL_NAME2.ReadOnly = true;
            this.txtORIGINAL_NAME2.Size = new System.Drawing.Size(123, 21);
            this.txtORIGINAL_NAME2.TabIndex = 9;
            // 
            // txtCIGARETTE_NAME2
            // 
            this.txtCIGARETTE_NAME2.Location = new System.Drawing.Point(68, 56);
            this.txtCIGARETTE_NAME2.Name = "txtCIGARETTE_NAME2";
            this.txtCIGARETTE_NAME2.ReadOnly = true;
            this.txtCIGARETTE_NAME2.Size = new System.Drawing.Size(123, 21);
            this.txtCIGARETTE_NAME2.TabIndex = 8;
            // 
            // txtScanCode
            // 
            this.txtScanCode.Location = new System.Drawing.Point(68, 24);
            this.txtScanCode.Name = "txtScanCode";
            this.txtScanCode.ReadOnly = true;
            this.txtScanCode.Size = new System.Drawing.Size(331, 21);
            this.txtScanCode.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(215, 125);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "重    量：";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(215, 93);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 12);
            this.label9.TabIndex = 5;
            this.label9.Text = "等    级：";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(215, 61);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 12);
            this.label10.TabIndex = 4;
            this.label10.Text = "入库批次：";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 125);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(65, 12);
            this.label11.TabIndex = 3;
            this.label11.Text = "形    态：";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 93);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(65, 12);
            this.label12.TabIndex = 2;
            this.label12.Text = "产    地：";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 61);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(65, 12);
            this.label13.TabIndex = 1;
            this.label13.Text = "牌    号：";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 29);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(65, 12);
            this.label14.TabIndex = 0;
            this.label14.Text = "产品条码：";
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
            this.groupBox1.Location = new System.Drawing.Point(14, 104);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(408, 158);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "左侧条码信息";
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
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(27, 78);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(65, 12);
            this.label15.TabIndex = 16;
            this.label15.Text = "左侧条码：";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(455, 80);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(65, 12);
            this.label16.TabIndex = 18;
            this.label16.Text = "右侧条码：";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(298, 280);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(281, 12);
            this.label17.TabIndex = 19;
            this.label17.Text = "温馨提示：条码扫描一致后，点击确认按钮执行入库";
            // 
            // ReadBarcode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(860, 345);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtLeftBarcode);
            this.Controls.Add(this.txtRightBarcode);
            this.Controls.Add(this.lblBadFlag);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ReadBarcode";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "错误条码处理";
            this.Activated += new System.EventHandler(this.ReadBarcode_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ReadBarcode_FormClosing);
            this.Load += new System.EventHandler(this.ReadBarcode_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblBadFlag;
        private System.Windows.Forms.TextBox txtRightBarcode;
        private System.Windows.Forms.TextBox txtLeftBarcode;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtWeight2;
        private System.Windows.Forms.TextBox txtGRADE_NAME2;
        private System.Windows.Forms.TextBox txtBill_No2;
        private System.Windows.Forms.TextBox txtSTYLE_NAME2;
        private System.Windows.Forms.TextBox txtORIGINAL_NAME2;
        private System.Windows.Forms.TextBox txtCIGARETTE_NAME2;
        private System.Windows.Forms.TextBox txtScanCode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
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
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
    }
}