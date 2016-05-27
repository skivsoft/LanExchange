using LanExchange.SDK.Presentation.Interfaces;
using System;

namespace LanExchange.SDK
{
    /// <summary>
    /// The window interface.
    /// </summary>
    /// <seealso cref="LanExchange.SDK.IView" />
    public interface IWindow : IView
    {
        /// <summary>
        /// Occurs when window closed.
        /// </summary>
        event EventHandler ViewClosed;

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        string Text { get; set; }

        /// <summary>
        /// Sets the window bounds.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="top">The top.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        void SetBounds(int left, int top, int width, int height);

        /// <summary>
        /// Shows the window.
        /// </summary>
        void Show();

        /// <summary>
        /// Hides the window.
        /// </summary>
        void Hide();

        /// <summary>
        /// Activates the window.
        /// </summary>
        void Activate();
    }
}