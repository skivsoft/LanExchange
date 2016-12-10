using LanExchange.Presentation.Interfaces;
using System;

namespace LanExchange.Application.Commands
{
    internal sealed class ToggleVisibleCommand : CommandBase
    {
        private readonly IMainView mainView;

        public ToggleVisibleCommand(IMainView mainView)
        {
            if (mainView != null) throw new ArgumentNullException(nameof(mainView));

            this.mainView = mainView;
        }

        protected override void InternalExecute()
        {
            mainView.Visible = !mainView.Visible;
            if (mainView.Visible)
                mainView.Activate();
        }
    }
}