namespace WMIViewer
{
    /// <summary>
    /// Simple description of remote computer for WMI plugin.
    /// </summary>
    public interface IWmiComputer
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        string Name { get; set; }
        /// <summary>
        /// Gets or sets the comment.
        /// </summary>
        /// <value>
        /// The comment.
        /// </value>
        string Comment { get; set; }
    }
}
