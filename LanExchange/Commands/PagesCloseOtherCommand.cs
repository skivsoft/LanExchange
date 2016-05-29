using LanExchange.Presentation.Interfaces;

namespace LanExchange.Commands
{
    internal sealed class PagesCloseOtherCommand : PagesCommandBase
    {
        public PagesCloseOtherCommand(IPagesPresenter pagesPresenter) : base(pagesPresenter)
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