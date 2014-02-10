using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace THOK.XC.Dispatching.WCS
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

           int height= Screen.PrimaryScreen.WorkingArea.Height;
           int weight = Screen.PrimaryScreen.WorkingArea.Width;
           decimal d = (decimal)weight / height;
           if (d >= (decimal)1.6)
               Application.Run(new MainForm());
           else
               Application.Run(new Main());
        }
    }
}