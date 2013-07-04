using System;
using System.Collections.Generic;
using System.Text;
using THOK.MCP.Collection;
using THOK.MCP.View;

namespace THOK.MCP
{
    public sealed class Context
    {
        private AttributeCollection attributes = new AttributeCollection();
        
        private ServiceCollection services = new ServiceCollection();

        private ProcessCollection processes = new ProcessCollection();

        private RelationCollection relations = new RelationCollection();

        private IServiceDispatcher serviceDispatcher = null;

        private IProcessDispatcher processDispatcher = null;

        private IDeviceManager deviceManager = null;

        private string viewProcess = "ViewProcess";

        public event EventHandler OnPostInitialize = null;

        /// <summary>
        /// 属性集合
        /// </summary>
        public AttributeCollection Attributes
        {
            get { return attributes; }
        }

        /// <summary>
        /// 服务集合
        /// </summary>
        public ServiceCollection Services
        {
            get { return services; }
        }

        /// <summary>
        /// 处理集合
        /// </summary>
        public ProcessCollection Processes
        {
            get { return processes; }
        }

        /// <summary>
        /// 关系集合
        /// </summary>
        public RelationCollection Relation
        {
            get { return relations; }
        }

        public IProcessDispatcher ProcessDispatcher
        {
            get { return processDispatcher; }
            set { processDispatcher = value; }
        }

        public IDeviceManager DeviceManager
        {
            get { return deviceManager; }
            set { deviceManager = value; }
        }

        public string ViewProcess
        {
            get { return viewProcess; }
            set { viewProcess = value; }
        }

        public Context()
        {
            Dispatcher dispatcher = new Dispatcher(this);
            processDispatcher = dispatcher;
            serviceDispatcher = dispatcher;

            deviceManager = new DeviceManager();
        }

        public void Release()
        {
            processDispatcher = null;
            serviceDispatcher = null;
            deviceManager = null;
            processes = null;
            services = null;
        }

        /// <summary>
        /// 注册Service
        /// </summary>
        /// <param name="service"></param>
        public void RegisterService(IService service)
        {
            service.OnStateChanged += new StateChangedEventHandler(service_OnStateChanged);
            services.Add(service);
        }

        void service_OnStateChanged(object sender, StateChangedArgs e)
        {
            serviceDispatcher.DispatchState(new StateItem(e.Name, e.ItemName, e.State));
        }

        /// <summary>
        /// 注册处理Item的Process
        /// </summary>
        /// <param name="serviceName"></param>
        /// <param name="itemName"></param>
        /// <param name="process"></param>
        public void RegisterRelation(string serviceName, string itemName, IProcess process)
        {
            processes.Add(process);

            if (serviceName != null || serviceName.Trim().Length != 0)
                relations.Add(serviceName, itemName, process.Name);
        }

        /// <summary>
        /// 注册和Service没有联系的Process
        /// </summary>
        /// <param name="process"></param>
        public void RegisterProcess(IProcess process)
        {
            processes.Add(process);
        }

        /// <summary>
        /// 注册界面显示Process
        /// </summary>
        /// <param name="processControl"></param>
        public void RegisterProcessControl(ProcessControl processControl)
        {
            processControl.Initialize(this);
            processes.Add(processControl);
        }

        internal void CompleteInitialize()
        {
            if (OnPostInitialize != null)
                OnPostInitialize(this, new EventArgs());
        }
    }
}
