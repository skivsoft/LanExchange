using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;
using LanExchange.SDK;
using LanExchange.SDK.Presenter;
using LanExchange.SDK.UI;

namespace LanExchange.UI.WinForms
{
    public class AppPresenter : IAppPresenter
    {
        public void Init()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
            Application.ThreadException += ApplicationOnThreadException;
            Application.ThreadExit += ApplicationOnThreadExit;
            Application.EnableVisualStyles();
            // must be called before first form created
            Application.SetCompatibleTextRenderingDefault(false);
        }

        public void Run(IMainView view)
        {
            Application.Run((Form)view);
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
            App.MainPages.SaveInstant();
            App.Config.Save();
            // dispose instances registered in plugins
            App.Resolve<IDisposableManager>().Dispose();
        }

        public void TranslateOpenForms()
        {
            foreach (var form in Application.OpenForms)
                if (form is ITranslationable)
                    (form as ITranslationable).TranslateUI();
        }
    }
}