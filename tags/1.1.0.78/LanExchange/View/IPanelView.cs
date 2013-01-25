using System.Collections.Generic;
using LanExchange.Presenter;

namespace LanExchange.View
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
        PanelPresenter GetPresenter();
    }
}
