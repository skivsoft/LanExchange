namespace LanExchange.SDK
{
    /// <summary>
    /// LanExchange main TabControl View interface.
    /// </summary>
    public interface IPagesView
    {
        /// <summary>
        /// Gets or sets the selected tab text.
        /// </summary>
        /// <value>
        /// The selected tab text.
        /// </value>
        string SelectedTabText { get; set; }
        /// <summary>
        /// Gets the tab pages count.
        /// </summary>
        /// <value>
        /// The tab pages count.
        /// </value>
        int TabPagesCount { get; }
        /// <summary>
        /// Gets the index of the popup selected.
        /// </summary>
        /// <value>
        /// The index of the popup selected.
        /// </value>
        int PopupSelectedIndex { get; }
        /// <summary>
        /// Gets or sets the index of the selected.
        /// </summary>
        /// <value>
        /// The index of the selected.
        /// </value>
        int SelectedIndex { get; set; }
        /// <summary>
        /// Gets the active panel view.
        /// </summary>
        /// <value>
        /// The active panel view.
        /// </value>
        IPanelView ActivePanelView { get; }
        /// <summary>
        /// Removes the tab at.
        /// </summary>
        /// <param name="index">The index.</param>
        void RemoveTabAt(int index);
        /// <summary>
        /// Sets the tab tool tip.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="value">The value.</param>
        void SetTabToolTip(int index, string value);
        /// <summary>
        /// Focuses the panel view.
        /// </summary>
        void FocusPanelView();
        /// <summary>
        /// Creates the panel view.
        /// </summary>
        /// <param name="info">The info.</param>
        /// <returns></returns>
        IPanelView CreatePanelView(IPanelModel info);
    }
}
