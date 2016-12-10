using LanExchange.Presentation.Interfaces;
using System;

namespace LanExchange.Application.Commands
{
    internal sealed class ToggleVisibleCommand : CommandBase
    {
        private readonly IMainView mainView;

        public ToggleVisibleCommand(IMainView mainView)
        {
            this.mainView = mainView ?? throw new ArgumentNullException(nameof(mainView));
        }

        protected override void InternalExecute()
        {
            mainView.Visible = !mainView.Visible;
            if (mainView.Visible)
                mainView.Activate();
        }
    }
}