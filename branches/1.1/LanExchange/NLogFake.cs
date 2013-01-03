#if !DEBUG
using System;
using System.Collections.Generic;
using System.Text;

namespace NLog
{
    public class Logger
    {
        public void Info(string message, params object[] args) { }
        public void Error(string message, params object[] args) { }
        public void Trace(string message, params object[] args) { }
    }

    public class LogManager
    {
        private static Logger logger = null;

        public static object Configuration = null;

        public static Logger GetCurrentClassLogger()
        {
            if (logger == null)
            {
                logger = new Logger();
            }
            return logger;
        }
    }
}
#endif