using LanExchange.Presentation.Interfaces;

namespace LanExchange.Application.Commands.AutoWired
{
    internal sealed class PagesReReadCommand : PagesCommandBase
    {
        public PagesReReadCommand(IPagesPresenter pagesPresenter) : base(pagesPresenter)
        {
        }

        protected override void InternalExecute()
        {
            pagesPresenter.CommandReRead();
        }
    }
}