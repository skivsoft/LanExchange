using LanExchange.Presentation.Interfaces;

namespace LanExchange.Application.Commands
{
    internal sealed class PagesCloseOtherCommand : PagesCommandBase
    {
        public PagesCloseOtherCommand(IPagesPresenter pagesPresenter) : base(pagesPresenter)
        {
        }

        protected override void InternalExecute()
        {
            pagesPresenter.CommanCloseOtherTabs();
        }

        public override bool Enabled
        {
            get { return pagesPresenter.Count > 1; }
        }
    }
}