using LanExchange.Intf;
using LanExchange.SDK;

namespace LanExchange.Presenter.Action
{
    class ActionReRead : IAction
    {
        public void Execute()
        {
            if (!Enabled) return;
            var pageModel = App.MainPages.GetItem(App.MainPages.SelectedIndex);
            // clear refreshable columns
            if (App.PanelColumns != null && pageModel.DataType != null)
                foreach (var column in App.PanelColumns.GetColumns(pageModel.DataType))
                    if (column.Callback != null && column.Refreshable)
                        column.LazyDict.Clear();
            pageModel.SyncRetrieveData();
        }

        public bool Enabled
        {
            get { return App.MainPages.SelectedIndex != -1; }
        }
    }
}
