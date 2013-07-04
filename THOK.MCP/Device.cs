using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace THOK.MCP
{
    public class Device
    {
        private string state = "0";
        private string deviceClass;
        private int deviceNo;
        private Rectangle rect = new Rectangle();

        public int X
        {
            get { return rect.X; }
            set { rect.X = value; }
        }

        public int Y
        {
            get { return rect.Y; }
            set { rect.Y = value; }
        }

        public int Width
        {
            get { return rect.Width; }
            set { rect.Width = value; }
        }

        public int Height
        {
            get { return rect.Height; }
            set { rect.Height = value; }
        }

        public string State
        {
            get { return state; }
            set { state = value; }
        }

        public string DeviceClass
        {
            get { return deviceClass; }
            set { deviceClass = value; }
        }

        public int DeviceNo
        {
            get { return deviceNo; }
            set { deviceNo = value; }
        }

        public virtual void Draw(System.Drawing.Graphics graphics, IDeviceManager resource)
        {
            Resource deviceInfo = resource.GetResource(deviceClass, state);
            if (deviceInfo != null && deviceInfo.Image != null)
                graphics.DrawImage(deviceInfo.Image, rect);
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", deviceClass, deviceNo);
        }

        public bool ClickDevice(int x, int y)
        {
            return rect.Contains(x, y);
        }
    }
}
