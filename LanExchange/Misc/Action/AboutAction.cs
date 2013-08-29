using System.Windows.Forms;
using LanExchange.SDK;
using LanExchange.UI;

namespace LanExchange.Misc.Action
{
    class AboutAction : IAction
    {
        private IAboutView m_Instance;

        public void Execute()
        {
            if (m_Instance == null)
            {
                m_Instance = App.Ioc.Resolve<IAboutView>();
                m_Instance.FormClosed += OnFormClosed;
                m_Instance.Show();
            } else
                m_Instance.Activate();
        }

        private void OnFormClosed(object sender, FormClosedEventArgs formClosedEventArgs)
        {
            m_Instance = null;
        }
    }
}
