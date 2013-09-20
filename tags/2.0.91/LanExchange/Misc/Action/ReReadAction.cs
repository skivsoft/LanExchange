using LanExchange.Intf;
using LanExchange.SDK;

namespace LanExchange.Misc.Action
{
    class ReReadAction : IAction
    {
        public void Execute()
        {
            var presenter = App.MainPages;
            if (presenter.SelectedIndex != -1)
            {
                var pageModel = presenter.GetItem(presenter.SelectedIndex);

                // clear refreshable columns
                if (App.PanelColumns != null && pageModel.DataType != null)
                    foreach (var column in App.PanelColumns.GetColumns(pageModel.DataType))
                        if (column.Callback != null && column.Refreshable)
                            column.LazyDict.Clear();
                
                pageModel.SyncRetrieveData();
            }
        }
    }
}
