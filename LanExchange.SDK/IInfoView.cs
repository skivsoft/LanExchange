namespace LanExchange.SDK
{
    /// <summary>
    /// View of top info panel.
    /// </summary>
    public interface IInfoView
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="IInfoView"/> is visible.
        /// </summary>
        /// <value>
        ///   <c>true</c> if visible; otherwise, <c>false</c>.
        /// </value>
        bool Visible { get; set; }
    }
}
