using System.Windows.Forms;
using LanExchange.Model;

namespace LanExchange.View
{
    public interface IPagesView
    {
        // properties
        int SelectedIndex { get; set; }
        string SelectedTabText { get; set; }
        int TabPagesCount { get; }
        int PopupSelectedIndex { get; }
        // methods
        void NewTabFromItemList(PanelItemList info);
        void RemoveTabAt(int index);
        void AddControl(int index, Control control);
        string Ellipsis(string text, int length);
        void SetTabToolTip(int index, string value);
        void FocusPanelView();
    }
}
