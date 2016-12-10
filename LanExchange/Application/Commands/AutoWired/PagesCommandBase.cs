using System;
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
            this.pagesPresenter = pagesPresenter ?? throw new ArgumentNullException(nameof(pagesPresenter));
        }

        public override bool Enabled
        {
            get { return pagesPresenter.SelectedIndex != -1; }
        }
    }
}
