using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;
using LanExchange.Intf;
using System.Diagnostics;

namespace LanExchange.UI
{
    public static class AppView
    {
        public static void ApplicationRun()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
            Application.ThreadException += ApplicationOnThreadException;
            Application.ThreadExit += ApplicationOnThreadExit;
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false); // must be called before first form created
            Application.Run((Form)App.Resolve<IMainView>());
        }

        private static void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            App.MainView.ApplicationExit();
        }

        [Localizable(false)]
        private static void ApplicationOnThreadException(object sender, ThreadExceptionEventArgs e)
        {
            #if DEBUG
            Debug.Fail(e.Exception.Message + Environment.NewLine + e.Exception.StackTrace);
            #else
            App.MainView.ApplicationExit();
            #endif
        }

        private static void ApplicationOnThreadExit(object sender, EventArgs e)
        {
            App.MainPages.SaveSettings(false);
            App.Config.Save();
        }
    }
}