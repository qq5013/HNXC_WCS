using System;
using System.Collections.Generic;
using System.Text;

namespace THOK.MCP
{
    public sealed class DeviceManager: IDeviceManager
    {
        private Dictionary<string, Resource> states = new Dictionary<string, Resource>();
        private Dictionary<string, List<Device>> devices = new Dictionary<string, List<Device>>();

        

        public void AddResource(Resource resource)
        {
            string key = string.Format("{0}.{1}", resource.DeviceClass, resource.StateCode);
            if (!states.ContainsKey(key))
                states.Add(key, resource);
            else
                states[key] = resource;
        }

        public void AddDevice(Device device)
        {
            if (devices.ContainsKey(device.DeviceClass))
            {
                if (!devices[device.DeviceClass].Contains(device))
                    devices[device.DeviceClass].Add(device);
            }
            else
            {
                List<Device> deviceList = new List<Device>();
                deviceList.Add(device);
                devices.Add(device.DeviceClass, deviceList);
            }
        }

        public Resource GetResource(string deviceClass, string stateCode)
        {
            string key = string.Format("{0}.{1}", deviceClass, stateCode);
            if (states.ContainsKey(key))
                return states[key];
            else
                return null;
        }

        public Dictionary<string, List<Device>> GetDevice()
        {
            return devices;
        }

    }
}
