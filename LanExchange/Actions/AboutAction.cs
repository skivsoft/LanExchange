using System;
using LanExchange.SDK;
using LanExchange.SDK.Factories;
using System.Diagnostics.Contracts;

namespace LanExchange.Actions
{
    internal sealed class AboutAction : IAction
    {
        private readonly IWindowFactory windowFactory;
        private IWindow aboutInstance;

        public AboutAction(IWindowFactory windowFactory)
        {
            Contract.Requires<ArgumentNullException>(windowFactory != null);

            this.windowFactory = windowFactory;
        }

        public void Execute()
        {
            if (aboutInstance == null)
            {
                aboutInstance = windowFactory.CreateAboutView();
                aboutInstance.ViewClosed += OnViewClosed;
                aboutInstance.Show();
            } else
                aboutInstance.Activate();
        }

        public bool Enabled
        {
            get { return true; }
        }

        private void OnViewClosed(object sender, EventArgs e)
        {
            aboutInstance = null;
        }
    }
}
