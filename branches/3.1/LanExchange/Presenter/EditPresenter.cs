using System;
using System.Collections.Generic;
using System.Text;
using LanExchange.SDK;

namespace LanExchange.Presenter
{
    public sealed class EditPresenter : PresenterBase<IEditView>, IEditPresenter
    {
        public void SetDataType(string typeName)
        {
            var columns = App.PanelColumns.GetColumns(typeName);
            var columnsForView = new List<PanelColumnHeader>();
            foreach(var header in columns)
                if (!header.Refreshable)
                    columnsForView.Add(header);
            View.SetColumns(columnsForView);
        }
    }
}
