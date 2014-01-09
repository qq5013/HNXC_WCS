using System;
using System.Collections.Generic;
using System.Text;
using THOK.MCP;
using THOK.TCP;
using System.Net.Sockets;
using System.Net;

namespace THOK.MCP.Service.TCP
{
    public class TCPService:THOK.MCP.AbstractService 
    {
        private System.Timers.Timer time1;
        private DateTime dtTime;
        private Server server = null;
        private Client client = null;
        private string ip = "127.0.0.1";
        private int port = 6000;
        private IProtocolParse protocol = null;

        public override void Initialize(string file)
        {
            Config.Configuration config = new Config.Configuration(file);
            protocol = (IProtocolParse)ObjectFactory.CreateInstance(config.Type);

            ip = config.IP;
            port = config.Port;
            server = new Server();
            server.OnReceive += new ReceiveEventHandler(server_OnReceive);
            time1 = new System.Timers.Timer();
            time1.Interval = 10000;
            dtTime = DateTime.Now;
            //time1.Elapsed += new System.Timers.ElapsedEventHandler(OnTimer);

            time1.Elapsed += new System.Timers.ElapsedEventHandler(time1_Elapsed);

            time1.Start();
            
        }

        void time1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            TimeSpan midTime = DateTime.Now - dtTime;
            if (midTime.TotalSeconds >= 45)
            {

                //context.ProcessDispatcher.WriteToService("Crane", "DUM", "<00000CRAN30THOK01DUM0000000>");
                DispatchState("DUU", "");
                dtTime = DateTime.Now;
            }
        }
        //protected void OnTimer(Object source, System.Timers.ElapsedEventArgs e)
        //{
        //    System.Threading.Thread thdInStock = new System.Threading.Thread(new System.Threading.ThreadStart(AutoInStock));
        //    thdInStock.IsBackground = true;
        //    thdInStock.Start();
        //}

        private void AutoInStock()
        {
            TimeSpan midTime = DateTime.Now - dtTime;
            if (midTime.TotalSeconds >= 55)
            {

                context.ProcessDispatcher.WriteToService("Crane", "DUM", "<00000CRAN30THOK01DUM0000000>");
                dtTime = DateTime.Now;
            }

        }

        private void server_OnReceive(object sender, ReceiveEventArgs e)
        {
            Message message = null;
            if (null != protocol)
                message = protocol.Parse(e.Read());
            else
            {
                message = new Message(e.Read());
            }
            string text = string.Format("recv: <--- {0}", e.Read());
            WriteToLog(text);

            if (message.Parsed)
                DispatchState(message.Command, message.Parameters);
            dtTime = DateTime.Now;
            
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
            throw new Exception("TCPService未实现Read方法，请用System.Net.Sockets.TCPClient类发送TCP消息。");
        }

        public override bool Write(string itemName, object state)
        {
            string text = string.Format("send: ---> {0}", (string)state);
            WriteToLog(text);

            //if(server.OnlineCount)

            server.Write(ip + ":" + port.ToString(), (string)state);
            return true;
            //throw new Exception("TCPService未实现Write方法，请用System.Net.Sockets.TCPClient类发送TCP消息。");
            //TcpClient tcpClient = new TcpClient();
            //tcpClient.Connect(IPAddress.Parse(ip), port);

            //NetworkStream ns = tcpClient.GetStream();


            //if (ns.CanWrite)
            //{
            //    Byte[] sendBytes = Encoding.UTF8.GetBytes(state.ToString());
            //    ns.Write(sendBytes, 0, sendBytes.Length);
            //}           

            //ns.Close();
            //tcpClient.Close();

        }

        private void WriteToLog(string Message)
        {
            if (!System.IO.Directory.Exists("Crane"))
                System.IO.Directory.CreateDirectory("Crane");
            string path = "Crane/" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
            System.IO.File.AppendAllText(path, string.Format("{0} :  {1}", DateTime.Now, Message + "\r\n"));
        }
    }
}
