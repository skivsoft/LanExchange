using System;
using LanExchange.Presentation.Interfaces;
using System.Diagnostics.Contracts;
using System.Windows.Forms;
using System.Threading;
using System.Collections.Generic;
using System.Linq;

namespace LanExchange.Presentation.WinForms
{
    internal class AppView : IAppView
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
            add { System.Windows.Forms.Application.ThreadException += value; }
            remove { System.Windows.Forms.Application.ThreadException -= value; }
        }

        public event EventHandler ThreadExit
        {
            add { System.Windows.Forms.Application.ThreadExit += value; }
            remove { System.Windows.Forms.Application.ThreadExit -= value; }
        }

        public void Run(IWindow mainView)
        {
            Contract.Requires<ArgumentNullException>(mainView != null);
            System.Windows.Forms.Application.Run((Form)mainView);
        }

        public void InitVisualStyles()
        {
            System.Windows.Forms.Application.EnableVisualStyles();
            // must be called before first form created
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
        }

        public IEnumerable<IWindow> GetOpenWindows()
        {
            return System.Windows.Forms.Application.OpenForms.Cast<IWindow>();
        }

        public void Exit()
        {
            System.Windows.Forms.Application.Exit();
        }
    }
}