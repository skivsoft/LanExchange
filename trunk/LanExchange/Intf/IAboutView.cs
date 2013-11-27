using System.Windows.Forms;
using LanExchange.SDK;

namespace LanExchange.Intf
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
