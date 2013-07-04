using System;
using System.Collections.Generic;
using System.Text;

namespace THOK.MCP
{
    public interface IProcessDispatcher
    {
        /// <summary>
        /// 向Service的某个Item写数据
        /// </summary>
        /// <param name="serverName">Service名称</param>
        /// <param name="itemName">Item名称</param>
        /// <param name="state">要写入的数据</param>
        bool WriteToService(string serverName, string itemName, object state);

        /// <summary>
        /// 读取Service的某个Item的状态
        /// </summary>
        /// <param name="serverName">Service名称</param>
        /// <param name="itemName">Item名称</param>
        /// <returns>状态</returns>
        object WriteToService(string serverName, string itemName);

        /// <summary>
        /// 向Process写数据
        /// </summary>
        /// <param name="processName">Process名称</param>
        /// <param name="itemName">Item名称</param>
        /// <param name="state">要写入的数据</param>
        void WriteToProcess(string processName, string itemName, object state);
    }
}
