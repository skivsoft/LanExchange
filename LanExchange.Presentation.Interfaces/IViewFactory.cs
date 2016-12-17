using System;

namespace LanExchange.Presentation.Interfaces
{
    /// <summary>
    /// The view factory interface.
    /// </summary>
    public interface IViewFactory
    {
        /// <summary>
        /// Creates the panel view.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        IPanelView CreatePanelView();

        /// <summary>
        /// Gets the pages view.
        /// TODO: need change to create
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        IPagesView GetPagesView();

        /// <summary>
        /// Creates the status panel view.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        IStatusPanelView CreateStatusPanelView();
    }
}
