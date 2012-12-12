using LanExchange.Network;
using System;
using System.ComponentModel;

namespace Tests
{
    class NetworkScannerMock : NetworkScanner
    {
        public DataChangedEventArgs args = new DataChangedEventArgs();
        public bool DeliverMode;

        protected override void OneWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = args;
        }

        protected override void OneWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (DeliverMode)
                base.OneWorker_RunWorkerCompleted(sender, e);
        }
    }
}
