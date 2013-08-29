using System;
using System.Windows.Forms;
using LanExchange.Core;

namespace LanExchange.UI
{
    /// <summary>
    /// View for AboutForm.
    /// </summary>
    public interface IAboutView : IView
    {
        event FormClosedEventHandler FormClosed;

        /// <summary>
        /// Gets or sets the web text.
        /// </summary>
        /// <value>
        /// The web text.
        /// </value>
        string WebText { get; set; }
        /// <summary>
        /// Gets or sets the twitter name.
        /// </summary>
        string TwitterText { get; set; }
        /// <summary>
        /// Gets or sets the email text.
        /// </summary>
        /// <value>
        /// The email text.
        /// </value>
        string EmailText { get; set; }

        string WebToolTip { get; set; }
        string TwitterToolTip { get; set; }
        string EmailToolTip { get; set; }

        string VersionText { get; set; }

        string CopyrightText { get; set; }
        string Text { get; set; }

        void Show();

        void Activate();
    }
}
