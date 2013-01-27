using System;
using System.Collections.Generic;

namespace LanExchange.Sdk.View
{
    public interface ITabParamsView : IDisposable
    {
        // events
        event EventHandler OkClicked;
        event EventHandler Closed;
        // properties
        bool SelectedChecked { get; set; }
        bool DontScanChecked { get; set; }
        string Text { get; set; }
        IEnumerable<string> Groups { get; }
        int DomainsCount { get; }
        string DomainsFocusedText { get; set; }
        IList<string> CheckedList { get; }
        // methods
        bool ShowModal();
        void DomainsClear();
        void DomainsAdd(string value, bool checkedItem);
    }
}
