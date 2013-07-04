using System;
using System.Collections.Generic;
using System.Text;

namespace THOK.MCP.View
{
    public class ViewClickArgs
    {
        private string button;
        private string deviceClass;
        private int deviceNo;
        private string state;
        private string stateDescription;

        public string Button
        {
            get { return button; }
        }

        public string DeviceClass
        {
            get { return deviceClass; }
        }

        public int DeviceNo
        {
            get { return deviceNo; }
        }

        public string State
        {
            get { return state; }
        }

        public string StateDescription
        {
            get { return stateDescription; }
        }

        public ViewClickArgs(string button, string deviceClass, int deviceNo, string state, string stateDescription)
        {
            this.button = button;
            this.deviceClass = deviceClass;
            this.deviceNo = deviceNo;
            this.state = state;
            this.stateDescription = stateDescription;
        }
    }
}
