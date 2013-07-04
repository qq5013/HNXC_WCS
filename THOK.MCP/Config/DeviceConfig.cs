using System;
using System.Collections.Generic;
using System.Text;

namespace THOK.MCP.Config
{
    public class DeviceConfig
    {
        private int deviceNo;
        private int x;
        private int y;
        private int width;
        private int height;

        public int DeviceNo
        {
            get { return deviceNo; }
        }

        public int X
        {
            get { return x; }
        }

        public int Y
        {
            get { return y; }
        }

        public int Width
        {
            get { return width; }
        }

        public int Height
        {
            get { return height; }
        }

        public DeviceConfig(int deviceNo, int x, int y, int width, int height)
        {
            this.deviceNo = deviceNo;
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }
    }
}
