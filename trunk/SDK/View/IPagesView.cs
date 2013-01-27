using LanExchange.Sdk.Model;

namespace LanExchange.Sdk.View
{
    public interface IPagesView
    {
        // properties
        string SelectedTabText { get; set; }
        int TabPagesCount { get; }
        int PopupSelectedIndex { get; }
        int SelectedIndex { get; set; }
        IPanelView ActivePanelView { get; }
        // methods
        void RemoveTabAt(int index);
        void SetTabToolTip(int index, string value);
        void FocusPanelView();
        ITabParamsView CreateTabParamsView();
        IPanelView CreatePanelView(IPanelModel info);
    }
}
