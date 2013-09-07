using System;

namespace LanExchange.Intf
{
    public interface IPagesPresenter : IPresenter<IPagesView>
    {
        event EventHandler PanelViewFocusedItemChanged;
        event EventHandler PanelViewFilterTextChanged;

        bool CanCloseTab();

        int Count { get; }

        void CommandNewTab();

        void CommandCloseTab();

        void CommandProperties();

        int SelectedIndex { get; set; }

        void SaveSettings();

        string GetTabName(int index);

        void SetupPanelViewEvents(IPanelView PV);

        void LoadSettings();

        IPanelModel GetItem(int index);

        bool CanSendToNewTab();

        bool CanPasteItems();

        void AddTab(IPanelModel info);

        void CommandDeleteItems();

        void CommandPasteItems();

        void CommandSendToNewTab();

        void DoPanelViewFocusedItemChanged(object sender, EventArgs e);
        void DoPanelViewFilterTextChanged(object sender, EventArgs e);
    }
}
