namespace LanExchange.Sdk
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
        /// <summary>
        /// Gets the focused panel item.
        /// </summary>
        /// <param name="bPingAndAsk">if set to <c>true</c> [b ping and ask].</param>
        /// <param name="bCanReturnParent">if set to <c>true</c> [b can return parent].</param>
        /// <returns></returns>
        PanelItemBase GetFocusedPanelItem(bool bPingAndAsk, bool bCanReturnParent);
        // TODO must be undepended from AbstractPanelItem's descendant
        /// <summary>
        /// Gets the focused computer.
        /// </summary>
        /// <param name="p">if set to <c>true</c> [p].</param>
        /// <returns></returns>
        PanelItemBase GetFocusedComputer(bool p);
    }
}
