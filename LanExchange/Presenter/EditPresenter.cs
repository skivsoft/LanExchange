using System.Linq;
using LanExchange.Ioc;
using LanExchange.SDK;

namespace LanExchange.Presenter
{
    public sealed class EditPresenter : PresenterBase<IEditView>, IEditPresenter
    {
        public void SetDataType(string typeName)
        {
            var columns = App.PanelColumns.GetColumns(typeName);
            var columnsForView = columns.Where(header => !header.Refreshable).ToList();
            View.SetColumns(columnsForView);
        }
    }
}
