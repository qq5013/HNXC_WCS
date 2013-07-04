using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace THOK.MCP
{
    public abstract class AbstractProcess: IProcess
    {
        private String name = null;

        private bool isRun = false;

        private bool isSuspend = false;

        private AutoResetEvent resetEvent = new AutoResetEvent(false);

        private Thread thread = null;

        private Queue<StateItem> queue = new Queue<StateItem>();

        private Context context = null;

        private ProcessState state = ProcessState.Uninitialize;

        protected Context Context
        {
            get { return context; }
        }

        public String Name
        {
            get { return name; }
            set { name = value; }
        }

        public ProcessState State
        {
            get { return state; }
        }

        public virtual void Initialize(Context context)
        {
            this.context = context;
            state = ProcessState.Initialized;
        }

        public virtual void Release()
        {
            state = ProcessState.Released;
        }
        
        public void Start()
        {
            thread = new Thread(new ThreadStart(Run));
            thread.IsBackground = true;
            isRun = true;
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            state = ProcessState.Stared;
        }

        public void Stop()
        {
            isRun = false;
            resetEvent.Set();
            state = ProcessState.Stoped;
        }

        public void Suspend()
        {
            isSuspend = true;
            resetEvent.Set();
        }

        public void Resume()
        {
            isSuspend = false;
            resetEvent.Set();
        }

        public void Process(StateItem item)
        {
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
                if (queue.Count == 0 || isSuspend)
                {
                    resetEvent.WaitOne();
                    if (isSuspend)
                        state = ProcessState.Suspend;
                    else
                        state = ProcessState.Waiting;
                }
                else
                {
                    state = ProcessState.Processing;
                    StateItem item = null;
                    lock (queue)
                    {
                        item = queue.Dequeue();
                    }
                    
                    try
                    {
                        IProcessDispatcher dispatcher = null;
                        if (context != null)
                            dispatcher = context.ProcessDispatcher;
                        else
                            dispatcher = new DefaultDispatcher();
                        StateChanged(item, dispatcher);
                    }
                    catch (Exception e)
                    {
                        Logger.Error(string.Format("{0}³ö´í¡£Ô­Òò£º{1}", GetType(), e.Message));
                    }
                }
            }
        }

        protected abstract void StateChanged(StateItem stateItem, IProcessDispatcher dispatcher);

        protected void WriteToProcess(string processName, string itemName, object state)
        {
            Context.ProcessDispatcher.WriteToProcess(processName, itemName, state);
        }

        protected bool WriteToService(string serviceName, string itemName, object state)
        {
            return Context.ProcessDispatcher.WriteToService(serviceName, itemName, state);
        }

        protected object WriteToService(string serviceName, string itemName)
        {
            return Context.ProcessDispatcher.WriteToService(serviceName, itemName);
        }
    }
}
