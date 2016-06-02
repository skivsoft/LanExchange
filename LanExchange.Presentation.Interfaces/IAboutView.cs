namespace LanExchange.Presentation.Interfaces
{
    /// <summary>
    /// View for AboutForm.
    /// </summary>
    public interface IAboutView : IWindow, IModalDialog
    {
        /// <summary>
        /// Gets or sets the web text.
        /// </summary>
        /// <value>
        /// The web text.
        /// </value>
        string WebText { get; set; }

        string WebToolTip { get; set; }

        string VersionText { get; set; }

        string CopyrightText { get; set; }
        bool DetailsVisible { get; set; }

        void TranslateUI();
    }
}
