using LanExchange.SDK.Presentation.Interfaces;

namespace LanExchange.SDK
{
    /// <summary>
    /// View for filter panel.
    /// </summary>
    public interface IFilterView : IView
    {
        /// <summary>
        /// Gets or sets a value indicating whether this instance is visible.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is visible; otherwise, <c>false</c>.
        /// </value>
        bool IsVisible { get; set; }
        /// <summary>
        /// Gets the presenter.
        /// </summary>
        /// <value>
        /// The presenter.
        /// </value>
        IFilterPresenter Presenter { get; }

        /// <summary>
        /// Updates from model.
        /// </summary>
        /// <param name="model">The model.</param>
        void UpdateFromModel(IFilterModel model);
        /// <summary>
        /// Does the filter count changed.
        /// </summary>
        void DoFilterCountChanged();
        //void FocusAndKeyPress(KeyPressEventArgs e);
        //void FocusMe();
        /// <summary>
        /// Sets the filter text.
        /// </summary>
        /// <param name="value">The value.</param>
        void SetFilterText(string value);
    }
}
