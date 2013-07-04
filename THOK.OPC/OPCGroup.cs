namespace THOK.OPC
{
    using OpcRcw.Comn;
    using OpcRcw.Da;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class OPCGroup : IOPCDataCallback
    {
        private int cookie;
        private object group;
        private string groupName;
        private OPCItemCollection items;
        private OPCServer parent;
        private int serverHandler;
        private int updateRate = 200;

        public event DataChangedEventHandler OnDataChanged;

        internal OPCGroup(OPCServer server, string groupName, int updateRate)
        {
            this.parent = server;
            this.GroupName = groupName;
            this.updateRate = updateRate;
            this.items = new OPCItemCollection();
        }

        private bool AddItem(OPCItem item)
        {
            bool flag = true;
            OPCITEMDEF[] pItemArray = new OPCITEMDEF[1];
            pItemArray[0].szAccessPath = "";
            pItemArray[0].szItemID = item.OPCItemName;
            pItemArray[0].bActive = item.IsActive ? 1 : 0;
            pItemArray[0].hClient = item.ClientHandler;
            pItemArray[0].dwBlobSize = 0;
            pItemArray[0].pBlob = IntPtr.Zero;
            pItemArray[0].vtRequestedDataType = 0;
            IntPtr zero = IntPtr.Zero;
            IntPtr ppErrors = IntPtr.Zero;
            try
            {
                ((IOPCItemMgt) this.group).AddItems(1, pItemArray, out zero, out ppErrors);
                int[] destination = new int[1];
                Marshal.Copy(ppErrors, destination, 0, 1);
                if (destination[0] == 0)
                {
                    OPCITEMRESULT opcitemresult = (OPCITEMRESULT) Marshal.PtrToStructure(zero, typeof(OPCITEMRESULT));
                    item.ServerHandler = opcitemresult.hServer;
                    return flag;
                }
                flag = false;
                throw new Exception("在组中添加项不成功，原因：" + this.parent.GetLastError(destination[0]));
            }
            finally
            {
                if (zero != IntPtr.Zero)
                {
                    Marshal.FreeCoTaskMem(zero);
                    zero = IntPtr.Zero;
                }
                if (ppErrors != IntPtr.Zero)
                {
                    Marshal.FreeCoTaskMem(ppErrors);
                    ppErrors = IntPtr.Zero;
                }
            }
            return flag;
        }

        public OPCItem AddItem(string itemName, string opcItemName, int clientHandler, bool isActive)
        {
            OPCItem item = new OPCItem(this, itemName, opcItemName, clientHandler, isActive);
            this.AddItem(item);
            this.items.Add(item);
            return item;
        }

        public virtual void OnCancelComplete(int dwTransid, int hGroup)
        {
        }

        public virtual void OnDataChange(int dwTransid, int hGroup, int hrMasterquality, int hrMastererror, int dwCount, int[] phClientItems, object[] pvValues, short[] pwQualities, OpcRcw.Da.FILETIME[] pftTimeStamps, int[] pErrors)
        {
            for (int i = 0; i < dwCount; i++)
            {
                object[] objArray;
                OPCItem item = this.items[phClientItems[i]];
                if (pvValues[i] is Array)
                {
                    Array array = (Array) pvValues[i];
                    objArray = new object[array.Length];
                    for (int j = 0; j < array.Length; j++)
                    {
                        objArray[j] = array.GetValue(j);
                    }
                }
                else
                {
                    objArray = new object[] { pvValues[i] };
                }
                if (this.OnDataChanged != null)
                {
                    this.OnDataChanged(this, new DataChangedEventArgs(this.parent.ServerName, this.groupName, item.ItemName, objArray));
                }
            }
        }

        public virtual void OnReadComplete(int dwTransid, int hGroup, int hrMasterquality, int hrMastererror, int dwCount, int[] phClientItems, object[] pvValues, short[] pwQualities, OpcRcw.Da.FILETIME[] pftTimeStamps, int[] pErrors)
        {
        }

        public virtual void OnWriteComplete(int dwTransid, int hGroup, int hrMastererr, int dwCount, int[] pClienthandles, int[] pErrors)
        {
        }

        void IOPCDataCallback.OnCancelComplete(int dwTransid, int hGroup)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void IOPCDataCallback.OnDataChange(int dwTransid, int hGroup, int hrMasterquality, int hrMastererror, int dwCount, int[] phClientItems, object[] pvValues, short[] pwQualities, OpcRcw.Da.FILETIME[] pftTimeStamps, int[] pErrors)
        {
            this.OnDataChange(dwTransid, hGroup, hrMasterquality, hrMastererror, dwCount, phClientItems, pvValues, pwQualities, pftTimeStamps, pErrors);
        }

        void IOPCDataCallback.OnReadComplete(int dwTransid, int hGroup, int hrMasterquality, int hrMastererror, int dwCount, int[] phClientItems, object[] pvValues, short[] pwQualities, OpcRcw.Da.FILETIME[] pftTimeStamps, int[] pErrors)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void IOPCDataCallback.OnWriteComplete(int dwTransid, int hGroup, int hrMastererr, int dwCount, int[] pClienthandles, int[] pErrors)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        internal Array Read(int serverHandler)
        {
            IntPtr zero = IntPtr.Zero;
            IntPtr ppItemValues = IntPtr.Zero;
            ((IOPCSyncIO) this.group).Read(OPCDATASOURCE.OPC_DS_DEVICE, 1, new int[] { serverHandler }, out ppItemValues, out zero);
            int[] destination = new int[1];
            Marshal.Copy(zero, destination, 0, 1);
            if (destination[0] != 0)
            {
                throw new Exception("读OPC数据失败，原因：" + this.parent.GetLastError(destination[0]));
            }
            OPCITEMSTATE opcitemstate = (OPCITEMSTATE) Marshal.PtrToStructure(ppItemValues, typeof(OPCITEMSTATE));
            if (opcitemstate.vDataValue is Array)
            {
                return (Array) opcitemstate.vDataValue;
            }
            return new object[] { Convert.ToInt32(opcitemstate.vDataValue) };
        }

        public void Release()
        {
            IConnectionPoint point;
            IConnectionPointContainer group = (IConnectionPointContainer) this.group;
            Guid gUID = typeof(IOPCDataCallback).GUID;
            group.FindConnectionPoint(ref gUID, out point);
            point.Unadvise(this.cookie);
            foreach (OPCItem item in this.items.AllItem)
            {
                this.RemoveItem(item.ServerHandler);
            }
            this.items.Release();
            Marshal.ReleaseComObject(this.group);
        }

        internal void RemoveItem(int serverHandler)
        {
            IntPtr zero = IntPtr.Zero;
            ((IOPCItemMgt) this.group).RemoveItems(1, new int[] { serverHandler }, out zero);
        }

        public bool SetGroupState(bool active)
        {
            bool flag = true;
            IntPtr zero = IntPtr.Zero;
            try
            {
                ((IOPCItemMgt) this.group).SetActiveState(this.cookie, new int[] { this.serverHandler }, active ? 1 : 0, out zero);
                int[] destination = new int[1];
                Marshal.Copy(zero, destination, 0, 1);
                if (destination[0] != 0)
                {
                    flag = false;
                    throw new Exception("在组中添加项不成功，原因：" + this.parent.GetLastError(destination[0]));
                }
            }
            finally
            {
                if (zero != IntPtr.Zero)
                {
                    Marshal.FreeCoTaskMem(zero);
                    zero = IntPtr.Zero;
                }
            }
            return flag;
        }

        internal void SetParam(int cookie, object group)
        {
            this.cookie = cookie;
            this.group = group;
        }

        internal bool Write(int serverHandler, object state)
        {
            bool flag = true;
            IntPtr zero = IntPtr.Zero;
            ((IOPCSyncIO) this.group).Write(1, new int[] { serverHandler }, new object[] { state }, out zero);
            int[] destination = new int[1];
            Marshal.Copy(zero, destination, 0, 1);
            if (destination[0] != 0)
            {
                flag = false;
                throw new Exception("写入OPC失败，原因：" + this.parent.GetLastError(destination[0]));
            }
            return flag;
        }

        public string GroupName
        {
            get
            {
                return this.groupName;
            }
            set
            {
                this.groupName = value;
            }
        }

        public OPCItemCollection Items
        {
            get
            {
                return this.items;
            }
        }

        public int ServerHandler
        {
            get
            {
                return this.serverHandler;
            }
            set
            {
                this.serverHandler = value;
            }
        }

        public string ServerName
        {
            get
            {
                return this.parent.ServerName;
            }
        }

        public int UpdateRate
        {
            get
            {
                return this.updateRate;
            }
            set
            {
                this.updateRate = value;
            }
        }

        public delegate void DataChangedEventHandler(object sender, DataChangedEventArgs e);
    }
}

