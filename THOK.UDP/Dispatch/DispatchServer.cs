namespace THOK.UDP.Dispatch
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Runtime.CompilerServices;
    using THOK.UDP;
    using THOK.UDP.Util;

    public class DispatchServer
    {
        private DataSet clientSet = new DataSet("ClientSet");
        private string name;
        private Server server;

        public event ServerEventHandler OnClientConnect;

        public event ServerEventHandler OnClientDisconnect;

        public event THOK.UDP.Dispatch.ReceiveEventHandler OnReceive;

        public DispatchServer(string name)
        {
            this.name = name;
            this.server = new Server();
            this.server.OnReceive += new THOK.UDP.ReceiveEventHandler(this.server_OnReceive);
            this.LoadTable();
        }

        private DataTable GenerateTable()
        {
            DataTable table = new DataTable("Client");
            table.Columns.Add("Name");
            table.Columns.Add("IP");
            table.Columns.Add("Port");
            table.Columns.Add("Date");
            return table;
        }

        public DataTable GetRegistedClient()
        {
            return this.clientSet.Tables["Client"].Copy();
        }

        private void LoadTable()
        {
            FileInfo info = new FileInfo(@".\Client.xml");
            if (info.Exists)
            {
                this.clientSet.ReadXml(@".\Client.xml");
                if (this.clientSet.Tables.Count == 0)
                {
                    this.clientSet.Tables.Add(this.GenerateTable());
                }
            }
            else
            {
                this.clientSet.Tables.Add(this.GenerateTable());
            }
        }

        private void ProcessMessage(Message message)
        {
            switch (message.Command)
            {
                case "REG":
                    this.RegisterClient(message);
                    return;

                case "UNREG":
                    this.UnregisterClient(message.Sender);
                    return;

                case "CLIENTS":
                    this.ReturnClients(message);
                    return;
            }
            if (this.OnReceive != null)
            {
                this.OnReceive(this, message);
            }
        }

        private void RegisterClient(Message message)
        {
            DataTable table = this.clientSet.Tables["Client"];
            try
            {
                Dictionary<string, string> parameters = message.Parameters;
                string sender = message.Sender;
                string clientIP = parameters["IP"];
                int clientPort = Convert.ToInt32(parameters["Port"]);
                if ((message.Sender != null) || (message.Sender.Trim().Length != 0))
                {
                    DataRow[] rowArray = table.Select(string.Format("Name='{0}'", message.Sender));
                    if (rowArray.Length == 0)
                    {
                        DataRow row = table.NewRow();
                        row["Name"] = sender;
                        row["IP"] = clientIP;
                        row["Port"] = clientPort;
                        row["Date"] = DateTime.Now.ToShortDateString();
                        table.Rows.Add(row);
                    }
                    else
                    {
                        rowArray[0]["Name"] = sender;
                        rowArray[0]["IP"] = clientIP;
                        rowArray[0]["Port"] = clientPort;
                        rowArray[0]["Date"] = DateTime.Now.ToShortDateString();
                    }
                    this.SaveTable();
                    if (this.OnClientConnect != null)
                    {
                        this.OnClientConnect(this, new ServerEventArgs(sender, clientIP, clientPort));
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void ReturnClients(Message message)
        {
            MessageGenerator generator = new MessageGenerator("CLIENTS", this.name);
            generator.AddReceiver(message.Sender);
            foreach (DataRow row in this.clientSet.Tables["Client"].Rows)
            {
                generator.AddParameter("Client", row["Name"].ToString());
            }
            this.Send(message.Sender, generator.GetMessage());
        }

        private void SaveTable()
        {
            this.clientSet.AcceptChanges();
            this.clientSet.WriteXml(@".\Client.xml");
        }

        public void Send(string clientName, string message)
        {
            DataRow[] rowArray = this.clientSet.Tables["Client"].Select(string.Format("Name='{0}'", clientName));
            if (rowArray.Length != 0)
            {
                DataRow row = rowArray[0];
                Client client = new Client(row["IP"].ToString(), Convert.ToInt32(row["Port"]));
                client.Send(message);
                client.Release();
            }
            else if (this.OnReceive != null)
            {
                MessageParser parser = new MessageParser();
                this.OnReceive(this, parser.Parse(message));
            }
        }

        private void server_OnReceive(object sender, ReceiveEventArgs e)
        {
            try
            {
                Message message = new MessageParser().Parse(e.Message);
                List<string> receivers = message.Receivers;
                for (int i = 0; i < receivers.Count; i++)
                {
                    string clientName = receivers[i].ToString();
                    if (clientName.ToUpper() == this.name.ToUpper())
                    {
                        this.ProcessMessage(message);
                    }
                    else
                    {
                        this.Send(clientName, message.Msg);
                    }
                }
            }
            catch
            {
            }
        }

        public void StartListen()
        {
            this.server.StartListen();
        }

        public void StartListen(string address, int port)
        {
            this.server.StartListen(address, port);
        }

        public void StopListen()
        {
            this.server.StopListen();
        }

        public void UnregisterClient(string clientName)
        {
            DataRow[] rowArray = this.clientSet.Tables["Client"].Select(string.Format("Name='{0}'", clientName));
            if (rowArray.Length != 0)
            {
                string clientIP = rowArray[0]["IP"].ToString();
                int clientPort = Convert.ToInt32(rowArray[0]["Port"]);
                rowArray[0].Delete();
                this.SaveTable();
                if (this.OnClientDisconnect != null)
                {
                    this.OnClientDisconnect(this, new ServerEventArgs(clientName, clientIP, clientPort));
                }
            }
        }

        public DataSet Clients
        {
            get
            {
                return this.clientSet;
            }
        }

        public string Name
        {
            get
            {
                return this.name;
            }
        }
    }
}

