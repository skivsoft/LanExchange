using LanExchange.Sdk.Model;

namespace LanExchange.Sdk.View
{
    public interface IFilterView
    {
        // properties
        bool IsVisible { get; set; }
        // methods
        IPresenter Presenter { get; }
        void UpdateFromModel(IFilterModel model);
        void DoFilterCountChanged();
        //void FocusAndKeyPress(KeyPressEventArgs e);
        void FocusMe();
        void SetFilterText(string value);
    }
}
