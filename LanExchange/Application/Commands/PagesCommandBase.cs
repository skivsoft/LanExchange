using System;
using System.Diagnostics.Contracts;
using LanExchange.Presentation.Interfaces;

namespace LanExchange.Application.Commands
{
    internal abstract class PagesCommandBase : ICommand
    {
        protected readonly IPagesPresenter pagesPresenter;

        protected PagesCommandBase(IPagesPresenter pagesPresenter)
        {
            Contract.Requires<ArgumentNullException>(pagesPresenter != null);

            this.pagesPresenter = pagesPresenter;
        }

        public abstract void Execute();

        public virtual bool Enabled
        {
            get { return pagesPresenter.SelectedIndex != -1; }
        }
    }
}
