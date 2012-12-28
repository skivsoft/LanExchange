using System;
using System.Windows.Forms;

namespace LanExchange.View
{
    public interface ITabControlView
    {
        // properties
        string Name { get; set; }
        int SelectedIndex { get; set; }
        string SelectedTabText { get; set; }
        int TabPagesCount { get; }
        // methods
        void NewTab(string tabname);
        void RemoveTabAt(int Index);
        void AddControl(int Index, Control control);
        string Ellipsis(string text, int length);
    }
}
