using System;
using System.Collections.Generic;
using System.Text;

namespace THOK.MCP
{
    public delegate string DialogEventHandler(DialogEventArgs args);
    public class DialogEventArgs
    {
       
        private string message;

       
        public string Message
        {
            get
            {
                return message;
            }
        }

        public DialogEventArgs(string message)
        {
            this.message = message;
        }
    }
    public class FormDialog
    { 

        public static event DialogEventHandler OnDialog = null;


        private FormDialog()
        {
        }
        public static string ShowDialog(string message)
        {
            if (OnDialog != null)
            {
                return OnDialog(new DialogEventArgs(message));
            }
            return "";
        }
    }
}
