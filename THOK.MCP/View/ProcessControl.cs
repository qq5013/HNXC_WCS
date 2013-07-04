using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace THOK.MCP.View
{
    public partial class ProcessControl : UserControl, IProcess
    {
        private ProcessState state = ProcessState.Uninitialize;
        private Context context = null;

        protected Context Context
        {
            get { return context; } 
        }

        public ProcessControl()
        {
            InitializeComponent();
        }

        public ProcessState State
        {
            get { return state; }
        }

        public virtual void Initialize(Context context)
        {
            state = ProcessState.Initialized;
            this.context = context;
        }

        public virtual void Release()
        {
            state = ProcessState.Released;
        }

        public virtual void Start()
        {
            state = ProcessState.Stared;
        }

        public virtual void Stop()
        {
            state = ProcessState.Stoped;
        }

        public virtual void Suspend()
        {
            state = ProcessState.Suspend;
        }

        public virtual void Resume()
        {
            state = ProcessState.Processing;
        }

        public virtual void Process(StateItem stateItem)
        {
            state = ProcessState.Waiting;
        }
    }
}
