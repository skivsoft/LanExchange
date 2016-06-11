using LanExchange.Plugin.FileSystem.Test;
using NUnit.Framework;

namespace LanExchange.Plugin.FileSystem
{
    [TestFixture]
    internal class FilePanelItemTest
    {
        [Test]
        public void TestSerialize()
        {
            var drive = new DrivePanelItem(new FileRoot(), @"C:\");
            var item = new FilePanelItem(drive, @"c:\windows");
            string content = SerializeHelper.SerializeObjectToXml(item);
        }
    }
}