using System;
using System.Collections.Generic;
using System.Text;

namespace THOK.MCP
{
    public class StateChangedArgs
    {
        private string name;
        private string itemName;
        private object state;

        public string Name
        {
            get { return name; }
        }

        public string ItemName
        {
            get { return itemName; }
        }

        public object State
        {
            get { return state; }
        }

        public StateChangedArgs(string name, string itemName, object state)
        {
            this.name = name;
            this.itemName = itemName;
            this.state = state;
        }
    }
    public delegate void StateChangedEventHandler(object sender, StateChangedArgs e);

    public interface IService
    {
        event StateChangedEventHandler OnStateChanged;

        string Name
        {
            get;
            set;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        void Initialize(string fileName);

        /// <summary>
        /// 释放资源
        /// </summary>
        void Release();

        /// <summary>
        /// 开始服务
        /// </summary>
        void Start();

        /// <summary>
        /// 停止服务
        /// </summary>
        void Stop();

        /// <summary>
        /// 读取项的状态
        /// </summary>
        /// <param name="itemName"></param>
        /// <returns></returns>
        object Read(string itemName);

        /// <summary>
        /// 更改项的状态
        /// </summary>
        /// <param name="itemName">项名称</param>
        /// <param name="state">状态</param>
        /// <returns>更改状态是否成功</returns>
        bool Write(string itemName, object state);

        /// <summary>
        /// 模拟服务运行
        /// </summary>
        /// <param name="itemName">项名称</param>
        /// <param name="state">状态</param>
        void Invoke(string itemName, object state);
    }
}
