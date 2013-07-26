using LanExchange.SDK;
using LanExchange.UI;

namespace LanExchange.Action
{
    class AboutAction : IAction
    {
        public void Execute()
        {
            if (AboutForm.Instance != null)
            {
                AboutForm.Instance.Activate();
                return;
            }
            AboutForm.Instance = new AboutForm();
            AboutForm.Instance.Show();
        }
    }
}
