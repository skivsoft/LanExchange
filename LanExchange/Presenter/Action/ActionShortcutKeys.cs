using LanExchange.Intf;
using LanExchange.Properties;
using LanExchange.SDK;

namespace LanExchange.Presenter.Action
{
    class ActionShortcutKeys : IAction
    {
        public static int FindPanelIndex()
        {
            var presenter = App.MainPages;
            for (int index = 0; index < presenter.Count; index++)
            {
                var model = presenter.GetItem(index);
                if (model.DataType.Equals(typeof(ShortcutPanelItem).Name))
                    return index;
            }
            return -1;
        }

        public void Execute()
        {
            var presenter = App.MainPages;
            var foundIndex = FindPanelIndex();
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