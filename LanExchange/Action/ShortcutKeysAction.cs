using LanExchange.Model;
using LanExchange.Presenter;
using LanExchange.SDK;

namespace LanExchange.Action
{
    class ShortcutKeysAction : IAction
    {
        public void Execute()
        {
            var model = AppPresenter.MainPages.GetModel();
            var info = new PanelItemList("Shortcut keys");
            model.AddTab(info);
            model.SelectedIndex = model.Count - 1;
        }

        private void SetupPanelItemList(PanelItemList list)
        {
            var item = new CustomPanelItem(null, "F1");
            item.SetCountColumns(2);
            item[1] = "This shortcut keys list.";
            list.Items.Add(item);
        }
    }
}
