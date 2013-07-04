using System;
using System.Collections.Generic;
using System.Text;

namespace THOK.MCP
{
    public abstract class AbstractService: IService
    {
        private string name = null;

        protected Context context = null;

        public String Name
        {
            get { return name; }
            set { name = value; }
        }

        public event StateChangedEventHandler OnStateChanged = null;

        protected void DispatchState(string itemName, object state)
        {
            if (OnStateChanged != null)
                OnStateChanged(this, new StateChangedArgs(name, itemName, state));
            else
                Logger.Debug(string.Format("未实现对象''的事件OnStateChanged", name));
        }

        public virtual void Invoke(string itemName, object state)
        {
            DispatchState(itemName, state);
        }

        public abstract void Initialize(string file);

        public abstract void Release();

        public abstract void Start();

        public abstract void Stop();

        public abstract object Read(string itemName);

        public abstract bool Write(string itemName, object state);
    }
}
