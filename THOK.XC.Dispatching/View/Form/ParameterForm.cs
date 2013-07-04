using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using THOK.MCP.Config;
using THOK.XC.Process;
using THOK.XC.Process.Util;
using THOK.Util;

namespace THOK.XC.Dispatching.View
{
    public partial class ParameterForm : THOK.AF.View.ToolbarForm
    {
        private THOK.XC.Process.Parameter parameter = new THOK.XC.Process.Parameter();
        private DBConfigUtil config = new DBConfigUtil("DefaultConnection", "SQLSERVER");
        private DBConfigUtil serverConfig = new DBConfigUtil("ServerConnection", "SQLSERVER");

        private Dictionary<string, string> attributes = null;

        public ParameterForm()
        {
            InitializeComponent();
            ReadParameter();
        }

        private void ReadParameter()
        {
            //读取Context配置文件LED显示屏参数
            ConfigUtil configUtil = new ConfigUtil();
            attributes = configUtil.GetAttribute();
            parameter.LED_01_CHANNELCODE = attributes["LED_01_CHANNELCODE"];
            parameter.LED_02_CHANNELCODE = attributes["LED_02_CHANNELCODE"];
            parameter.SupplyToSortLine = attributes["SupplyToSortLine"];

            //本机数据库连接参数
            parameter.ServerName = config.Parameters["server"].ToString();
            parameter.DBName = config.Parameters["database"].ToString();
            parameter.DBUser = config.Parameters["uid"].ToString();
            parameter.Password = config.Parameters["password"].ToString();

            //服务器数据库连接参数
            parameter.RemoteServerName = serverConfig.Parameters["server"].ToString();
            parameter.RemoteDBName = serverConfig.Parameters["database"].ToString();
            parameter.RemoteDBUser = serverConfig.Parameters["uid"].ToString();
            parameter.RemotePassword = serverConfig.Parameters["password"].ToString();


            propertyGrid.SelectedObject = parameter;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //保存Context配置文件LED显示屏参数
                attributes["LED_01_CHANNELCODE"] = parameter.LED_01_CHANNELCODE;
                attributes["LED_02_CHANNELCODE"] = parameter.LED_02_CHANNELCODE;
                attributes["SupplyToSortLine"] = parameter.SupplyToSortLine;

                ConfigUtil configUtil = new ConfigUtil();
                configUtil.Save(attributes);

                //保存本机数据库连接参数
                config.Parameters["server"] = parameter.ServerName;
                config.Parameters["database"] = parameter.DBName;
                config.Parameters["uid"] = parameter.DBUser;
                config.Parameters["Password"] = config.Parameters["Password"].ToString() == parameter.Password?parameter.Password: THOK.Util.Coding.Encoding(parameter.Password);
                config.Save();

                //保存服务器数据库连接参数
                serverConfig.Parameters["server"] = parameter.RemoteServerName;
                serverConfig.Parameters["database"] = parameter.RemoteDBName;
                serverConfig.Parameters["uid"] = parameter.RemoteDBUser;
                serverConfig.Parameters["Password"] = serverConfig.Parameters["Password"].ToString() == parameter.RemotePassword ? parameter.RemotePassword:THOK.Util.Coding.Encoding(parameter.RemotePassword);
                serverConfig.Save();   


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

