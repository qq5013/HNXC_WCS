using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace THOK.MCP.View
{
    internal delegate void ProgressEventHandler(StateItem stateItem);

    public partial class MonitorView : ProcessControl
    {
        private Context context = null;

        private Dictionary<string, List<Device>> devices = new Dictionary<string, List<Device>>();

        private IDeviceManager deviceManager = null;

        public MonitorView()
        {
            InitializeComponent();
            //设置双缓冲
            SetStyle(ControlStyles.DoubleBuffer |
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint, true);
        }

        public override void Initialize(Context context)
        {
            this.context = context;
            deviceManager = context.DeviceManager;
            devices = deviceManager.GetDevice();
            progressPanel.Left = (Width - progressPanel.Width) / 2;
            progressPanel.Top = (Height - progressPanel.Height) / 2;
        }

        public override void Process(StateItem stateItem)
        {
            if (stateItem.ItemName == "ProgressState")
            {
                ProcessProgress(stateItem);
            }
            else
            {
                if (devices.ContainsKey(stateItem.ItemName))
                {
                    List<Device> deviceList = devices[stateItem.ItemName];
                    if (stateItem.State is Array)
                    {
                        Array array = (Array)stateItem.State;
                        for (int i = 0; i < array.Length; i++)
                        {
                            if (i < deviceList.Count)
                                deviceList[i].State = array.GetValue(i).ToString();
                            else
                                break;
                        }
                    }
                    else
                    {
                        if (deviceList.Count != 0)
                            deviceList[0].State = stateItem.State.ToString();
                    }
                }
                else
                {
                    MessageBox.Show(string.Format("没有找到名称为‘{0}’的设备。", stateItem.ItemName), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                Invalidate();
            }
        }

        private void ProcessProgress(StateItem stateItem)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new ProgressEventHandler(ProcessProgress), stateItem);
            }
            else
            {
                if (!progressPanel.Visible)
                    progressPanel.Visible = true;
                ProgressState progressState = (ProgressState)stateItem.State;

                if (progressState.IsFinish)
                    progressPanel.Visible = false;
                else
                {
                    lblProgress.Text = progressState.StateDescription;
                    progressBar.Maximum = progressState.TotalStep;
                    progressBar.Value = progressState.CurrentStep;
                    Application.DoEvents();
                }
            }
        }

        private void MonitorView_Paint(object sender, PaintEventArgs e)
        {
            foreach (List<Device> deviceList in devices.Values)
            {
                foreach (Device device in deviceList)
                {
                    device.Draw(e.Graphics, deviceManager);
                }
            }
        }

        private void MonitorView_MouseDown(object sender, MouseEventArgs e)
        {
            bool clicked = false;
            foreach (List<Device> deviceList in devices.Values)
            {
                foreach (Device device in deviceList)
                {
                    if (device.ClickDevice(e.X, e.Y))
                    {
                        try
                        {
                            Resource info = deviceManager.GetResource(device.DeviceClass, device.State);
                            string stateDesc = null;
                            if (info != null)
                            {
                                stateDesc = info.StateDesc;
                            }

                            ViewClickArgs state = new ViewClickArgs(e.Button.ToString(),
                                                                    device.DeviceClass,
                                                                    device.DeviceNo,
                                                                    device.State,
                                                                    stateDesc);

                            context.ProcessDispatcher.WriteToProcess(context.ViewProcess, "DeviceClick", state);
                        }
                        catch (Exception ex)
                        {
                            Logger.Debug("MonitorView出错。原因：" + ex.Message);
                        }
                        clicked = true;
                        break;
                    }
                }
                if (clicked)
                    break;
            }
        }
    }
}

