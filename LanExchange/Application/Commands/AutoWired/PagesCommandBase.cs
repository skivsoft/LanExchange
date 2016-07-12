using System;
using System.Diagnostics.Contracts;
using LanExchange.Presentation.Interfaces;
using LanExchange.Application.Attributes;

namespace LanExchange.Application.Commands.AutoWired
{
    [AutoWired]
    internal abstract class PagesCommandBase : CommandBase
    {
        protected readonly IPagesPresenter pagesPresenter;

        protected PagesCommandBase(IPagesPresenter pagesPresenter)
        {
            Contract.Requires<ArgumentNullException>(pagesPresenter != null);

            this.pagesPresenter = pagesPresenter;
        }

        public override bool Enabled
        {
            get { return pagesPresenter.SelectedIndex != -1; }
        }
    }
}
