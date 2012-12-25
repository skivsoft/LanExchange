using LanExchange.Network;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using LanExchange.Utils;

namespace Tests
{
    /// <summary>
    ///This is a test class for NetworkScannerTest and is intended
    ///to contain all NetworkScannerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class NetworkScannerTest
    {
        private TestContext testContextInstance;
        private NetworkScannerMock Instance;

        
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        [TestInitialize()]
        public void MyTestInitialize()
        {
            Instance = new NetworkScannerMock();
        }
        
        [TestCleanup()]
        public void MyTestCleanup()
        {
            Instance = null;
        }

        [TestMethod()]
        public void CheckSingleton()
        {
            NetworkScanner instance1 = NetworkScannerMock.GetInstance();
            NetworkScanner instance2 = NetworkScannerMock.GetInstance();
            Assert.AreSame(instance1, instance2);
        }

        /// <summary>
        ///A test for SubscribeToSubject
        ///</summary>
        [TestMethod()]
        public void SubscribeToSubjectTest()
        {
            ISubscriber sub1 = null;
            Instance.SubscribeToSubject(sub1, "test");
            Assert.IsTrue(Instance.HasSubscribers(), "1 subscriber");
            Instance.UnSubscribe(sub1);
            Assert.IsFalse(Instance.HasSubscribers(), "0 subscriber");
        }

        /// <summary>
        ///A test for IsInstantUpdate
        ///</summary>
        [TestMethod()]
        public void IsInstantUpdateTest()
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

        [TestMethod()]
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
                Assert.IsInstanceOfType(sub1.e.Data, typeof(IList<ServerInfo>));
                List = (IList<ServerInfo>)sub1.e.Data;
                Assert.AreEqual(1, List.Count);
                Assert.AreEqual("HELLO", List[0].Name);
            }
            Instance.DeliverMode = false;
        }

        [TestMethod()]
        public void UpdateAfterUnsubscribe()
        {
            SubscriberMock sub1 = new SubscriberMock();
            IList<ServerInfo> List = new List<ServerInfo>();
            NetApi32.SERVER_INFO_101 info = new NetApi32.SERVER_INFO_101 { sv101_name = "HELLO" };
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
