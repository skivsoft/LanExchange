using System;
using System.Threading;
using System.Windows.Forms;
using LanExchange.Utils;

namespace LanExchange.UI
{
    public static class AppView
    {
        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            LogUtils.Error("Thread Exception: {0}\n{1}", e.Exception.Message, e.Exception.StackTrace);
            MainForm.Instance.ApplicationExit();
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var E = e.ExceptionObject as Exception;
            LogUtils.Error("UI Exception: {0}\n{1}\n{2}", E.Source, E.Message, E.StackTrace);
            MainForm.Instance.ApplicationExit();
        }

        public static void ApplicationRun()
        {
            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;            
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false); // must be called before first form created
            Application.Run(new MainForm());
        }
    }
}
