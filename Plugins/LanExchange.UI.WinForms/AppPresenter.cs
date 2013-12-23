using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;
using LanExchange.SDK;

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
            // need for changing rtl
            var forms = new List<Form>();
            foreach(Form form in Application.OpenForms)
                forms.Add(form);
            // enum opened forms
            foreach (var form in forms)
                if (form is ITranslationable)
                {
                    // set rtl
                    var rtlChanged = App.TR.RightToLeft != form.RightToLeftLayout;
                    if (rtlChanged)
                        form.Hide();
                    if (App.TR.RightToLeft)
                    {
                        form.RightToLeftLayout = true;
                        form.RightToLeft = RightToLeft.Yes;
                    }
                    else
                    {
                        form.RightToLeftLayout = false;
                        form.RightToLeft = RightToLeft.No;
                    }
                    (form as ITranslationable).TranslateUI();
                    if (rtlChanged)
                        form.Show();
                }
        }
    }
}