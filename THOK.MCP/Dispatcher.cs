using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace THOK.MCP
{
    public sealed class Dispatcher: IProcessDispatcher, IServiceDispatcher
    {
        private Context context = null;

        private Thread thread = null;

        private AutoResetEvent resetEvent = new AutoResetEvent(false);

        private Queue<StateItem> queue = new Queue<StateItem>();

        private bool isRun = false;

        public Dispatcher(Context context)
        {
            this.context = context;
            thread = new Thread(new ThreadStart(Run));
            thread.IsBackground = true;
            isRun = true;
            thread.Start();
        }

        public bool WriteToService(string serviceName, string itemName, object state)
        {
            bool result = false;
            if (context != null)
            {
                IService service = context.Services[serviceName];

                if (service != null)
                    result = service.Write(itemName, state);
                else
                    throw new MCPException(string.Format("在Context中找不到名称为‘{0}’的Service。", serviceName));
            }
            else
            {
                throw new MCPException("Dispatcher实例的Context属性为空。");
            }
            return result;
        }

        public object WriteToService(string serviceName, string itemName)
        {
            object result = null;
            if (context != null)
            {
                IService service = context.Services[serviceName];
                if (service != null)
                    result = service.Read(itemName);
                else
                    throw new MCPException(string.Format("在Context中找不到名称为‘{0}’的Service。", serviceName));
            }
            else
            {
                throw new MCPException("Dispatcher实例的Context属性为空。");
            }
            return result;
        }

        public void WriteToProcess(string processName, string itemName, object state)
        {
            if (context != null)
            {
                IProcess process = context.Processes[processName];
                if (process != null)
                    process.Process(new StateItem("", itemName, state));
                else
                    throw new MCPException(string.Format("在Context中找不到名称为‘{0}’的Process。", processName));
            }
            else
            {
                throw new MCPException("Dispatcher实例的Context属性为空。");
            }
        }

        public void DispatchState(StateItem item)
        {
            try
            {
                if (item == null)
                {
                    throw new Exception("DispatchState item = null.");
                }
            }
            catch (Exception e)
            {
                Logger.Error(e.TargetSite + "|" + e.Message + "|" + e.StackTrace);
                return;
            }

            lock (queue)
            {
                queue.Enqueue(item);
            }
            resetEvent.Set();
        }

        private void Run()
        {
            while (isRun)
            {
                if (queue.Count == 0)
                {
                    resetEvent.WaitOne();
                }
                else
                {
                    StateItem item = null;
                    lock (queue)
                    {
                        item = queue.Dequeue();
                    }
                    if (item == null)
                    {
                        continue;
                    }
                    
                    List<string> processes = context.Relation.GetProcessName(item.Name, item.ItemName);
                    if (processes != null)
                    {
                        foreach (string processName in processes)
                        {
                            IProcess process = context.Processes[processName];
                            if (process != null)
                                process.Process(item);
                        }
                    }
                    else
                    {
                        Logger.Error(string.Format("没有为{0}的{1}找到Process。", item.Name, item.ItemName));
                    }
                }
            }
        }
    }
}
