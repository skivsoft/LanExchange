﻿using LanExchange.Presentation.Interfaces;
using LanExchange.SDK;

namespace LanExchange.Actions
{
    internal sealed class PagesCloseOtherAction : PagesActionBase
    {
        public PagesCloseOtherAction(IPagesPresenter pagesPresenter) : base(pagesPresenter)
        {
        }

        public override void Execute()
        {
            pagesPresenter.CommanCloseOtherTabs();
        }

        public override bool Enabled
        {
            get { return pagesPresenter.Count > 1; }
        }
    }
}