using LanExchange.SDK.Factories;
using System;
using LanExchange.SDK;
using LanExchange.Plugin.WinForms.Forms;
using System.Diagnostics.Contracts;

namespace LanExchange.Implementations.Factories
{
    internal sealed class WindowFactory : IWindowFactory
    {
        private readonly IAboutPresenter aboutPresenter;
        private readonly ITranslationService translationService;

        public WindowFactory(
            IAboutPresenter aboutPresenter,
            ITranslationService translationService)
        {
            Contract.Requires<ArgumentNullException>(aboutPresenter != null);
            Contract.Requires<ArgumentNullException>(translationService != null);

            this.aboutPresenter = aboutPresenter;
            this.translationService = translationService;
        }

        public IWindow CreateAboutView()
        {
            return new AboutForm(aboutPresenter, translationService);
        }
    }
}