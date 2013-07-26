using LanExchange.SDK;
using LanExchange.UI;

namespace LanExchange.Action
{
    class SettingsAction : IAction
    {
        public void Execute()
        {
            if (SettingsForm.Instance != null)
            {
                SettingsForm.Instance.Activate();
                return;
            }
            SettingsForm.Instance = new SettingsForm();
            SettingsForm.Instance.Show();
        }
    }
}
