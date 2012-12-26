using System;

namespace LanExchange.Model
{
    public class DataChangedEventArgs : EventArgs
    {
        public string Subject;
        public object Data;
    }

    public interface ISubscriber
    {
        void DataChanged(ISubscription sender, DataChangedEventArgs e);
    }

}
