using System.Windows.Forms;
using LanExchange.Presenter;
using LanExchange.Model;

namespace LanExchange.View
{
    public interface IFilterView
    {
        // properties
        bool IsVisible { get; set; }
        // methods
        FilterPresenter GetPresenter();
        void UpdateFromModel(IFilterModel model);
        void DoFilterCountChanged();
        void FocusAndKeyPress(KeyPressEventArgs e);
        void FocusMe();
        void SetFilterText(string value);
    }
}
