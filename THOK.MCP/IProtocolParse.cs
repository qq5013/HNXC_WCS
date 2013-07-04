using System;
using System.Collections.Generic;
using System.Text;

namespace THOK.MCP
{
    public interface IProtocolParse
    {
        Message Parse(string msg);
    }
}
