using LanExchange.Model;
using LanExchange.Presenter;
using LanExchange.Properties;
using LanExchange.SDK;

namespace LanExchange.Action
{
    class ShortcutKeysAction : IAction
    {
        public void Execute()
        {
            var model = AppPresenter.MainPages.GetModel();
            var info = new PanelItemList(Resources.ShortcutKeys);
            model.AddTab(info);
            model.SelectedIndex = model.Count - 1;
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
