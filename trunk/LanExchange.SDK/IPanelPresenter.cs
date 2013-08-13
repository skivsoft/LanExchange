using System;

namespace LanExchange.SDK
{
    /// <summary>
    /// Presenter of LanExchange panel.
    /// </summary>
    public interface IPanelPresenter
    {
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
        /// <param name="pingAndAsk">if set to <c>true</c> [b ping and ask].</param>
        /// <param name="canReturnParent">if set to <c>true</c> [b can return parent].</param>
        /// <returns></returns>
        PanelItemBase GetFocusedPanelItem(bool pingAndAsk, bool canReturnParent);
        /// <summary>
        /// Gets the focused computer.
        /// </summary>
        /// <param name="pingAndAsk">if set to <c>true</c> [p].</param>
        /// <returns></returns>
        [Obsolete("Bad idea to use this computer-specific method.")]
        PanelItemBase GetFocusedComputer(bool pingAndAsk);

        void ResetSortOrder();
    }
}
