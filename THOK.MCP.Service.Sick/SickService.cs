using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using THOK.MCP;

namespace THOK.MCP.Service.Sick
{
    public class SickService:THOK.MCP.AbstractService
    {
        private Thread thread = null;
        private AutoResetEvent resetEvent = new AutoResetEvent(false);
        private IProtocolParse protocol = null;
        private System.IO.Ports.SerialPort serialPort = new System.IO.Ports.SerialPort();
        private bool isConnected = false;
        private bool isHex = false;

        private char strHead = (char)byte.Parse("02");
        private char strTail = (char)byte.Parse("03");

        /// <summary>
        /// 开始字符
        /// </summary>
        public char HeadString
        {
            get
            {
                return this.strHead;
            }
            set
            {
                this.strHead = value;
            }
        }
        /// <summary>
        /// 结束字符
        /// </summary>
        public char TailString
        {
            get
            {
                return this.strTail;
            }
            set
            {
                this.strTail = value;
            }
        }

        public bool IsConnected
        {
            get { return isConnected; }
        }

        public override void Initialize(string file)
        {
            Config.Configuration config = new Config.Configuration(file);
            if (config.Type != null)
                protocol = (IProtocolParse)ObjectFactory.CreateInstance(config.Type);
            else
                protocol = new BarcodeParse();
            BarcodeParse.separator = config.Separator;

            serialPort.ReadTimeout = 100;
            serialPort.PortName = config.PortName;
            serialPort.BaudRate = config.BaudRate;
            serialPort.Parity = config.Parity;
            serialPort.DataBits = config.DataBits;
            serialPort.StopBits = config.StopBits;

            isHex = config.IsHex;

            serialPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(serialPort_DataReceived);
        }

        void serialPort_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            resetEvent.Set();
        }

        void Run()
        {
            resetEvent.WaitOne();
            while (isConnected && serialPort.IsOpen)
            {
                while (isConnected && serialPort.IsOpen && serialPort.BytesToRead == 0)
                {
                    resetEvent.WaitOne(100,false);
                }
                
                string msg = null;
                if (isHex)
                {
                    int bytes = serialPort.BytesToRead;
                    byte[] buffer = new byte[bytes];
                    serialPort.Read(buffer, 0, bytes);
                    msg = BitConverter.ToString(buffer);
                }
                else
                {
                    msg = serialPort.ReadExisting();
                }

                Message message = null;

                if (null != protocol)
                {
                    message = protocol.Parse(msg);
                }
                else
                {
                    message = new Message(msg);
                }

                if (message.Parsed)
                {
                    DispatchState(message.Command, message.Parameters);
                }
            }
        }

        public override void Release()
        {
            Stop();
        }

        public override void Start()
        {
            try
            {
                serialPort.Open();
                isConnected = true;
                
                thread = new Thread(new ThreadStart(Run));
                thread.IsBackground = true;
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
            }
            catch
            {
                isConnected = false;
            }
        }

        public override void Stop()
        {
            if (serialPort.IsOpen)
                serialPort.Close();
            isConnected = false;
        }

        public override object Read(string itemName)
        {
            throw new Exception("SickService未实现Read方法。");
        }

        public override bool Write(string itemName, object state)
        {
            throw new Exception("SickService未实现Write方法。");
        }
    }
}
