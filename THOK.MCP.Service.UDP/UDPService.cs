using System;
using System.Collections.Generic;
using System.Text;
using THOK.UDP;

namespace THOK.MCP.Service.UDP
{
    class UDPService : THOK.MCP.AbstractService
    {
        private THOK.MCP.IProtocolParse protocol = null;
        private Server server = null;
        private string ip = "127.0.0.1";
        private int port = 5000;

        public override void Initialize(string file)
        {
            Config.Configuration config = new Config.Configuration(file);
            server = new Server();
            ip = config.IP;
            port = config.Port;
            protocol = (IProtocolParse)ObjectFactory.CreateInstance(config.Type);
            server.OnReceive += new ReceiveEventHandler(server_OnReceive);
        }

        void server_OnReceive(object sender, ReceiveEventArgs e)
        {
            THOK.MCP.Message message = null;
            if (null != protocol)
                message = protocol.Parse(e.Message);
            else
            {
                Logger.Error(string.Format("UDPService出错。原因：未能找到消息'{0}'的解析类", e.Message));
                message = new THOK.MCP.Message(e.Message);
            }
            if (message.Parsed)
                DispatchState(message.Command, message.Parameters);
        }

        public override void Release()
        {
            server.StopListen();
        }

        public override void Start()
        {
            server.StartListen(ip, port);
        }

        public override void Stop()
        {
            server.StopListen();
        }

        public override object Read(string itemName)
        {
            throw new Exception("UDPService未实现Read方法，请用System.Net.Sockets.UdpClient类发送UDP消息。");
        }

        public override bool Write(string itemName, object state)
        {
            throw new Exception("UDPService未实现Write方法，请用System.Net.Sockets.UdpClient类发送UDP消息。");
        }
    }
}
