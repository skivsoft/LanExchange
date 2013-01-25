using System;
using System.Diagnostics;
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

        public static void ApplicationRun()
        {
            Application.ThreadExit += Application_ThreadExit;
            Application.ApplicationExit += Application_ApplicationExit;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false); // must be called before first form created
            Application.Run(new MainForm());
        }
    }
}
