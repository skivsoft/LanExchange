using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using LanExchange.Presentation.Interfaces;
using WinFormsApplication = System.Windows.Forms.Application;

namespace LanExchange.Presentation.WinForms
{
    internal sealed class AppView : IAppView
    {
        private readonly IAppPresenter presenter;

        public AppView(IAppPresenter presenter)
        {
            if (presenter == null) throw new ArgumentNullException(nameof(presenter));

            this.presenter = presenter;
            presenter.Initialize(this);
        }

        public void SetExceptionHandlers()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
            WinFormsApplication.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            WinFormsApplication.ThreadException += ApplicationOnThreadException;
            WinFormsApplication.ThreadExit += ApplicationOnThreadExit;
        }

        public void Run(IWindow mainView)
        {
            if (mainView == null) throw new ArgumentNullException(nameof(mainView));
            WinFormsApplication.Run((Form)mainView);
        }

        public void InitVisualStyles()
        {
            WinFormsApplication.EnableVisualStyles();

            // must be called before first form created
            WinFormsApplication.SetCompatibleTextRenderingDefault(false);
        }

        public IEnumerable<IWindow> GetOpenWindows()
        {
            return System.Windows.Forms.Application.OpenForms.Cast<IWindow>();
        }

        public void Exit()
        {
            System.Windows.Forms.Application.Exit();
        }

        private void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            presenter.OnNonUIException((Exception)e.ExceptionObject);
        }

        private void ApplicationOnThreadException(object sender, ThreadExceptionEventArgs e)
        {
            presenter.OnUIException(e.Exception);
        }

        private void ApplicationOnThreadExit(object sender, EventArgs e)
        {
            presenter.OnExit();
        }
    }
}