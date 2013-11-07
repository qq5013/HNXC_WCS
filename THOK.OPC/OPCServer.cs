namespace THOK.OPC
{
    using OpcRcw.Comn;
    using OpcRcw.Da;
    using System;
    using System.Runtime.InteropServices;

    public class OPCServer
    {
        private OPCGroupCollection groups;
        private Guid itemMgtInterface;
        private IOPCServer pIOPCServer;
        private string serverName;

        public OPCServer(string serverName)
        {
            this.serverName = serverName;
        }

        private bool AddGroup(OPCGroup opcGroup)
        {
            bool flag = true;
            float num2 = 0f;
            int phServerGroup = 0;
            GCHandle handle = GCHandle.Alloc(num2, GCHandleType.Pinned);
            IntPtr zero = IntPtr.Zero;
            try
            {
                int num;
                object obj2;
                IConnectionPoint point;
                int num4;
                this.pIOPCServer.AddGroup(opcGroup.GroupName, 1, opcGroup.UpdateRate, 0, zero, IntPtr.Zero, 0x86, out phServerGroup, out num, ref this.itemMgtInterface, out obj2);
                opcGroup.UpdateRate = num;
                opcGroup.ServerHandler = phServerGroup;
                IConnectionPointContainer container = (IConnectionPointContainer) obj2;
                Guid gUID = typeof(IOPCDataCallback).GUID;
                container.FindConnectionPoint(ref gUID, out point);
                point.Advise(opcGroup, out num4);
                opcGroup.SetParam(num4, obj2);
            }
            catch (Exception)
            {
                flag = false;
            }
            finally
            {
                if (handle.IsAllocated)
                {
                    handle.Free();
                }
            }
            return flag;
        }

        public OPCGroup AddGroup(string groupName, int updateRate)
        {
            OPCGroup opcGroup = new OPCGroup(this, groupName, updateRate);
            this.AddGroup(opcGroup);
            this.groups.Add(groupName, opcGroup);
            return opcGroup;
        }

        public void Connect(string serverID)
        {
            this.itemMgtInterface = typeof(IOPCItemMgt).GUID;
            Type typeFromProgID = Type.GetTypeFromProgID(serverID);
            this.pIOPCServer = (IOPCServer) Activator.CreateInstance(typeFromProgID);
            this.groups = new OPCGroupCollection();
        }
        public void Connect(string ProgID,string ServerName)
        {
            this.itemMgtInterface = typeof(IOPCItemMgt).GUID;
            Type typeFromProgID;
            if (ProgID == null)
                typeFromProgID = Type.GetTypeFromProgID(ServerName);
            else
                typeFromProgID = Type.GetTypeFromProgID(ProgID, ServerName);
            this.pIOPCServer = (IOPCServer)Activator.CreateInstance(typeFromProgID);
            this.groups = new OPCGroupCollection();
        }

        internal string GetLastError(int errorID)
        {
            string ppString = "";
            this.pIOPCServer.GetErrorString(errorID, 0x86, out ppString);
            return ppString;
        }

        public void Release()
        {
            this.groups.Release();
            Marshal.ReleaseComObject(this.pIOPCServer);
        }

        private void RemoveGroup(int serverHandler)
        {
            this.pIOPCServer.RemoveGroup(serverHandler, 1);
        }

        public OPCGroupCollection Groups
        {
            get
            {
                return this.groups;
            }
        }

        public string ServerName
        {
            get
            {
                return this.serverName;
            }
        }
    }
}

