using LanExchange.Intf;
using LanExchange.SDK;

namespace LanExchange.Presenter.Action
{
    class CloseTabAction : IAction
    {
        public void Execute()
        {
            if (Enabled)
                App.MainPages.CommandCloseTab();
        }

        public bool Enabled
        {
            get { return App.MainPages.SelectedIndex != -1; }
        }
    }
}
