using LanExchange.Intf;
using LanExchange.SDK;

namespace LanExchange.Presenter.Action
{
    class CloseOtherAction : IAction
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
