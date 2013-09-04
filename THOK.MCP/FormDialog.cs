using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace THOK.MCP
{
    public delegate string DialogEventHandler(DialogEventArgs args);
    public class DialogEventArgs
    {
       
        private string[] message;
        private DataTable dtinfo;
       
        public string[] Message
        {
            get
            {
                return message;
            }
        }
        public DataTable dtInfo
        {
            get
            {
                return dtinfo;
            }
        }

        public DialogEventArgs(string[] message,DataTable dt)
        {
            this.message = message;
            this.dtinfo = dt;
        }
    }
    public class FormDialog
    { 

        public static event DialogEventHandler OnDialog = null;


        private FormDialog()
        {
        }
        public static string ShowDialog(string[] message,DataTable dt)
        {
            if (OnDialog != null)
            {
                return OnDialog(new DialogEventArgs(message, dt));
            }
            return "";
        }
    }
}
