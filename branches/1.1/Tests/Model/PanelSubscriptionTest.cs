using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using LanExchange.Utils;
using LanExchange.Model;

namespace Tests
{
    /// <summary>
    ///This is a test class for NetworkScannerTest and is intended
    ///to contain all NetworkScannerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class PanelSubscriptionTest
    {
        private TestContext testContextInstance;
        private PanelSubscriptionMock Instance;

        
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
            Instance = new PanelSubscriptionMock();
        }
        
        [TestCleanup()]
        public void MyTestCleanup()
        {
            Instance = null;
        }

        [TestMethod()]
        public void CheckSingleton()
        {
            var instance1 = PanelSubscriptionMock.Instance;
            var instance2 = PanelSubscriptionMock.Instance;
            Assert.AreSame(instance1, instance2);
        }

        /// <summary>
        ///A test for SubscribeToSubject
        ///</summary>
        [TestMethod()]
        public void SubscribeToSubjectTest()
        {
            //ISubscriber sub1 = null;
            //Instance.SubscribeToSubject(sub1, new ConcreteSubject {Subject = "test"});
            //Assert.IsTrue(Instance.HasSubscribers(), "1 subscriber");
            //Instance.UnSubscribe(sub1);
            //Assert.IsFalse(Instance.HasSubscribers(), "0 subscriber");
        }

        //[TestMethod()]
        //public void DeliveryForOneSubject()
        //{
        //    SubscriberMock sub1 = new SubscriberMock();
        //    IList<ServerInfo> List = new List<ServerInfo>();
        //    NetApi32.SERVER_INFO_101 info = new NetApi32.SERVER_INFO_101();
        //    info.sv101_name = "HELLO";
        //    List.Add(new ServerInfo(info));
        //    Instance.args.Subject = "one";
        //    Instance.args.Data = List;
        //    Instance.DeliverMode = true;
        //    Instance.SubscribeToSubject(sub1, "one");
        //    while (Instance.IsBusy)
        //    {
        //        // wait for delivery
        //    }
        //    if (sub1.IsEventFired)
        //    {
        //        Assert.IsInstanceOfType(sub1.e.Data, typeof(IList<ServerInfo>));
        //        List = (IList<ServerInfo>)sub1.e.Data;
        //        Assert.AreEqual(1, List.Count);
        //        Assert.AreEqual("HELLO", List[0].Name);
        //    }
        //    Instance.DeliverMode = false;
        //}

        //[TestMethod()]
        //public void UpdateAfterUnsubscribe()
        //{
        //    SubscriberMock sub1 = new SubscriberMock();
        //    IList<ServerInfo> List = new List<ServerInfo>();
        //    NetApi32.SERVER_INFO_101 info = new NetApi32.SERVER_INFO_101 { sv101_name = "HELLO" };
        //    List.Add(new ServerInfo(info));
        //    Instance.args.Subject = "one";
        //    Instance.args.Data = List;
        //    Instance.SubscribeToSubject(sub1, "one");
        //    Instance.DeliverMode = true;
        //    Instance.UnSubscribe(sub1);
        //    Assert.IsTrue(sub1.IsEventFired);
        //    if (sub1.IsEventFired)
        //    {
        //        Assert.IsNull(sub1.e.Data);
        //    }
        //    Instance.DeliverMode = false;


        //}

    }
}
