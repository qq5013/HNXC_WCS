using System;
using System.Collections.Generic;
using System.Text;

namespace THOK.MCP.Config
{
    public class ResourceConfig
    {
        private string stateCode;
        private string stateDesc;
        private string imageFile;

        public string StateCode
        {
            get { return stateCode; }
        }

        public string StateDesc
        {
            get { return stateDesc; }
        }

        public string ImageFile
        {
            get { return imageFile; }
        }

        public ResourceConfig(string stateCode, string stateDesc, string imageFile)
        {
            this.stateCode = stateCode;
            this.stateDesc = stateDesc;
            this.imageFile = imageFile;
        }
    }
}
