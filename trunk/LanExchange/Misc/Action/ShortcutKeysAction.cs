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
            var info = App.Ioc.Resolve<IPanelModel>();
            info.TabName = Resources.ShortcutKeys;
            presenter.AddTab(info);
            presenter.SelectedIndex = presenter.Count - 1;
        }

        private void SetupPanelItemList(PanelItemList list)
        {
            var item = new CustomPanelItem(null, "F1");
            item.SetCountColumns(2);
            item[1] = Resources.F1Help;
            list.Items.Add(item);
        }
    }
}
