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
            if (windowFactory != null) throw new ArgumentNullException(nameof(windowFactory));

            this.windowFactory = windowFactory;
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