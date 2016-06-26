using LanExchange.Presentation.Interfaces;
using System;
using System.Diagnostics.Contracts;

namespace LanExchange.Application.Commands
{
    internal sealed class ToggleVisibleCommand : CommandBase
    {
        private readonly IMainView mainView;

        public ToggleVisibleCommand(IMainView mainView)
        {
            Contract.Requires<ArgumentNullException>(mainView != null);

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