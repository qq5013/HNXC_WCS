using System;
using System.Collections.Generic;
using System.Text;

namespace THOK.MCP
{
    public enum ProcessState {Uninitialize, Initialized, Released, Stared, Stoped, Suspend, Waiting, Processing};
    public interface IProcess
    {
        String Name
        {
            get;
            set;
        }

        ProcessState State
        {
            get;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        void Initialize(Context context);

        /// <summary>
        /// 释放资源
        /// </summary>
        void Release();

        /// <summary>
        /// 开始处理
        /// </summary>
        void Start();

        /// <summary>
        /// 停止处理
        /// </summary>
        void Stop();

        /// <summary>
        /// 挂起处理
        /// </summary>
        void Suspend();

        /// <summary>
        /// 开始挂起的处理
        /// </summary>
        void Resume();

        /// <summary>
        /// 处理请求
        /// </summary>
        /// <param name="stateItem">需要处理的状态</param>
        void Process(StateItem stateItem);
    }
}
