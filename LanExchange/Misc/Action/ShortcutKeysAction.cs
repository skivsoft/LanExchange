using System.ComponentModel;
using LanExchange.Intf;
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
            info.TabName = Resources.ShortcutKeys;
            SetupPanelModel(info);
            if (presenter.AddTab(info))
                App.MainPages.View.ActivePanelView.Presenter.UpdateItemsAndStatus();
            // !!! cycle is bad!
            for (int index = 0; index < presenter.Count; index++)
                if (presenter.GetItem(index).Equals(info))
                {
                    presenter.SelectedIndex = index;
                }
            //presenter.SelectedIndex = presenter.Count - 1;
        }

        [Localizable(false)]
        private void SetupPanelModel(IPanelModel info)
        {
            info.DataType = typeof (ShortcutPanelItem);
            info.CurrentPath.Push(ShortcutFiller.ROOT_OF_SHORTCUTS);
        }
    }
}