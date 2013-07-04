using System;
using System.Collections.Generic;
using System.Text;

namespace THOK.MCP
{
    public class MCPException: Exception
    {
        public MCPException(string message)
            : base(message)
        {
        }
    }
}
