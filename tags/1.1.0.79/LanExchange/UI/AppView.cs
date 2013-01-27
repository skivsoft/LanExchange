using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using LanExchange.Presenter;
using LanExchange.UI;
using LanExchange.Utils;

namespace LanExchange.UI
{
    public class AppView
    {
        static void Application_ThreadExit(object sender, EventArgs e)
        {
            LogUtils.Info("ThreadExit");
        }

        static void Application_ApplicationExit(object sender, EventArgs e)
        {
            LogUtils.Info("ApplicationExit");
            // restart after version update
            if (AboutPresenter.NeedRestart)
            {
                LogUtils.Info("Start: {0}", Application.ExecutablePath);
                Process.Start(Application.ExecutablePath);
            }
        }

        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            LogUtils.Error("Thread Exception: {0}\n{1}", e.Exception.Message, e.Exception.StackTrace);
            MainForm.Instance.ApplicationExit();
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            LogUtils.Error("UI Exception: {0}", e.ExceptionObject);
            MainForm.Instance.ApplicationExit();
        }

        public static void ApplicationRun()
        {
            Application.ThreadExit += Application_ThreadExit;
            Application.ApplicationExit += Application_ApplicationExit;
            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;            
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false); // must be called before first form created
            Application.Run(new MainForm());
        }
    }
}
