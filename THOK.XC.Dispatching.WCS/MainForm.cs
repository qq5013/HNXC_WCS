using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using THOK.MCP;

namespace THOK.XC.Dispatching.WCS
{
    public partial class MainForm : Form
    {
        private Context context = null;
        
        public MainForm()
        {
            InitializeComponent();
        }

        private void CreateDirectory(string directoryName)
        {
            if (!System.IO.Directory.Exists(directoryName))
                System.IO.Directory.CreateDirectory(directoryName);
        }

        private void WriteLoggerFile(string text)
        {
            try
            {
                string path = "";
                CreateDirectory("日志");
                path = "日志";
                path = path + @"/" + DateTime.Now.ToString().Substring(0, 4).Trim();
                CreateDirectory(path);
                path = path + @"/" + DateTime.Now.ToString().Substring(0, 7).Trim();
                path = path.TrimEnd(new char[] { '-'});
                CreateDirectory(path);
                path = path + @"/" + DateTime.Now.ToShortDateString() + ".txt";
                System.IO.File.AppendAllText(path, string.Format("{0} {1}", DateTime.Now, text + "\r\n"));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        void Logger_OnLog(THOK.MCP.LogEventArgs args)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new LogEventHandler(Logger_OnLog), args);
            }
            else
            {
                lock (lbLog)
                {
                    string msg = string.Format("[{0}] {1} {2}", args.LogLevel, DateTime.Now, args.Message);
                    lbLog.Items.Insert(0, msg);
                    WriteLoggerFile(msg);
                }
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            context.Release();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
                e.Cancel = true;
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            lblTitle.Left = (pnlTitle.Width - lblTitle.Width) / 2;
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            try
            {
                Logger.OnLog += new LogEventHandler(Logger_OnLog);

                context = new Context();

                ContextInitialize initialize = new ContextInitialize();
                context.RegisterProcessControl(buttonArea);
                initialize.InitializeContext(context);
                context.RegisterProcessControl(monitorView);

                //context.Processes["DynamicShowProcess"].Resume();
            }
            catch (Exception ee)
            {
                Logger.Error("初始化处理失败请检查配置，原因：" + ee.Message);
            }
        }
    }
}