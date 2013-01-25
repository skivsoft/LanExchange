#define __LOG
using System;
using System.IO;
using System.Reflection;
#if LOG
using NLog;
#endif

namespace LanExchange.Utils
{
    public static class LogUtils
    {
        #if LOG
        private readonly static Logger logger = LogManager.GetCurrentClassLogger();
        #endif

        /// <summary>
        /// True indicates that we already try load NLog assembly.
        /// </summary>
        private static bool m_TryLoad;
        /// <summary>
        /// Logger instance.
        /// </summary>
        private static object m_LoggerInstance;
        /// <summary>
        /// Link to method Logger.Info.
        /// </summary>
        private static MethodInfo m_LoggerInfo;
        /// <summary>
        /// Link to method Logger.Error.
        /// </summary>
        private static MethodInfo m_LoggerError;

        #if !LOG
        /// <summary>
        /// Loads NLog assembly, creates Logger instance and gets methods Info and Error from it.
        /// </summary>
        private static bool LoadAssembly()
        {
            if (!m_TryLoad)
            {
                var Params = Environment.GetCommandLineArgs();
                var fileName = Params.Length > 0 ? Params[0] : String.Empty;
                var pathName = Path.GetDirectoryName(fileName);
                if (pathName != null)
                {
                    fileName = Path.Combine(pathName, "NLog.dll");
                    if (File.Exists(fileName))
                    {
                        try
                        {
                            var NLogAssembly = Assembly.LoadFile(fileName);
                            var LogManagerType = NLogAssembly.GetType("NLog.LogManager");
                            var LoggerType = NLogAssembly.GetType("NLog.Logger");
                            if (LoggerType != null)
                            {
                                var list = LoggerType.GetMethods();
                                int count = 0;
                                foreach (var item in list)
                                {
                                    if (item.ToString() == "Void Error(System.String)")
                                    {
                                        m_LoggerError = item;
                                        count++;
                                    }
                                    if (item.ToString() == "Void Info(System.String)")
                                    {
                                        m_LoggerInfo = item;
                                        count++;
                                    }
                                    if (count == 2) break;
                                }
                            }
                            if (LogManagerType != null)
                            {
                                var list = LogManagerType.GetMethods();
                                foreach (var method in list)
                                    if (method.Name == "GetCurrentClassLogger")
                                    {
                                        m_LoggerInstance = method.Invoke(null, null);
                                        break;
                                    }
                            }
                        }
                        catch
                        {
                            m_LoggerInstance = null;
                        }
                    }
                }
                m_TryLoad = true;
            }
            return m_LoggerInstance != null && m_LoggerInfo != null && m_LoggerError != null;
        }
        #endif

        /// <summary>
        /// Calling Logger instance methods Info or Error.
        /// </summary>
        private static void Log(bool error, string message, params object[] args)
        {
            #if LOG
            //var logger = LogManager.GetCurrentClassLogger();
            //logger.Info("qqq");
            if (error)
                logger.Error(message, args);
            else
                logger.Info(message, args);
            #else
            if (LoadAssembly())
            {
                MethodInfo method = error ? m_LoggerError : m_LoggerInfo;
                var strParam = String.Format(message, args);
                method.Invoke(m_LoggerInstance, new object[] {strParam});
            }
            #endif
        }

        public static void Info(string message, params object[] args)
        {
            Log(false, message, args);
        }

        public static void Error(string message, params object[] args)
        {
            Log(true, message, args);
        }

        /// <summary>
        /// Clear configuration. For Mono.
        /// </summary>
        public static void Stop()
        {
            #if LOG
            LogManager.Configuration = null;
            #endif
        }
    }
}
