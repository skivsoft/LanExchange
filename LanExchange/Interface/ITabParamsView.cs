using System;
using System.Collections.Generic;

namespace LanExchange.Interface
{
    public interface ITabParamsView : IDisposable
    {
        // events
        event EventHandler OKClicked;
        event EventHandler Closed;
        // properties
        bool SelectedChecked { get; set; }
        bool DontScanChecked { get; set; }
        string Text { get; set; }
        IEnumerable<string> Groups { get; }
        int DomainsCount { get; }
        string DomainsFocusedText { get; set; }
        // methods
        IList<string> GetCheckedList();
        bool ShowModal();
        void DomainsClear();
        void DomainsAdd(string value, bool checkedItem);
    }
}
