namespace LanExchange.Model
{
    public interface ISubscriber
    {
        void DataChanged(ISubscription sender, string subject);
    }

}
