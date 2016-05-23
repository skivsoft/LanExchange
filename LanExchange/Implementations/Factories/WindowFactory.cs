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

        public WindowFactory(IAboutPresenter aboutPresenter)
        {
            Contract.Requires<ArgumentNullException>(aboutPresenter != null);

            this.aboutPresenter = aboutPresenter;
        }

        public IWindow CreateAboutView()
        {
            return new AboutForm(aboutPresenter);
        }
    }
}