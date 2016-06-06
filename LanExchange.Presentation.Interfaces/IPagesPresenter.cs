using System;

namespace LanExchange.Presentation.Interfaces
{
    public interface IPagesPresenter : IPresenter<IPagesView>, IPerformEscape
    {
        event EventHandler PanelViewFocusedItemChanged;
        event EventHandler PanelViewFilterTextChanged;

        int Count { get; }

        void CommandCloseTab();

        int SelectedIndex { get; set; }

        [Obsolete("Need to be removed.")]
        IPanelView ActivePanelView { get; }

        void SaveInstant();

        string GetTabName(int index);

        void SetupPanelViewEvents(IPanelView panelView);

        void LoadSettings();

        bool CanSendToNewTab();

        bool CanPasteItems();

        void CommandDeleteItems();

        void CommandPasteItems();

        void CommandSendToNewTab();

        void DoPanelViewFocusedItemChanged(object sender, System.EventArgs e);
        void DoPanelViewFilterTextChanged(object sender, System.EventArgs e);

        bool SelectTabByName(string tabName);

        void CommanCloseOtherTabs();

        //TODO hide model
        //int IndexOf(IPanelModel model);

        void UpdateTabName(int index);

        void CommandReRead();

        //TODO hide model
        //void SetTabImageForModel(IPanelModel theModel, string imageName);
        void DoPagesReRead();
        void DoPagesCloseTab();
        void DoPagesCloseOther();
    }
}
