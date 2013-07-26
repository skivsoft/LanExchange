namespace LanExchange.SDK
{
    /// <summary>
    /// Interface for those objects who wants to sort by a column.
    /// </summary>
    public interface IColumnComparable
    {
        /// <summary>
        /// Compares to other object by <cref>column</cref>.
        /// </summary>
        /// <param name="other">The other object.</param>
        /// <param name="column">The column.</param>
        /// <returns></returns>
        int CompareTo(object other, int column);     
    }
}
