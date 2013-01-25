using System;
using System.Collections.Generic;

namespace LanExchange.Interface
{
    public interface ITabParamsView : IDisposable
    {
        // properties
        bool SelectedChecked { get; set; }
        bool DontScanChecked { get; set; }
        string Text { get; set; }
        IEnumerable<string> Groups { get; }
        // methods
        IPresenter GetPresenter();
        void PrepareForm();
        bool ShowModal();
        void ClearGroups();
        void AddGroup(string value);
    }
}
