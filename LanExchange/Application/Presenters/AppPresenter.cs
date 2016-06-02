using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading;
using LanExchange.Interfaces.Services;
using LanExchange.Presentation.Interfaces;
using LanExchange.SDK;

namespace LanExchange.Application.Presenters
{
    internal sealed class AppPresenter : PresenterBase<IAppView>, IAppPresenter
    {
        private readonly IConfigPersistenceService configService;
        private readonly IPagesPresenter pagesPresenter;
        private readonly ITranslationService translationService;
        private readonly IDisposableManager disposableManager;

        public AppPresenter(
            IConfigPersistenceService configService,
            IPagesPresenter pagesPresenter,
            ITranslationService translationService,
            IDisposableManager disposableManager)
        {
            Contract.Requires<ArgumentNullException>(configService != null);
            Contract.Requires<ArgumentNullException>(pagesPresenter != null);
            Contract.Requires<ArgumentNullException>(translationService != null);
            Contract.Requires<ArgumentNullException>(disposableManager != null);

            this.configService = configService;
            this.pagesPresenter = pagesPresenter;
            this.translationService = translationService;
            this.disposableManager = disposableManager;
        }

        protected override void InitializePresenter()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
            View.ThreadException += ApplicationOnThreadException;
            View.ThreadExit += ApplicationOnThreadExit;

            View.InitVisualStyles();
        }

        private void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            View.Exit();
        }

        private void ApplicationOnThreadException(object sender, ThreadExceptionEventArgs e)
        {
            #if DEBUG
            Debug.Fail(e.Exception.Message + Environment.NewLine + e.Exception.StackTrace);
            #else
            View.Exit();
            #endif
        }

        private void ApplicationOnThreadExit(object sender, EventArgs e)
        {
            pagesPresenter.SaveInstant();
            configService.Save(App.Config);
            // dispose instances registered in plugins
            disposableManager.Dispose();
        }

        public void TranslateOpenForms()
        {
            // enum opened forms
            foreach (var form in View.GetOpenWindows().Cast<IWindowTranslationable>())
            {
                // set rtl
                var rtlChanged = translationService.RightToLeft != form.RightToLeft;
                if (rtlChanged) form.Hide();
                form.RightToLeft = translationService.RightToLeft;
                form.TranslateUI();
                if (rtlChanged) form.Show();
            }
        }
    }
}