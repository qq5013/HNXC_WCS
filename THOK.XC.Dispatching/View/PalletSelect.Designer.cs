namespace THOK.XC.Dispatching.View
{
    partial class PalletSelect
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
            this.btnPallet = new System.Windows.Forms.Button();
            this.btnpallets = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnPallet
            // 
            this.btnPallet.Location = new System.Drawing.Point(18, 42);
            this.btnPallet.Name = "btnPallet";
            this.btnPallet.Size = new System.Drawing.Size(116, 33);
            this.btnPallet.TabIndex = 0;
            this.btnPallet.Text = "单托盘入库";
            this.btnPallet.UseVisualStyleBackColor = true;
            this.btnPallet.Click += new System.EventHandler(this.btnPallet_Click);
            // 
            // btnpallets
            // 
            this.btnpallets.Location = new System.Drawing.Point(167, 42);
            this.btnpallets.Name = "btnpallets";
            this.btnpallets.Size = new System.Drawing.Size(122, 33);
            this.btnpallets.TabIndex = 1;
            this.btnpallets.Text = "托盘组入库";
            this.btnpallets.UseVisualStyleBackColor = true;
            this.btnpallets.Click += new System.EventHandler(this.btnpallets_Click);
            // 
            // PalletSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(306, 132);
            this.Controls.Add(this.btnpallets);
            this.Controls.Add(this.btnPallet);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PalletSelect";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "托盘入库选择";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnPallet;
        private System.Windows.Forms.Button btnpallets;
    }
}