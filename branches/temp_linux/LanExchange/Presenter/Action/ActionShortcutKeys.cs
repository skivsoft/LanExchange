using LanExchange.Misc;
using LanExchange.Properties;
using LanExchange.SDK;
using LanExchange.SDK.Model;

namespace LanExchange.Presenter.Action
{
    class ActionShortcutKeys : IAction
    {
        public void Execute()
        {
            var presenter = App.MainPages;
            var foundIndex = App.Presenter.FindShortcutKeysPanelIndex();
            if (foundIndex == -1)
            {
                var model = App.Resolve<IPanelModel>();
                model.TabName = Resources.mHelpKeys_Text;
                model.DataType = typeof (ShortcutPanelItem).Name;
                model.CurrentPath.Push(ShortcutFiller.ROOT_OF_SHORTCUTS);
                presenter.AddTab(model);
            }
            else
                presenter.SelectedIndex = foundIndex;
        }

        public bool Enabled
        {
            get { return true; }
        }
    }
}