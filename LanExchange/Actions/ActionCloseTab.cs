using LanExchange.SDK;

namespace LanExchange.Actions
{
    class ActionCloseTab : IAction
    {
        public void Execute()
        {
            if (!Enabled) return;
            App.MainPages.CommandCloseTab();
            if (App.MainPages.Count == 0)
                App.MainView.ClearInfoPanel();
        }

        public bool Enabled
        {
            get { return App.MainPages.SelectedIndex != -1; }
        }
    }
}
