using System;
using System.Drawing;

namespace LanExchange.SDK
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
    }
}
