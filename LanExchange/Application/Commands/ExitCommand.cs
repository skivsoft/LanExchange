using LanExchange.Presentation.Interfaces;
using System;
using System.Diagnostics.Contracts;

namespace LanExchange.Application.Commands
{
    internal sealed class ExitCommand : CommandBase
    {
        private readonly IAppView appView;

        public ExitCommand(IAppView appView)
        {
            Contract.Requires<ArgumentNullException>(appView != null);

            this.appView = appView;
        }

        protected override void InternalExecute()
        {
            appView.Exit();
        }
    }
}
