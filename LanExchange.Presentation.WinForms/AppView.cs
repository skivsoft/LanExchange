using System;
using LanExchange.Presentation.Interfaces;
using System.Diagnostics.Contracts;
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
            Contract.Requires<ArgumentNullException>(presenter != null);

            this.presenter = presenter;
            presenter.Initialize(this);
        }

        public event ThreadExceptionEventHandler ThreadException
        {
            add { Application.ThreadException += value; }
            remove { Application.ThreadException -= value; }
        }

        public event EventHandler ThreadExit
        {
            add { Application.ThreadExit += value; }
            remove { Application.ThreadExit -= value; }
        }

        public void Run(IWindow mainView)
        {
            Contract.Requires<ArgumentNullException>(mainView != null);
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