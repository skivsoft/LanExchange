namespace LanExchange.SDK
{
    /// <summary>
    /// ISubscriber interface declaration in Subscription-Subscriber-Subject model.
    /// </summary>
    public interface ISubscriber
    {
        /// <summary>
        /// Datas the changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="subject">The subject.</param>
        void DataChanged(ISubscription sender, ISubject subject);
    }

}
