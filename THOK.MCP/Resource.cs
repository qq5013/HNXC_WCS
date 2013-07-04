using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace THOK.MCP
{
    public class Resource
    {
        private string deviceClass;

        private string stateCode;

        private string stateDesc;

        private Image image;

        /// <summary>
        /// Éè±¸Àà±ð
        /// </summary>
        public string DeviceClass
        {
            get { return deviceClass; }
            set { deviceClass = value; }
        }

        /// <summary>
        /// ×´Ì¬´úÂë
        /// </summary>
        public string StateCode
        {
            get { return stateCode; }
            set { stateCode = value; }
        }

        /// <summary>
        /// ×´Ì¬ÃèÊö
        /// </summary>
        public string StateDesc
        {
            get { return stateDesc; }
            set { stateDesc = value; }
        }

        /// <summary>
        /// ×´Ì¬Í¼Æ¬
        /// </summary>
        public Image Image
        {
            get { return image; }
            set { image = value; }
        }
    }
}
