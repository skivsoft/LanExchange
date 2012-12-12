using System;
using System.Collections.Generic;

namespace LanExchange.Network
{
    public interface ISubscriptionProvider
    {
        void SubscribeToSubject(ISubscriber sender, string subject);
        void SubscribeToAll(ISubscriber sender);
        void UnSubscribe(ISubscriber sender);
        void EnableSubscriptions();
        void DisableSubscriptions();
        IDictionary<string, IList<ISubscriber>> GetSubjects();
        IList<ISubscriber> GetAllSubjects();
    }
}
