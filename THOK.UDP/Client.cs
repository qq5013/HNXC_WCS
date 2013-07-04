namespace THOK.UDP
{
    using System;
    using System.IO;
    using System.Net;
    using System.Net.Sockets;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Text;

    public class Client
    {
        private IPEndPoint endPoint;
        private Socket socket;

        public Client(string hostAddress, int port)
        {
            this.endPoint = new IPEndPoint(IPAddress.Parse(hostAddress), port);
            this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        }

        public void Release()
        {
            if (this.socket != null)
            {
                this.socket.Close();
                this.socket = null;
            }
        }

        public void Send(object message)
        {
            byte[] buffer = null;
            IFormatter formatter = new BinaryFormatter();
            Stream serializationStream = new MemoryStream();
            formatter.Serialize(serializationStream, message);
            serializationStream.Read(buffer, 0, (int) serializationStream.Length);
            this.socket.SendTo(buffer, this.endPoint);
        }

        public void Send(string message)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(message);
            this.socket.SendTo(bytes, this.endPoint);
        }
    }
}

