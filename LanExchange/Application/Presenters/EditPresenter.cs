using System;
using System.Linq;
using LanExchange.Presentation.Interfaces;

namespace LanExchange.Application.Presenters
{
    internal sealed class EditPresenter : PresenterBase<IEditView>, IEditPresenter
    {
        private readonly IPanelColumnManager panelColumns;

        public EditPresenter(IPanelColumnManager panelColumns)
        {
            if (panelColumns == null) throw new ArgumentNullException(nameof(panelColumns));

            this.panelColumns = panelColumns;
        }

        public void SetDataType(string typeName)
        {
            var columns = panelColumns.GetColumns(typeName);
            var columnsForView = columns.Where(header => !header.Refreshable).ToList();
            // TODO hide model
            // View.SetColumns(columnsForView);
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
