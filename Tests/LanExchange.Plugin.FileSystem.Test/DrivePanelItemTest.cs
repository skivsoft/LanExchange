using LanExchange.Presentation.WinForms.Helpers;
using NUnit.Framework;

namespace LanExchange.Plugin.FileSystem
{
    [TestFixture]
    internal class DrivePanelItemTest
    {
        [Test]
        public void TestSerialize()
        {
            var item = new DrivePanelItem(new FileRoot(), @"C:\");
            string content = SerializeHelper.SerializeObjectToXml(item);
        }
    }
}