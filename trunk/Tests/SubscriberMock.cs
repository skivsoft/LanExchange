using System;
using LanExchange.Model;

namespace Tests
{
    class SubscriberMock : ISubscriber
    {
        public bool IsEventFired;
        public ISubject Subject;

        public void DataChanged(ISubscription sender, ISubject subject)
        {
            IsEventFired = true;
            Subject = subject;
        }
    }
}
