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

                // clear refreshable columns
                if (AppPresenter.PanelColumns != null && pageModel.DataType != null)
                    foreach (var column in AppPresenter.PanelColumns.GetColumns(pageModel.DataType))
                        if (column.Callback != null && column.Refreshable)
                            column.LazyDict.Clear();
                
                pageModel.SyncRetrieveData();
            }
            MainForm.Instance.popTop.Tag = null;
        }
    }
}
