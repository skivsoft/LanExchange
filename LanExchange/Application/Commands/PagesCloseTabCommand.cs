using LanExchange.Presentation.Interfaces;

namespace LanExchange.Application.Commands
{
    internal sealed class PagesCloseTabCommand : PagesCommandBase
    {
        public PagesCloseTabCommand(IPagesPresenter pagesPresenter) : base(pagesPresenter)
        {
        }

        protected override void InternalExecute()
        {
            pagesPresenter.CommandCloseTab();
        }
    }
}