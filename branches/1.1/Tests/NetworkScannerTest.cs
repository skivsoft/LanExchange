using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using LanExchange.Network;
using System.ComponentModel;

namespace Tests
{
    class NetworkScannerMock : NetworkScanner
    {
        public DataChangedEventArgs args = new DataChangedEventArgs();
        public bool DeliverMode = false;

        public NetworkScannerMock()
        {
            //RefreshInterval = 100 * 1000;
        }

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

    class SubscriberMock : ISubscriber
    {
        public bool IsEventFired = false;
        public DataChangedEventArgs e = new DataChangedEventArgs();

        public SubscriberMock()
        {

        }

        public void DataChanged(ISubscriptionProvider sender, DataChangedEventArgs e)
        {
            IsEventFired = true;
            this.e = e;
        }
    }
    
    
    [TestFixture]
    class NetworkScannerTest
    {
        NetworkScannerMock Instance = null;

        [SetUp]
        public void SetUp()
        {
            Instance = new NetworkScannerMock();
        }

        [TearDown]
        public void TearDown()
        {
            Instance = null;
        }

        [Test]
        public void CheckSingleton()
        {
            NetworkScanner instance1 = NetworkScannerMock.GetInstance();
            NetworkScanner instance2 = NetworkScannerMock.GetInstance();
            Assert.AreSame(instance1, instance2);
        }

        [Test]
        public void SubscribeToSubject()
        {
            ISubscriber sub1 = null;
            Instance.SubscribeToSubject(sub1, "test");
            Assert.IsTrue(Instance.HasSubscribers(), "1 subscriber");
            Instance.UnSubscribe(sub1);
            Assert.IsFalse(Instance.HasSubscribers(), "0 subscriber");
        }

        [Test]
        public void InstantUpdate1()
        {
            Assert.IsTrue(Instance.IsInstantUpdate, "init");
            Instance.DisableSubscriptions();
            Assert.IsTrue(Instance.IsInstantUpdate, "after disable");
            Instance.EnableSubscriptions();
            Assert.IsTrue(Instance.IsInstantUpdate, "after enable");

            ISubscriber sub1 = null;
            Instance.SubscribeToSubject(sub1, "test");
            Assert.IsTrue(Instance.HasSubscribers(), "1 subscriber");
            Assert.IsFalse(Instance.IsInstantUpdate, "must instant update after first subscribe");
            Instance.DisableSubscriptions();
            Assert.IsTrue(Instance.IsInstantUpdate, "1 subscriber after disable");
            Instance.EnableSubscriptions();
            Assert.IsFalse(Instance.IsInstantUpdate, "1 subscriber after enable");
        }

        [Test]
        public void DeliveryForOneSubject()
        {
            SubscriberMock sub1 = new SubscriberMock();
            IList<ServerInfo> List = new List<ServerInfo>();
            NetApi32.SERVER_INFO_101 info = new NetApi32.SERVER_INFO_101();
            info.sv101_name = "HELLO";
            List.Add(new ServerInfo(info));
            Instance.args.Subject = "one";
            Instance.args.Data = List;
            Instance.DeliverMode = true;
            Instance.SubscribeToSubject(sub1, "one");
            while (Instance.IsBusy)
            {
                // wait for delivery
            }
            if (sub1.IsEventFired)
            {
                Assert.IsInstanceOf(typeof(IList<ServerInfo>), sub1.e.Data);
                List = (IList<ServerInfo>)sub1.e.Data;
                Assert.AreEqual(1, List.Count);
                Assert.AreEqual("HELLO", List[0].Name);
            }
            Instance.DeliverMode = false;
        }

        [Test]
        public void UpdateAfterUnsubscribe()
        {
            SubscriberMock sub1 = new SubscriberMock();
            IList<ServerInfo> List = new List<ServerInfo>();
            NetApi32.SERVER_INFO_101 info = new NetApi32.SERVER_INFO_101();
            info.sv101_name = "HELLO";
            List.Add(new ServerInfo(info));
            Instance.args.Subject = "one";
            Instance.args.Data = List;
            Instance.SubscribeToSubject(sub1, "one");
            Instance.DeliverMode = true;
            Instance.UnSubscribe(sub1);
            Assert.IsTrue(sub1.IsEventFired);
            if (sub1.IsEventFired)
            {
                Assert.IsNull(sub1.e.Data);
            }
            Instance.DeliverMode = false;


        }
    }
}
