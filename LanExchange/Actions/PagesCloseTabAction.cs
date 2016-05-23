using LanExchange.SDK;

namespace LanExchange.Actions
{
    internal sealed class PagesCloseTabAction : PagesActionBase
    {
        public PagesCloseTabAction(IPagesPresenter pagesPresenter) : base(pagesPresenter)
        {
        }

        public override void Execute()
        {
            pagesPresenter.CommandCloseTab();
        }
    }
}