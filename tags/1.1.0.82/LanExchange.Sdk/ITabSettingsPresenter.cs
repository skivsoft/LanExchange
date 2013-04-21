namespace LanExchange.Sdk
{
    /// <summary>
    /// Presenter of LanExchange TabParams form.
    /// </summary>
    public interface ITabSettingsPresenter : ISubscriber
    {
        /// <summary>
        /// Sets the view.
        /// </summary>
        /// <param name="view">The view.</param>
        void SetView(ITabSettingView view);
        /// <summary>
        /// Sets the info.
        /// </summary>
        /// <param name="info">The info.</param>
        void SetInfo(IPanelModel info);
    }
}
