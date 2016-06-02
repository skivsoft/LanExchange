using LanExchange.Presentation.Interfaces;

namespace LanExchange.Application.Commands
{
    internal sealed class PagesReReadCommand : PagesCommandBase
    {
        public PagesReReadCommand(IPagesPresenter pagesPresenter) : base(pagesPresenter)
        {
        }

        public override void Execute()
        {
            pagesPresenter.CommandReRead();
        }
    }
}