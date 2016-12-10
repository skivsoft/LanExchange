using System;
using LanExchange.Presentation.Interfaces;
using LanExchange.Application.Attributes;

namespace LanExchange.Application.Commands.AutoWired
{
    [AutoWired]
    internal sealed class AboutCommand : CommandBase
    {
        private readonly IWindowFactory windowFactory;

        public AboutCommand(IWindowFactory windowFactory)
        {
            this.windowFactory = windowFactory ?? throw new ArgumentNullException(nameof(windowFactory));
        }

        protected override void InternalExecute()
        {
            using (var window = windowFactory.CreateAboutView())
            {
                window.ShowModalDialog();
            }
        }
    }
}