using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using THOK.XC.Process;
using System.IO.Ports;
using System.Text.RegularExpressions;


namespace THOK.XC.Dispatching.View
{
    public partial class ReadBarcode : Form
    {
        private string strBadFlag;//错误类型
        public string strBarCode;//烟包条码
        private SerialPort comm = new SerialPort();
        StringBuilder builder = new StringBuilder();

        public ReadBarcode()
        {
            InitializeComponent();
        }
        public ReadBarcode(string badFlag)
        {
            InitializeComponent();
            strBadFlag = badFlag;

        }

        private void ReadBarcode_Load(object sender, EventArgs e)
        {
            this.lblBadFlag.Text = "不合格品已到达，故障信息：" + strBadFlag + "，请确认条码位置或条码的正确性";

            THOK.MCP.Config.Configuration conf = new MCP.Config.Configuration();
            conf.Load("Config.xml");


            comm.PortName = conf.Attributes["ScanPortName"];
            comm.BaudRate = int.Parse(conf.Attributes["ScanBaudRate"]);
            comm.NewLine = "\r\n";
            comm.RtsEnable = true;//根据实际情况吧。

            //添加事件注册
            comm.DataReceived += comm_DataReceived;
            try
            {
                comm.Open();
            }
            catch (Exception ex)
            {
                //捕获到异常信息，创建一个新的comm对象，之前的不能用了。
                comm = new SerialPort();
                //现实异常信息给客户。
                MessageBox.Show(ex.Message);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.txtProductBarCode.Text.Trim() != "" && this.txtScanCode.Text.Trim()!="")
            {
                if (this.txtProductBarCode.Text.Trim() == this.txtScanCode.Text.Trim())
                {
                    strBarCode = this.txtProductBarCode.Text.Trim();
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show("左右两边条码不一致，请重新扫描！");
                }
            }
            else
            {
                MessageBox.Show("请扫描条码！");
            }
        }
        void comm_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int n = comm.BytesToRead;//先记录下来，避免某种原因，人为的原因，操作几次之间时间长，缓存不一致
            byte[] buf = new byte[n];//声明一个临时数组存储当前来的串口数据

            comm.Read(buf, 0, n);//读取缓冲数据
            builder.Remove(0, builder.Length);//清除字符串构造器的内容
            //因为要访问ui资源，所以需要使用invoke方式同步ui。
            this.Invoke((EventHandler)(delegate
            {

                //直接按ASCII规则转换成字符串
                builder.Append(Encoding.ASCII.GetString(buf));

                if (this.txtLeftBarcode.Focused)
                {
                    //追加的形式添加到文本框末端，并滚动到最后。
                    this.txtLeftBarcode.AppendText(builder.ToString());
                    if (this.txtLeftBarcode.Text.IndexOf("\r\n") > 0)
                    {
                        txtLeftBarcode.Text = txtLeftBarcode.Text.Replace("\r\n", "");
                        txtLeftBarcode_KeyDown(sender, new KeyEventArgs(Keys.Enter));
                    }
                }
                else
                {
                    if (this.txtRightBarcode.Focused)
                    {
                        this.txtRightBarcode.AppendText(builder.ToString());
                        if (this.txtRightBarcode.Text.IndexOf("\r\n") > 0)
                        {
                            txtRightBarcode.Text = txtRightBarcode.Text.Replace("\r\n", "");
                            txtRightBarcode_KeyDown(sender, new KeyEventArgs(Keys.Enter));
                        }
                    }
                }

            }));
        }

        private void txtLeftBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {


                THOK.XC.Process.Dal.ProductStateDal dal = new THOK.XC.Process.Dal.ProductStateDal();
                DataTable dt = dal.GetProductInfoByBarCode(this.txtLeftBarcode.Text.Trim());
                this.txtLeftBarcode.Text = "";
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    this.txtBill_No.Text = dr["BILL_NO"].ToString();
                    this.txtCIGARETTE_NAME.Text = dr["CIGARETTE_NAME"].ToString();
                    this.txtGRADE_NAME.Text = dr["GRADE_NAME"].ToString();
                    this.txtORIGINAL_NAME.Text = dr["ORIGINAL_NAME"].ToString();
                    this.txtProductBarCode.Text = dr["PRODUCT_BARCODE"].ToString();
                    this.txtSTYLE_NAME.Text = dr["STYLE_NAME"].ToString();
                    this.txtWeight.Text = dr["WEIGHT"].ToString();
                }

                
                this.txtRightBarcode.SelectAll();
                this.txtRightBarcode.Focus();
                
            }
            
        }

        private void txtRightBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                THOK.XC.Process.Dal.ProductStateDal dal = new THOK.XC.Process.Dal.ProductStateDal();
                DataTable dt = dal.GetProductInfoByBarCode(this.txtRightBarcode.Text.Trim());
                this.txtRightBarcode.Text = "";
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    this.txtBill_No2.Text = dr["BILL_NO"].ToString();
                    this.txtCIGARETTE_NAME2.Text = dr["CIGARETTE_NAME"].ToString();
                    this.txtGRADE_NAME2.Text = dr["GRADE_NAME"].ToString();
                    this.txtORIGINAL_NAME2.Text = dr["ORIGINAL_NAME"].ToString();
                    this.txtScanCode.Text = dr["PRODUCT_BARCODE"].ToString();
                    this.txtSTYLE_NAME2.Text = dr["STYLE_NAME"].ToString();
                    this.txtWeight2.Text = dr["WEIGHT"].ToString();
                }

                this.txtLeftBarcode.SelectAll();
                this.txtLeftBarcode.Focus();
            }
        }

        private void ReadBarcode_Activated(object sender, EventArgs e)
        {
           
            this.txtLeftBarcode.Focus();
        }

        private void ReadBarcode_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (comm.IsOpen)
                comm.Close();
        }
    }
}
