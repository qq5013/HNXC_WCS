using System;
using System.Collections.Generic;
using System.Text;

namespace THOK.MCP
{
    public enum LogLevel { ERROR, INFO, DEBUG, ALL };

    public delegate void LogEventHandler(LogEventArgs args);

    public class LogEventArgs
    {
        private LogLevel logLevel;
        private string message;

        public LogLevel LogLevel
        {
            get
            {
                return logLevel;
            }
        }

        public string Message
        {
            get
            {
                return message;
            }
        }

        public LogEventArgs(LogLevel logLevel, string message)
        {
            this.logLevel = logLevel;
            this.message = message;
        }
    }

    public class Logger
    {
        private static LogLevel logLevel = LogLevel.DEBUG;

        public static event LogEventHandler OnLog = null;

        public static LogLevel LogLevel
        {
            get
            {
                return logLevel;
            }
            set
            {
                logLevel = value;
            }
        }

        private Logger()
        {
        }

        public static void Debug(string message)
        {
            if (logLevel >= LogLevel.DEBUG)
            {
                if (OnLog != null)
                {
                    OnLog(new LogEventArgs(LogLevel.DEBUG, message));
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine(string.Format("[DEBUG]\t{0} {1}", DateTime.Now, message));
                }
            }
        }

        public static void Info(String message)
        {
            if (logLevel >= LogLevel.INFO)
            {
                if (OnLog != null)
                {
                    OnLog(new LogEventArgs(LogLevel.INFO, message));
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine(string.Format("[INFO]\t{0} {1}", DateTime.Now, message));
                }
            }
        }

        public static void Error(string message)
        {
            if (logLevel >= LogLevel.ERROR)
            {
                if (OnLog != null)
                {
                    OnLog(new LogEventArgs(LogLevel.ERROR, message));
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine(string.Format("[ERROR]\t{0} {1}", DateTime.Now, message));
                }
            }
        }
    }
}
