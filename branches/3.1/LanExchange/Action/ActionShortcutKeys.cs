using LanExchange.SDK;

namespace LanExchange.Action
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
                var root = new ShortcutRoot();
                model.DataType = typeof (ShortcutPanelItem).Name;
                model.CurrentPath.Push(root);
                presenter.AddTab(model);
                foundIndex = presenter.Count - 1;
            }
            presenter.SelectedIndex = foundIndex;
        }

        public bool Enabled
        {
            get { return true; }
        }
    }
}