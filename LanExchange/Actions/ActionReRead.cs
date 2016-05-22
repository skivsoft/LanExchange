using LanExchange.SDK;
using System;
using System.Diagnostics.Contracts;

namespace LanExchange.Actions
{
    class ActionReRead : IAction
    {
        private readonly IPanelColumnManager panelColumns;

        public ActionReRead(IPanelColumnManager panelColumns)
        {
            Contract.Requires<ArgumentNullException>(panelColumns != null);

            this.panelColumns = panelColumns;
        }

        public void Execute()
        {
            if (!Enabled) return;
            var pageModel = App.MainPages.GetItem(App.MainPages.SelectedIndex);
            // clear refreshable columns
            if (pageModel.DataType != null)
                foreach (var column in panelColumns.GetColumns(pageModel.DataType))
                    if (column.Callback != null && column.Refreshable)
                        column.LazyDict.Clear();
            //var result = pageModel.RetrieveData(RetrieveMode.Sync, false);
            //pageModel.SetFillerResult(result, false);
            pageModel.AsyncRetrieveData(false);
        }

        public bool Enabled
        {
            get { return App.MainPages.SelectedIndex != -1; }
        }
    }
}
