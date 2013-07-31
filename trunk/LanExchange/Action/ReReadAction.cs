using LanExchange.Presenter;
using LanExchange.SDK;
using LanExchange.UI;

namespace LanExchange.Action
{
    class ReReadAction : IAction
    {
        public void Execute()
        {
            var model = AppPresenter.MainPages.GetModel();
            if (model.SelectedIndex != -1)
            {
                var pageModel = model.GetItem(model.SelectedIndex);
                pageModel.SyncRetrieveData();
            }
        }
    }
}
