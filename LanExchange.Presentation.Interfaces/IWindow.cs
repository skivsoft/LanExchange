using System;

namespace LanExchange.Presentation.Interfaces
{
    /// <summary>
    /// The window interface.
    /// </summary>
    /// <seealso cref="IView" />
    public interface IWindow : IView, IDisposable
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
        /// Gets or sets a value indicating whether this <see cref="IView"/> is visible.
        /// </summary>
        /// <value>
        ///   <c>true</c> if visible; otherwise, <c>false</c>.
        /// </value>
        bool Visible { get; set; }

        /// <summary>
        /// Sets the window bounds.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="top">The top.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        void SetBounds(int left, int top, int width, int height);

        /// <summary>
        /// Activates the window.
        /// </summary>
        void Activate();

        /// <summary>
        /// Closes the window.
        /// </summary>
        void Close();
    }
}