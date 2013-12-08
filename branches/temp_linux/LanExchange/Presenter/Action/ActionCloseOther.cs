using LanExchange.SDK;

namespace LanExchange.Presenter.Action
{
    class ActionCloseOther : IAction
    {
        public void Execute()
        {
            if (Enabled)
                App.MainPages.CommanCloseOtherTabs();
        }

        public bool Enabled
        {
            get { return App.MainPages.Count > 1; }
        }
    }
}
