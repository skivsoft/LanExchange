namespace LanExchange.Sdk
{
    /// <summary>
    /// ISubject interface declaration in Subscription-Subscriber-Subject model.
    /// </summary>
    public interface ISubject
    {
        string Subject { get; }
        bool IsCacheable { get; }
    }
}
