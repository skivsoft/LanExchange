using System;

namespace LanExchange.View
{
    public interface IFilterView
    {
        string FilterText { get; set; }
        void InitFilterText(string value);
        void SetIsFound(bool value);
    }
}
