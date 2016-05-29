using LanExchange.SDK;
using System;
using System.Diagnostics.Contracts;
using LanExchange.Presentation.Interfaces;

namespace LanExchange.Actions
{
    internal abstract class PagesActionBase : IAction
    {
        protected readonly IPagesPresenter pagesPresenter;

        protected PagesActionBase(IPagesPresenter pagesPresenter)
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
