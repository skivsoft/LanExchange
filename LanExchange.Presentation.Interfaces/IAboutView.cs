namespace LanExchange.SDK
{
    /// <summary>
    /// View for AboutForm.
    /// </summary>
    public interface IAboutView : IWindow
    {
        /// <summary>
        /// Gets or sets the web text.
        /// </summary>
        /// <value>
        /// The web text.
        /// </value>
        string WebText { get; set; }

        string WebToolTip { get; set; }
        string TwitterToolTip { get; set; }

        string VersionText { get; set; }

        string CopyrightText { get; set; }
    }
}
