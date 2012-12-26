using System;
using System.Collections.Generic;

namespace LanExchange.Model
{
    public interface ISubscription
    {
        void SubscribeToSubject(ISubscriber sender, string subject);
        void UnSubscribe(ISubscriber sender);
    }
}
