using LanExchange.SDK;
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
            string content = SerializeUtils.SerializeObjectToXML(item);
        }
    }
}