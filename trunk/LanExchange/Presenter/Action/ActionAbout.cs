using System.Windows.Forms;
using LanExchange.Intf;
using LanExchange.SDK;

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
                m_Instance.FormClosed += OnFormClosed;
                m_Instance.Show();
            } else
                m_Instance.Activate();
        }

        public bool Enabled
        {
            get { return true; }
        }

        private void OnFormClosed(object sender, FormClosedEventArgs formClosedEventArgs)
        {
            m_Instance = null;
        }
    }
}
