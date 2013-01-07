using System;
using System.Collections.Generic;
using System.Collections;

namespace LanExchange.Model
{
    public interface ISubscription
    {
        // properties
        int RefreshInterval { get; set; }
        // methods
        void SubscribeToSubject(ISubscriber sender, string subject);
        void UnSubscribe(ISubscriber sender);
        IDictionary<string, IList<ISubscriber>> GetSubjects();
        IEnumerable GetListBySubject(string subject);
    }
}
