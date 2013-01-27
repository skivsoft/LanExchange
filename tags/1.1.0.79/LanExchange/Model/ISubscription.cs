using System.Collections.Generic;
using System.Collections;

namespace LanExchange.Model
{
    /// <summary>
    /// ISubscription interface declaration in Subscription-Subscriber-Subject model.
    /// </summary>
    public interface ISubscription
    {
        // properties
        int RefreshInterval { get; set; }
        // methods
        void SubscribeToSubject(ISubscriber sender, ISubject subject);
        void UnSubscribe(ISubscriber sender, bool updateTimer);
        bool HasStrategyForSubject(ISubject subject); // used for check subitems (level down/level up)
        IEnumerable GetListBySubject(ISubject subject); // used for update items
        IEnumerable<KeyValuePair<ISubject, IList<ISubscriber>>> GetSubjects(); // used for debug subjects
    }
}
