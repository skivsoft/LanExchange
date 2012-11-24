using System;
using System.Collections.Generic;
using System.Text;

namespace LanExchange.Network
{
    public class DataChangedEventArgs : EventArgs
    {
        public string Subject;
        public object Data;
    }

    public interface ISubscriber
    {
        void DataChanged(ISubscriptionProvider sender, DataChangedEventArgs e);
    }
}
