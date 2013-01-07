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
        IEnumerable<int> SelectedIndices { get; }
        string FocusedItemText { get; }
        int FocusedItemIndex { get; }
        bool FilterVisible { get; set; }
        string FilterText { get; set; }
        // methods
        void SelectItem(int Index);
        void SetIsFound(bool value);
        void SetVirtualListSize(int count);
        void RedrawFocusedItem();
    }
}
