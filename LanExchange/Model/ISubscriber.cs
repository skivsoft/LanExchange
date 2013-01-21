namespace LanExchange.Model
{
    /// <summary>
    /// ISubscriber interface declaration in Subscription-Subscriber-Subject model.
    /// </summary>
    public interface ISubscriber
    {
        void DataChanged(ISubscription sender, ISubject subject);
    }

}
