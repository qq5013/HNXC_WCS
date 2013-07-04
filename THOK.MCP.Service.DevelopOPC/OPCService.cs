using System;
using System.Collections.Generic;
using System.Text;
using THOK.OPC;

namespace THOK.MCP.Service.DevelopOPC
{
    public class OPCService: THOK.MCP.AbstractService
    {
        private OPCServer opcServer = null;
        public override void Initialize(string file)
        {
            opcServer = new OPCServer(Name);

            Config.Configuration config = new Config.Configuration(file);
            opcServer.Connect(config.ConnectionString);

            OPCGroup group = opcServer.AddGroup(config.GroupName, config.UpdateRate);
            foreach (Config.ItemInfo item in config.Items)
            {
                group.AddItem(item.ItemName, item.OpcItemName, item.ClientHandler, item.IsActive);
            }
            opcServer.Groups.DefaultGroup.OnDataChanged += new OPCGroup.DataChangedEventHandler(DefaultGroup_OnDataChanged);
        }

        void DefaultGroup_OnDataChanged(object sender, DataChangedEventArgs e)
        {
            DispatchState(e.ItemName, e.States);
        }

        public override void Release()
        {
            opcServer.Release();
        }

        public override void Start()
        {
            
        }

        public override void Stop()
        {
            
        }

        public override object Read(string itemName)
        {
            return null;
        }

        public override bool Write(string itemName, object state)
        {
            string msg = "";
            if (state is Array)
            {
                Array tmp = (Array)state;
                msg = (itemName + " = ");
                for (int i = 0; i < tmp.Length; i++)
                    msg += string.Format("[{0}]", tmp.GetValue(i));

            }
            else
            {
                msg = string.Format("{0} = {1}", itemName, state);
                
            }
            Logger.Debug(msg);
            System.Diagnostics.Debug.WriteLine(msg);
            return true;
        }
    }
}
