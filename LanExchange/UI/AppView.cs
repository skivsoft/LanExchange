using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;
using LanExchange.Intf;

namespace LanExchange.UI
{
    public static class AppView
    {
        [Localizable(false)]
        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            #if DEBUG
            MessageBox.Show(e.Exception.Message+Environment.NewLine+e.Exception.StackTrace,
              "Error in "+e.Exception.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            #else
            App.MainView.ApplicationExit();
            #endif
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            App.MainView.ApplicationExit();
        }

        public static void ApplicationRun()
        {
            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;            
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false); // must be called before first form created
            Application.Run((Form)App.Resolve<IMainView>());
        }
    }
}