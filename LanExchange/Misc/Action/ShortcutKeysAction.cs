using LanExchange.Core;
using LanExchange.Model;
using LanExchange.Properties;
using LanExchange.SDK;

namespace LanExchange.Misc.Action
{
    class ShortcutKeysAction : IAction
    {
        public void Execute()
        {
            var presenter = App.MainPages;
            var info = App.Resolve<IPanelModel>();
            SetupPanelModel(info);
            presenter.AddTab(info);
            presenter.SelectedIndex = presenter.Count - 1;
        }

        private void SetupPanelModel(IPanelModel info)
        {
            info.TabName = Resources.ShortcutKeys;
            var item = new CustomPanelItem(null, "F1");
            item.SetCountColumns(2);
            item[1] = Resources.F1Help;
            info.Items.Add(item);
        }
    }
}
