using System.Collections;

namespace LanExchange.Sdk
{
    /// <summary>
    /// ISubscription interface declaration in Subscription-Subscriber-Subject model.
    /// </summary>
    public interface ISubscription
    {
        /// <summary>
        /// Gets or sets the refresh interval.
        /// </summary>
        /// <value>
        /// The refresh interval.
        /// </value>
        int RefreshInterval { get; set; }
        /// <summary>
        /// Subscribes to subject.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="subject">The subject.</param>
        void SubscribeToSubject(ISubscriber sender, ISubject subject);
        /// <summary>
        /// Uns the subscribe.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="updateTimer">if set to <c>true</c> [update timer].</param>
        void UnSubscribe(ISubscriber sender, bool updateTimer);
        /// <summary>
        /// Determines whether [has strategy for subject] [the specified subject].
        /// </summary>
        /// <param name="subject">The subject.</param>
        /// <returns>
        ///   <c>true</c> if [has strategy for subject] [the specified subject]; otherwise, <c>false</c>.
        /// </returns>
        bool HasStrategyForSubject(ISubject subject); // used for check subitems (level down/level up)
        /// <summary>
        /// Gets the list by subject.
        /// </summary>
        /// <param name="subject">The subject.</param>
        /// <returns></returns>
        IEnumerable GetListBySubject(ISubject subject); // used for update items
        //IEnumerable<KeyValuePair<ISubject, IList<ISubscriber>>> GetSubjects(); // used for debug subjects
    }
}
