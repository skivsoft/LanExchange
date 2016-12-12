using System;
using LanExchange.Presentation.Interfaces;
using System.Windows.Forms;
using System.Threading;
using System.Collections.Generic;
using System.Linq;

namespace LanExchange.Presentation.WinForms
{
    internal sealed class AppView : IAppView
    {
        private readonly IAppPresenter presenter;

        public AppView(IAppPresenter presenter)
        {
            if (presenter != null) throw new ArgumentNullException(nameof(presenter));

            this.presenter = presenter;
            presenter.Initialize(this);
        }

        public void SetExceptionHandlers()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += ApplicationOnThreadException;
            Application.ThreadExit += ApplicationOnThreadExit;
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

        public void Run(IWindow mainView)
        {
            if (mainView != null) throw new ArgumentNullException(nameof(mainView));
            Application.Run((Form)mainView);
        }

        public void InitVisualStyles()
        {
            Application.EnableVisualStyles();
            // must be called before first form created
            Application.SetCompatibleTextRenderingDefault(false);
        }

        public IEnumerable<IWindow> GetOpenWindows()
        {
            return Application.OpenForms.Cast<IWindow>();
        }

        public void Exit()
        {
            Application.Exit();
        }
    }
}