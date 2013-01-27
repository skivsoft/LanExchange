namespace LanExchange.Sdk
{
    /// <summary>
    /// ISubject interface declaration in Subscription-Subscriber-Subject model.
    /// </summary>
    public interface ISubject
    {
        /// <summary>
        /// Gets the subject.
        /// </summary>
        /// <value>
        /// The subject.
        /// </value>
        string Subject { get; }
        /// <summary>
        /// Gets a value indicating whether this instance is cacheable.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is cacheable; otherwise, <c>false</c>.
        /// </value>
        bool IsCacheable { get; }
    }
}
