using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using THOK.MCP.Config;
using THOK.Util;
using THOK.ParamUtil;


namespace THOK.XC.Dispatching.OperateView
{
    public partial class ParameterForm :THOK.AF.View.ToolbarForm
    {
        private Parameter parameter = new Parameter();
        private DBConfigUtil config = new DBConfigUtil("DefaultConnection", "ORACLE");


        THOK.MCP.Service.TCP.Config.Configuration TcpConfg = new MCP.Service.TCP.Config.Configuration("Crane.xml");

        THOK.MCP.Service.Siemens.Config.Configuration PLC1 = new MCP.Service.Siemens.Config.Configuration("StockPLC_01.xml");

        THOK.MCP.Service.Siemens.Config.Configuration PLC2 = new MCP.Service.Siemens.Config.Configuration("StockPLC_02.xml");
       
        private Dictionary<string, string> attributes = null;

        public ParameterForm()
        {
            InitializeComponent();
            ReadParameter();
        }

        private void ReadParameter()
        {
            //本机数据库连接参数
            parameter.ServerName = config.Parameters["data source"].ToString();
            parameter.DBUser = config.Parameters["user id"].ToString();
            parameter.Password = config.Parameters["password"].ToString();


            //扫描枪--由于使用USB接口，而屏蔽
            //ConfigUtil configUtil = new ConfigUtil();
            //attributes = configUtil.GetAttribute();

            //parameter.ScanPortName = attributes["ScanPortName"];
            //parameter.ScanBaudRate = attributes["ScanBaudRate"];

           

            //堆垛机
            parameter.IP = TcpConfg.IP;
            parameter.Port = TcpConfg.Port;


            //PLC1
            parameter.PLC1ServerName = PLC1.ServerName;
            parameter.PLC1ServerIP = PLC1.ProgID;
            parameter.PLC1GroupString = PLC1.GroupString;
            parameter.PLC1UpdateRate = PLC1.UpdateRate;
            
            //PLC2
            parameter.PLC2ServerName = PLC2.ServerName;
            parameter.PLC2ServerIP = PLC2.ProgID;
            parameter.PLC2GroupString = PLC2.GroupString;
            parameter.PLC2UpdateRate = PLC2.UpdateRate;

            propertyGrid.SelectedObject = parameter;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //保存本机数据库连接参数
                config.Parameters["data source"] = parameter.ServerName;
                config.Parameters["user id"] = parameter.DBUser;
                config.Parameters["Password"] = config.Parameters["Password"].ToString() == parameter.Password ? parameter.Password : THOK.Util.Coding.Encoding(parameter.Password);
                config.Save();


                //由于扫码枪使用USB接口，而屏蔽。
                ////保存Context参数
                //attributes["ScanPortName"] = parameter.ScanPortName;
                //attributes["ScanBaudRate"] = parameter.ScanBaudRate;
               
                //ConfigUtil configUtil = new ConfigUtil();
                //configUtil.Save(attributes);


                TcpConfg.IP = parameter.IP;
                TcpConfg.Port = parameter.Port;
                TcpConfg.Save();

                //PLC1
                PLC1.GroupString = parameter.PLC1GroupString;
                PLC1.ProgID = parameter.PLC1ServerIP;
                PLC1.UpdateRate = parameter.PLC1UpdateRate;
                PLC1.ServerName = parameter.PLC1ServerName;
                PLC1.Save();
                //PLC2
                PLC2.GroupString = parameter.PLC2GroupString;
                PLC2.ProgID = parameter.PLC2ServerIP;
                PLC2.UpdateRate = parameter.PLC2UpdateRate;
                PLC2.ServerName = parameter.PLC2ServerName;
                PLC2.Save();

         

                MessageBox.Show("系统参数保存成功，请重新启动本系统。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception exp)
            {
                MessageBox.Show("保存系统参数过程中出现异常，原因：" + exp.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Exit();
        }
    }
}

