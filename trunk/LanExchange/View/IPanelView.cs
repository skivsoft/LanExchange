using System;
using System.Collections.Generic;
using System.Text;
using LanExchange.Model;

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
        int FocusedItemIndex { get; }
        // methods
        void SelectItem(int Index);
        void SetVirtualListSize(int count);
        void RedrawFocusedItem();
    }
}
