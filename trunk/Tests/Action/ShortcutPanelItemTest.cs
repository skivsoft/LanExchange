using LanExchange.Plugin.FileSystem;
using LanExchange.SDK;
using NUnit.Framework;

namespace LanExchange.Action
{
    [TestFixture]
    class ShortcutPanelItemTest
    {
        [Test]
        public void TestSerialize()
        {
            var item = new ShortcutPanelItem(new ShortcutRoot(), "F1", "Helo");
            var content = SerializeUtils.SerializeObjectToXML(item);
        }
    }
}
