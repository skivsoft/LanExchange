using System.Collections.Generic;

namespace LanExchange.Interface
{
    /// <summary>
    /// Interface for Panel component.
    /// </summary>
    public interface IPanelView
    {
        // properties
        IFilterView Filter { get; }
        IEnumerable<int> SelectedIndices { get; }
        string FocusedItemText { get; }
        int FocusedItemIndex { get; set; }
        // methods
        void SelectItem(int index);
        void SetVirtualListSize(int count);
        void RedrawFocusedItem();
        void FocusListView();
        void ClearSelected();
        void SetClipboardText(string value);
        IPresenter GetPresenter();
        void ShowRunCmdError(string cmdLine);
    }
}
