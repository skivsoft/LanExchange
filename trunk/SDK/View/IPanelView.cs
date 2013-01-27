using System.Collections.Generic;

namespace LanExchange.Sdk.View
{
    /// <summary>
    /// Interface for Panel component.
    /// </summary>
    public interface IPanelView
    {
        // properties
        IFilterView Filter { get; }
        IEnumerable<int> SelectedIndexes { get; }
        string FocusedItemText { get; }
        int FocusedItemIndex { get; set; }
        IPresenter Presenter { get; }
        // methods
        void SelectItem(int index);
        void SetVirtualListSize(int count);
        void RedrawFocusedItem();
        void FocusListView();
        void ClearSelected();
        void SetClipboardText(string value);
        void ShowRunCmdError(string cmdLine);
    }
}
