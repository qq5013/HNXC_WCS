using System;
using System.Collections.Generic;
using System.Text;

namespace THOK.MCP.View
{
    public class ProgressState
    {
        private string stateDescription = "";
        private int totalStep = 0;
        private int currentStep = 0;
        private bool isFinish = false;

        public string StateDescription
        {
            get { return stateDescription; }
        }

        public int TotalStep
        {
            get { return totalStep; }
        }

        public int CurrentStep
        {
            get { return currentStep; }
        }

        public bool IsFinish
        {
            get { return isFinish; }
        }

        public ProgressState(string stateDescription, int totalStep, int currentStep)
        {
            this.stateDescription = stateDescription;
            this.totalStep = totalStep;
            this.currentStep = currentStep;
        }

        public ProgressState()
        {
            isFinish = true;
        }
    }
}
