using System;

namespace LanExchange.Presentation.Interfaces
{
    /// <summary>
    /// Presenter of LanExchange panel.
    /// </summary>
    public interface IPanelPresenter : IPresenter<IPanelView>
    {
        event EventHandler CurrentPathChanged;

        // IPanelModel Objects { get; set; }

        /// <summary>
        /// Updates the items and status.
        /// </summary>
        void UpdateItemsAndStatus();

        /// <summary>
        /// Commands the level up.
        /// </summary>
        /// <returns></returns>
        bool CommandLevelUp();

        bool CommandLevelDown();

        // PanelItemBase GetFocusedPanelItem(bool canReturnParent);
        void ResetSortOrder();

        void ColumnClick(int index);

        bool ReorderColumns(int oldIndex, int newIndex);

        void ColumnWidthChanged(int index, int newWidth);

        void ColumnRightClick(int columnIndex);

        void ShowHideColumnClick(int columnIndex);
    }
}