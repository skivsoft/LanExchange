using System;
using System.Drawing;

namespace LanExchange.Sdk
{
    /// <summary>
    /// View for AboutForm.
    /// </summary>
    public interface IAboutView
    {
        /// <summary>
        /// Gets or sets the web text.
        /// </summary>
        /// <value>
        /// The web text.
        /// </value>
        string WebText { get; set; }
        /// <summary>
        /// Gets or sets the email text.
        /// </summary>
        /// <value>
        /// The email text.
        /// </value>
        string EmailText { get; set; }

        /// <summary>
        /// Shows the message.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="color">The color.</param>
        void ShowMessage(string text, Color color);
        /// <summary>
        /// Shows the progress bar.
        /// </summary>
        void ShowProgressBar();
        /// <summary>
        /// Cancels the view.
        /// </summary>
        void CancelView();
        /// <summary>
        /// Shows the update button.
        /// </summary>
        /// <param name="version">The version.</param>
        void ShowUpdateButton(Version version);
    }
}
