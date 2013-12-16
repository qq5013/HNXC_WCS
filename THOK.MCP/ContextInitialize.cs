using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using THOK.MCP.Config;

namespace THOK.MCP
{
    public sealed class ContextInitialize
    {
        private Dictionary<string, Assembly> assemblies = new Dictionary<string, Assembly>();
        private Configuration configuration = new Configuration();

        public void InitializeContext(Context context)
        {
            configuration.Load("Config.xml");

            Logger.LogLevel = (LogLevel)Enum.Parse(typeof(LogLevel), configuration.LogLevel);
            context.ViewProcess = configuration.ViewProcess;

            InitializeAttributes(context);
            InitializeProcess(context);
            InitializeService(context);            
            configuration.Release();

            //InitializeResourceManager(context);

            context.CompleteInitialize();
        }

        private void InitializeAttributes(Context context)
        {
            foreach (string key in configuration.Attributes.Keys)
                context.Attributes.Add(key, configuration.Attributes[key]); 
        }

        private void InitializeService(Context context)
        {
            foreach (ServiceConfig serviceConfig in configuration.Services)
            {
                IService service = (IService)Create(serviceConfig.Assembly, serviceConfig.ClassName);
                service.Name = serviceConfig.Name;
                service.Initialize(serviceConfig.FileName);
                context.RegisterService(service);
                service.Start();
            }
        }

        private void InitializeProcess(Context context)
        {
            foreach (ProcessConfig processConfig in configuration.Processes)
            {
                IProcess process = (IProcess)Create(processConfig.Assembly, processConfig.ClassName);
                process.Name = processConfig.Name;
                process.Initialize(context);

                if (processConfig.Items.Count != 0)
                    foreach (ProcessItemConfig itemConfig in processConfig.Items)
                        context.RegisterRelation(itemConfig.ServiceName, itemConfig.ItemName, process);
                else
                    context.RegisterProcess(process);

                process.Start();

                //如果初始化要求为挂起，则挂起当前Process
                if (processConfig.Suspend)
                    process.Suspend();
            }
        }

        private void InitializeResourceManager(Context context)
        {
            try
            {
                ResourceConfiguration configuration = new ResourceConfiguration();
                configuration.Load("Monitor.xml");
                DeviceManager deviceManager = new DeviceManager();

                foreach (string key in configuration.Resources.Keys)
                {
                    foreach (ResourceConfig resourceConfig in configuration.Resources[key])
                    {
                        Resource resource = new Resource();
                        resource.DeviceClass = key;
                        resource.StateCode = resourceConfig.StateCode;
                        resource.StateDesc = resourceConfig.StateDesc;
                        System.Drawing.Image image = null;
                        try
                        {
                            if (resourceConfig.ImageFile != null && resourceConfig.ImageFile.Trim().Length != 0)
                                image = System.Drawing.Bitmap.FromFile(resourceConfig.ImageFile);
                        }
                        catch (Exception e)
                        {
                            Logger.Error("ContextInitialize出错。原因：" + e.Message);
                        }
                        resource.Image = image;

                        deviceManager.AddResource(resource);
                    }
                }

                foreach (string key in configuration.Devices.Keys)
                {
                    foreach (DeviceConfig deviceConfig in configuration.Devices[key])
                    {
                        Device device = new Device();
                        device.DeviceClass = key;
                        device.DeviceNo = deviceConfig.DeviceNo;
                        device.X = deviceConfig.X;
                        device.Y = deviceConfig.Y;
                        device.Width = deviceConfig.Width;
                        device.Height = deviceConfig.Height;
                        deviceManager.AddDevice(device);
                    }
                }

                context.DeviceManager = deviceManager;
                configuration.Release();
            }
            catch (Exception e)
            {
                Logger.Error("ContextInitialize出错。原因：" + e.Message);
            }
        }

        private object Create(string assemblyFile, string typeName)
        {
            Assembly assembly = null;
            if (assemblies.ContainsKey(assemblyFile))
                assembly = assemblies[assemblyFile];
            else
            {
                assembly = Assembly.LoadFrom(assemblyFile);
                assemblies.Add(assemblyFile, assembly);
            }
            return assembly.CreateInstance(typeName);
        }
    }
}
