using System;
using LanExchange.SDK.Presentation.Interfaces;

namespace LanExchange.SDK
{
    /// <summary>
    /// Presenter of LanExchange panel.
    /// </summary>
    public interface IPanelPresenter : IPresenter<IPanelView>
    {
        event EventHandler CurrentPathChanged;

        /// <summary>
        /// Gets or sets the objects.
        /// </summary>
        /// <value>
        /// The objects.
        /// </value>
        IPanelModel Objects { get; set; }
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
        /// <summary>
        /// Gets the focused panel item.
        /// </summary>
        /// <param name="canReturnParent">if set to <c>true</c> [b can return parent].</param>
        /// <returns></returns>
        PanelItemBase GetFocusedPanelItem(bool canReturnParent);

        void ResetSortOrder();
        void ColumnClick(int index);
        bool ReorderColumns(int oldIndex, int newIndex);
        void ColumnWidthChanged(int index, int newWidth);
        void ColumnRightClick(int columnIndex);
        void ShowHideColumnClick(int columnIndex);
    }
}
