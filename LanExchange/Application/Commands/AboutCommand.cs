using System;
using System.Diagnostics.Contracts;
using LanExchange.Presentation.Interfaces;

namespace LanExchange.Application.Commands
{
    internal sealed class AboutCommand : CommandBase
    {
        private readonly IWindowFactory windowFactory;

        public AboutCommand(IWindowFactory windowFactory)
        {
            Contract.Requires<ArgumentNullException>(windowFactory != null);

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