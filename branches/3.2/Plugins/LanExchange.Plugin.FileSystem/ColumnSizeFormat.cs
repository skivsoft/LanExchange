namespace LanExchange.Plugin.FileSystem
{
    /// <summary>
    /// Size format.
    /// </summary>
    public enum ColumnSizeFormat
    {
        /// <summary>
        /// Size should be shown in bytes.
        /// </summary>
        Byte,
        /// <summary>
        /// Size should be shown in kilobytes.
        /// </summary>
        Kilobyte,
        /// <summary>
        /// Size should be shown in kibibytes.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Kibibyte")]
        Kibibyte  
    }
}