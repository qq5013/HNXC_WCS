using System;
using System.Collections.Generic;
using System.Text;

namespace THOK.MCP
{
    public interface IServiceDispatcher
    {
        void DispatchState(StateItem item);
    }
}
