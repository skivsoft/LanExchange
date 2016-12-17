using System;
using LanExchange.Application.Attributes;
using LanExchange.Presentation.Interfaces;

namespace LanExchange.Application.Commands.AutoWired
{
    [AutoWired]
    internal abstract class PagesCommandBase : CommandBase
    {
        protected readonly IPagesPresenter PagesPresenter;

        protected PagesCommandBase(IPagesPresenter pagesPresenter)
        {
            if (pagesPresenter != null) throw new ArgumentNullException(nameof(pagesPresenter));

            PagesPresenter = pagesPresenter;
        }

        public override bool Enabled
        {
            get { return PagesPresenter.SelectedIndex != -1; }
        }
    }
}
