namespace LanExchange.Sdk
{
    /// <summary>
    /// Column header interface returns by <cref>PanelItemBase</cref>.
    /// </summary>
    public interface IPanelColumnHeader
    {
        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        string Text { get; set; }
        /// <summary>
        /// Sets the visible.
        /// </summary>
        /// <param name="value">if set to <c>true</c> [value].</param>
        void SetVisible(bool value);
    }
}
