using LanExchange.Presenter;

namespace LanExchange.View
{
    public interface IFilterView
    {
        // properties
        bool Visible { get; set; }
        // methods
        FilterPresenter GetPresenter();
        void SetIsFound(bool value);
        void FocusMe();
        void SendKeysCorrect(string keys);
        void DoFilterCountChanged();
        void SetFilterText(string value);
        string GetFilterText();
    }
}
