using System;
using System.Diagnostics.Contracts;
using System.Linq;
using LanExchange.Presentation.Interfaces;
using LanExchange.SDK;

namespace LanExchange.Application.Presenters
{
    internal sealed class EditPresenter : PresenterBase<IEditView>, IEditPresenter
    {
        private readonly IPanelColumnManager panelColumns;

        public EditPresenter(IPanelColumnManager panelColumns)
        {
            Contract.Requires<ArgumentNullException>(panelColumns != null);

            this.panelColumns = panelColumns;
        }

        public void SetDataType(string typeName)
        {
            var columns = panelColumns.GetColumns(typeName);
            var columnsForView = columns.Where(header => !header.Refreshable).ToList();
            //TODO hide model
            //View.SetColumns(columnsForView);
        }

        public void PerformOk()
        {
            View.Close();
        }

        public void PerformCancel()
        {
            View.Close();
        }

        protected override void InitializePresenter()
        {
        }
    }
}
