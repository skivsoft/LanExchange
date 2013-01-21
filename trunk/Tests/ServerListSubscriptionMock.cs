using System;
using System.ComponentModel;
using LanExchange.Model;

namespace Tests
{
    class ServerListSubscriptionMock : PanelSubscription
    {
        public string args;
        public bool Cancelled;
        public bool DeliverMode;

        protected override void OneWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = args;
            e.Cancel = Cancelled;
        }

        protected override void OneWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (DeliverMode)
                base.OneWorker_RunWorkerCompleted(sender, e);
        }
    }
}
