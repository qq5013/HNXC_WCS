using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace THOK.XC.Dispatching.View
{
    public partial class CellError : Form
    {
        public string Flag;
        private string ErrCode;
         
        private string ErrMsg;
        public CellError()
        {
            InitializeComponent();
        }
        public CellError(string strErrCode, string strErrMsg)
        {
            InitializeComponent();
            ErrCode = strErrCode;
            ErrMsg = strErrMsg;           
        }

        private void CellError_Load(object sender, EventArgs e)
        {
            this.lblMsg.Text = "堆垛机返回错误，错误代码：" + ErrCode + ",错误内容：" + ErrMsg;

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.rbt1.Checked)
                Flag = "1";
            else
                Flag = "2";
            this.DialogResult = DialogResult.OK;
        }
    }
}
