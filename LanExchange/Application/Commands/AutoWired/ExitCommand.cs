using LanExchange.Application.Attributes;
using LanExchange.Presentation.Interfaces;
using System;

namespace LanExchange.Application.Commands.AutoWired
{
    [AutoWired]
    internal sealed class ExitCommand : CommandBase
    {
        private readonly IAppView appView;

        public ExitCommand(IAppView appView)
        {
            this.appView = appView ?? throw new ArgumentNullException(nameof(appView));
        }

        protected override void InternalExecute()
        {
            appView.Exit();
        }
    }
}
