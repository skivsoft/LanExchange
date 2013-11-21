using System.ComponentModel;
using LanExchange.Intf;
using LanExchange.Properties;
using LanExchange.SDK;

namespace LanExchange.Presenter.Action
{
    class ShortcutKeysAction : IAction
    {

        public void Execute()
        {
            var presenter = App.MainPages;
            var info = App.Resolve<IPanelModel>();
            info.TabName = Resources.KeyF1_;
            SetupPanelModel(info);
            if (presenter.AddTab(info))
            {
                if (App.MainPages.View.ActivePanelView != null)
                    App.MainPages.View.ActivePanelView.Presenter.UpdateItemsAndStatus();
            }
            // !!! cycle is bad!
            for (int index = 0; index < presenter.Count; index++)
                if (presenter.GetItem(index).Equals(info))
                {
                    presenter.SelectedIndex = index;
                }
            //presenter.SelectedIndex = presenter.Count - 1;
        }

        public bool Enabled
        {
            get { return true; }
        }

        [Localizable(false)]
        private void SetupPanelModel(IPanelModel info)
        {
            info.DataType = typeof (ShortcutPanelItem).Name;
            info.CurrentPath.Push(ShortcutFiller.ROOT_OF_SHORTCUTS);
        }
    }
}