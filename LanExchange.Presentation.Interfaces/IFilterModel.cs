namespace LanExchange.Presentation.Interfaces
{
    /// <summary>
    /// Interface for filtering panel items.
    /// </summary>
    public interface IFilterModel
    {
        /// <summary>
        /// Gets or sets the filter text.
        /// </summary>
        /// <value>
        /// The filter text.
        /// </value>
        string FilterText { get; set; }

        /// <summary>
        /// Gets the filter count.
        /// </summary>
        /// <value>
        /// The filter count.
        /// </value>
        int FilterCount { get; }

        /// <summary>
        /// Gets a value indicating whether this instance has back item.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has back item; otherwise, <c>false</c>.
        /// </value>
        bool HasBackItem { get; }

        /// <summary>
        /// Applies the filter.
        /// </summary>
        void ApplyFilter();
    }
}
