using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using THOK.ParamUtil;

namespace THOK.XC.Process
{
    public class Parameter: BaseObject
    {
        private string serverName;

        [CategoryAttribute("本机数据库连接参数"), DescriptionAttribute("数据库服务器名称"), Chinese("服务器名称")]
        public string ServerName
        {
            get { return serverName; }
            set { serverName = value; }
        }

        private string dbName;

        [CategoryAttribute("本机数据库连接参数"), DescriptionAttribute("数据库名称"), Chinese("数据库名")]
        public string DBName
        {
            get { return dbName; }
            set { dbName = value; }
        }

        private string dbUser;

        [CategoryAttribute("本机数据库连接参数"), DescriptionAttribute("数据库连接用户名"), Chinese("用户名")]
        public string DBUser
        {
            get { return dbUser; }
            set { dbUser = value; }
        }
        private string password;

        [CategoryAttribute("本机数据库连接参数"), DescriptionAttribute("数据库连接密码"), Chinese("密码")]
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        private string remoteServerName;

        [CategoryAttribute("服务器数据库连接参数"), DescriptionAttribute("数据库服务器名称"), Chinese("服务器名称")]
        public string RemoteServerName
        {
            get { return remoteServerName; }
            set { remoteServerName = value; }
        }

        private string remoteDBName;

        [CategoryAttribute("服务器数据库连接参数"), DescriptionAttribute("数据库名称"), Chinese("数据库名")]
        public string RemoteDBName
        {
            get { return remoteDBName; }
            set { remoteDBName = value; }
        }

        private string remoteDBUser;

        [CategoryAttribute("服务器数据库连接参数"), DescriptionAttribute("数据库连接用户名"), Chinese("用户名")]
        public string RemoteDBUser
        {
            get { return remoteDBUser; }
            set { remoteDBUser = value; }
        }
        private string remotePassword;

        [CategoryAttribute("服务器数据库连接参数"), DescriptionAttribute("数据库连接密码"), Chinese("密码")]
        public string RemotePassword
        {
            get { return remotePassword; }
            set { remotePassword = value; }
        }

        private int port;

        [CategoryAttribute("备货系统通信参数"), DescriptionAttribute("备货系统监听端口"), Chinese("监听端口")]
        public int Port
        {
            get { return port; }
            set { port = value; }
        }

        private string ip;

        [CategoryAttribute("备货系统通信参数"), DescriptionAttribute("备货系统IP地址"), Chinese("IP地址")]
        public string IP
        {
            get { return ip; }
            set { ip = value; }
        }

        private string portName;

        [CategoryAttribute("扫码器通信参数"), DescriptionAttribute("扫码器串口号"), Chinese("串口号")]
        public string PortName
        {
            get { return portName; }
            set { portName = value; }
        }

        private string baudRate;

        [CategoryAttribute("扫码器通信参数"), DescriptionAttribute("扫码器波特率"), Chinese("波特率")]
        public string BaudRate
        {
            get { return baudRate; }
            set { baudRate = value; }
        }

        private string parity;

        [CategoryAttribute("扫码器通信参数"), DescriptionAttribute("扫码器较验位"), Chinese("较验位")]
        public string Parity
        {
            get { return parity; }
            set { parity = value; }
        }

        private string dataBits;

        [CategoryAttribute("扫码器通信参数"), DescriptionAttribute("扫码器数据位"), Chinese("数据位")]
        public string DataBits 
        {
            get { return dataBits; }
            set { dataBits = value; }
        }

        private string stopBits;

        [CategoryAttribute("扫码器通信参数"), DescriptionAttribute("扫码器停止位"), Chinese("停止位")]
        public string StopBits 
        {
            get { return stopBits; }
            set { stopBits = value; }
        }

        private string led_01_ChannelCode = "";

        [CategoryAttribute("LED显示屏参数"), DescriptionAttribute("一号屏烟道编码"), Chinese("一号屏烟道编码")]
        public string LED_01_CHANNELCODE
        {
            get { return led_01_ChannelCode; }
            set { led_01_ChannelCode = value; }
        }

        private string led_02_ChannelCode = "";

        [CategoryAttribute("LED显示屏参数"), DescriptionAttribute("二号屏烟道编码"), Chinese("二号屏烟道编码")]
        public string LED_02_CHANNELCODE
        {
            get { return led_02_ChannelCode; }
            set { led_02_ChannelCode = value; }
        }

        private string led_03_ChannelCode = "";

        [CategoryAttribute("LED显示屏参数"), DescriptionAttribute("三号屏烟道编码"), Chinese("三号屏烟道编码")]
        public string LED_03_CHANNELCODE
        {
            get { return led_03_ChannelCode; }
            set { led_03_ChannelCode = value; }
        }

        private string supplyToSortLine = "";

        [CategoryAttribute("补货方向强制干预控制"), DescriptionAttribute("分拣线代码［00：自动（不干预）；01：一号线；02：二号线；］"), Chinese("分拣线代码［00：自动（不干预）；01：一号线；02：二号线；］")]
        public string SupplyToSortLine
        {
            get { return supplyToSortLine; }
            set { supplyToSortLine = value; }
        }
    }
}
