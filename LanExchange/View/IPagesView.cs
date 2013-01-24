using System.Windows.Forms;
using LanExchange.Model;

namespace LanExchange.View
{
    public interface IPagesView
    {
        // properties
        string SelectedTabText { get; set; }
        int TabPagesCount { get; }
        int PopupSelectedIndex { get; }
        // methods
        void SetSelectedIndex(int value);
        int GetSelectedIndex();
        void NewTabFromItemList(PanelItemList info);
        void RemoveTabAt(int index);
        void AddControl(int index, Control control);
        string Ellipsis(string text, int length);
        void SetTabToolTip(int index, string value);
        void FocusPanelView();
        IPanelView GetActivePanelView();
    }
}
