using LanExchange.Model;

namespace LanExchange.Interface
{
    public interface IFilterView
    {
        // properties
        bool IsVisible { get; set; }
        // methods
        IPresenter GetPresenter();
        void UpdateFromModel(IFilterModel model);
        void DoFilterCountChanged();
        //void FocusAndKeyPress(KeyPressEventArgs e);
        void FocusMe();
        void SetFilterText(string value);
    }
}
