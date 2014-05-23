using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LanExchange.SDK;
using NUnit.Framework;

namespace LanExchange.Plugin.FileSystem
{
    [TestFixture]
    class DrivePanelItemTest
    {
        [Test]
        public void TestSerialize()
        {
            var item = new DrivePanelItem(new FileRoot(), @"C:\");
            var content = SerializeUtils.SerializeObjectToXML(item);
        }
    }
}
