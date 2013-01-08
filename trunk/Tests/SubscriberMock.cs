using System;
using LanExchange.Model;

namespace Tests
{
    class SubscriberMock : ISubscriber
    {
        public bool IsEventFired;
        public string Subject;

        public SubscriberMock()
        {

        }

        public void DataChanged(ISubscription sender, string subject)
        {
            IsEventFired = true;
            Subject = subject;
        }
    }
}
