using System;
using LanExchange.Intf;
using LanExchange.SDK;
using LanExchange.SDK.UI;

namespace LanExchange.Presenter.Action
{
    class ActionAbout : IAction
    {
        private IAboutView m_Instance;

        public void Execute()
        {
            if (m_Instance == null)
            {
                m_Instance = App.Resolve<IAboutView>();
                m_Instance.ViewClosed += OnViewClosed;
                m_Instance.Show();
            } else
                m_Instance.Activate();
        }

        public bool Enabled
        {
            get { return true; }
        }

        private void OnViewClosed(object sender, EventArgs e)
        {
            m_Instance = null;
        }
    }
}
