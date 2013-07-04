using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace THOK.MCP.Config
{
    public class ResourceConfiguration
    {
        private Dictionary<string, List<ResourceConfig>> resources = new Dictionary<string, List<ResourceConfig>>();
        private Dictionary<string, List<DeviceConfig>> devices = new Dictionary<string, List<DeviceConfig>>();

        public Dictionary<string, List<ResourceConfig>> Resources
        {
            get { return resources; }
        }

        public Dictionary<string, List<DeviceConfig>> Devices
        {
            get { return devices; }
        }

        public void Load(string fileName)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(fileName);

            ReadResources(doc);
            ReadDevices(doc);
        }

        public void Release()
        {
            resources.Clear();
            devices.Clear();
            resources = null;
            devices = null;
        }

        private void ReadResources(XmlDocument doc)
        {
            XmlNode resourcesNode = doc.GetElementsByTagName("Resources")[0];
            foreach (XmlNode deviceClass in resourcesNode.ChildNodes)
            {
                List<ResourceConfig> resourceList = new List<ResourceConfig>();
                foreach (XmlNode resourceNode in deviceClass.ChildNodes)
                {
                    ResourceConfig resourceConfig = new ResourceConfig(resourceNode.Attributes["StateCode"].Value,
                                                                    resourceNode.Attributes["StateDesc"].Value,
                                                                    resourceNode.Attributes["Image"].Value);
                    resourceList.Add(resourceConfig);
                }

                resources.Add(deviceClass.Attributes["Name"].Value, resourceList);
            }
        }

        private void ReadDevices(XmlDocument doc)
        {
            XmlNode devicesNode = doc.GetElementsByTagName("Devices")[0];
            foreach (XmlNode deviceClass in devicesNode.ChildNodes)
            {
                List<DeviceConfig> deviceList = new List<DeviceConfig>();
                foreach (XmlNode deviceNode in deviceClass.ChildNodes)
                {
                    DeviceConfig deviceConfig = new DeviceConfig(Convert.ToInt32(deviceNode.Attributes["DeviceNo"].Value),
                                                            Convert.ToInt32(deviceNode.Attributes["X"].Value),
                                                            Convert.ToInt32(deviceNode.Attributes["Y"].Value),
                                                            Convert.ToInt32(deviceNode.Attributes["Width"].Value),
                                                            Convert.ToInt32(deviceNode.Attributes["Height"].Value));
                    deviceList.Add(deviceConfig);
                }

                devices.Add(deviceClass.Attributes["Name"].Value, deviceList);
            }
        }
    }
}
