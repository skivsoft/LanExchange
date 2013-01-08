using System;
using LanExchange.Presenter;

namespace LanExchange.View
{
    public interface IFilterView
    {
        // properties
        string FilterText { get; set; }
        bool Visible { get; set; }
        // methods
        FilterPresenter GetPresenter();
        void SetIsFound(bool value);
        void FocusMe();
        void SendKeysCorrect(string Keys);
        void DoFilterCountChanged();
    }
}
