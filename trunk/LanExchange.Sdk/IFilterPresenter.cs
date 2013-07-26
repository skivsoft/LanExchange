namespace LanExchange.SDK
{
    /// <summary>
    /// Presenter for filter panel.
    /// </summary>
    public interface IFilterPresenter
    {
        /// <summary>
        /// Sets the model.
        /// </summary>
        /// <param name="model">The model.</param>
        void SetModel(IFilterModel model);
    }
}
