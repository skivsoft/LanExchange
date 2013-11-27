using System;
using System.Collections.Generic;

namespace LanExchange.SDK
{
    /// <summary>
    /// View of LanExchange panel.
    /// </summary>
    public interface IPanelView : IView
    {
        event EventHandler FocusedItemChanged;
        event EventHandler FilterTextChanged;

        /// <summary>
        /// Gets the filter.
        /// </summary>
        /// <value>
        /// The filter.
        /// </value>
        IFilterView Filter { get; }
        /// <summary>
        /// Gets the selected indexes.
        /// </summary>
        /// <value>
        /// The selected indexes.
        /// </value>
        IEnumerable<int> SelectedIndexes { get; }
        /// <summary>
        /// Gets the focused item text.
        /// </summary>
        /// <value>
        /// The focused item text.
        /// </value>
        string FocusedItemText { get; }
        /// <summary>
        /// Gets or sets the index of the focused item.
        /// </summary>
        /// <value>
        /// The index of the focused item.
        /// </value>
        int FocusedItemIndex { get; set; }
        /// <summary>
        /// Gets the presenter.
        /// </summary>
        /// <value>
        /// The presenter.
        /// </value>
        IPanelPresenter Presenter { get; }
        /// <summary>
        /// Selects the item.
        /// </summary>
        /// <param name="index">The index.</param>
        void SelectItem(int index);
        /// <summary>
        /// Sets the size of the virtual list.
        /// </summary>
        /// <param name="count">The count.</param>
        void SetVirtualListSize(int count);
        /// <summary>
        /// Redraws the focused item.
        /// </summary>
        void RedrawFocusedItem();
        /// <summary>
        /// Focuses the list view.
        /// </summary>
        void FocusListView();
        /// <summary>
        /// Clears the selected.
        /// </summary>
        void ClearSelected();
        /// <summary>
        /// Shows the run CMD error.
        /// </summary>
        /// <param name="cmdLine">The CMD line.</param>
        void ShowRunCmdError(string cmdLine);

        void ColumnsClear();
        void AddColumn(PanelColumnHeader header);
        PanelViewMode ViewMode { get; set; }
        PanelItemBase FocusedItem { get; }
        bool GridLines { get; set; }

        void RedrawItem(int index);

        void SetColumnMarker(int columnIndex, PanelSortOrder sortOrder);

        void ShowHeaderMenu(IList<PanelColumnHeader> columns);
    }
}