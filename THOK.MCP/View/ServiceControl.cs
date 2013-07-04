using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace THOK.MCP.View
{
    public partial class ServiceControl : UserControl, IService
    {
        public event StateChangedEventHandler OnStateChanged;

        public ServiceControl()
        {
            InitializeComponent();
        }

        protected void DispatchState(string itemName, object state)
        {
            if (OnStateChanged != null)
                OnStateChanged(this, new StateChangedArgs(Name, itemName, state));
            else
                Logger.Debug(string.Format("未实现对象''的事件OnStateChanged", Name));
        }

        public virtual void Initialize(string fileName)
        {
            
        }

        public virtual void Release()
        {

        }

        public virtual void Start()
        {
            
        }

        public virtual void Stop()
        {
            
        }

        public virtual object Read(string itemName)
        {
            return null;
        }

        public virtual bool Write(string itemName, object state)
        {
            return true;
        }

        public virtual void Invoke(string itemName, object state)
        {
            DispatchState(itemName, state);
        }
    }
}
