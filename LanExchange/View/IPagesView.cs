using System;
using System.Windows.Forms;
using LanExchange.Model;

namespace LanExchange.View
{
    public interface IPagesView
    {
        // properties
        string Name { get; set; }
        int SelectedIndex { get; set; }
        string SelectedTabText { get; set; }
        int TabPagesCount { get; }
        // methods
        void NewTabFromItemList(PanelItemList Info);
        void RemoveTabAt(int Index);
        void AddControl(int Index, Control control);
        string Ellipsis(string text, int length);
        void SetTabToolTip(int Index, string value);
        void FocusPanelView();
    }
}
