using System;
using LanExchange.Application.Attributes;
using LanExchange.Presentation.Interfaces;

namespace LanExchange.Application.Commands.AutoWired
{
    [AutoWired]
    internal sealed class AboutCommand : CommandBase
    {
        private readonly IWindowFactory windowFactory;

        /// <exception cref="ArgumentNullException"></exception>
        public AboutCommand(IWindowFactory windowFactory)
        {
            if (windowFactory == null) throw new ArgumentNullException(nameof(windowFactory));

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