using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LanExchange.SDK;
using NUnit.Framework;

namespace LanExchange.Plugin.FileSystem
{
    [TestFixture]
    class FilePanelItemTest
    {
        [Test]
        public void TestSerialize()
        {
            var drive = new DrivePanelItem(new FileRoot(), @"C:\");
            var item = new FilePanelItem(drive, @"c:\windows");
            var content = SerializeUtils.SerializeObjectToXML(item);
        }
    }
}
