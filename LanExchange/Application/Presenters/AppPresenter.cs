using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using LanExchange.Presentation.Interfaces;

namespace LanExchange.Application.Presenters
{
    internal sealed class AppPresenter : PresenterBase<IAppView>, IAppPresenter
    {
        private readonly IPagesPresenter pagesPresenter;
        private readonly ITranslationService translationService;
        private readonly IDisposableManager disposableManager;

        public AppPresenter(
            IPagesPresenter pagesPresenter,
            ITranslationService translationService,
            IDisposableManager disposableManager)
        {
            Contract.Requires<ArgumentNullException>(pagesPresenter != null);
            Contract.Requires<ArgumentNullException>(translationService != null);
            Contract.Requires<ArgumentNullException>(disposableManager != null);

            this.pagesPresenter = pagesPresenter;
            this.translationService = translationService;
            this.disposableManager = disposableManager;
        }

        protected override void InitializePresenter()
        {
            View.SetExceptionHandlers();
            View.InitVisualStyles();
        }

        public void OnNonUIException(Exception exception)
        {
        }

        public void OnUIException(Exception exception)
        {
#if DEBUG
            Debug.Fail(exception.Message + Environment.NewLine + exception.StackTrace);
#else
            View.Exit();
#endif
        }

        public void OnExit()
        {
            pagesPresenter.SaveInstant();
            // dispose instances registered in plugins
            disposableManager.Dispose();
        }

        public void TranslateOpenForms()
        {
            // enum opened forms
            foreach (var form in View.GetOpenWindows().Cast<IWindowTranslationable>())
            {
                // set rtl
                var rtlChanged = translationService.RightToLeft != form.RightToLeftValue;
                if (rtlChanged) form.Visible = false;
                form.RightToLeftValue = translationService.RightToLeft;
                form.TranslateUI();
                if (rtlChanged) form.Visible = true;
            }
        }
    }
}