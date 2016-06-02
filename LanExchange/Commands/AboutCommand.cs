using System;
using System.Diagnostics.Contracts;
using LanExchange.Presentation.Interfaces.Factories;
using LanExchange.SDK;

namespace LanExchange.Commands
{
    internal sealed class AboutCommand : ICommand
    {
        private readonly IWindowFactory windowFactory;

        public AboutCommand(IWindowFactory windowFactory)
        {
            Contract.Requires<ArgumentNullException>(windowFactory != null);

            this.windowFactory = windowFactory;
        }

        public void Execute()
        {
            using (var window = windowFactory.CreateAboutView())
            {
                window.ShowModalDialog();
            }
        }

        public bool Enabled
        {
            get { return true; }
        }
    }
}
