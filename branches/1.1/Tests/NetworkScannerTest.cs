using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using LanExchange.Network;

namespace Tests
{
    [TestFixture]
    class NetworkScannerTest
    {
        [Test]
        public void CheckSingleton()
        {
            NetworkScanner instance1 = NetworkScanner.GetInstance();
            NetworkScanner instance2 = NetworkScanner.GetInstance();
            Assert.AreSame(instance1, instance2);
        }
    }
}
