using LanExchange.Presentation.Interfaces;

namespace LanExchange.Application.Commands.AutoWired
{
    internal sealed class PagesCloseOtherCommand : PagesCommandBase
    {
        public PagesCloseOtherCommand(IPagesPresenter pagesPresenter) : base(pagesPresenter)
        {
        }

        public override bool Enabled
        {
            get { return PagesPresenter.Count > 1; }
        }

        protected override void InternalExecute()
        {
            PagesPresenter.CommanCloseOtherTabs();
        }
    }
}