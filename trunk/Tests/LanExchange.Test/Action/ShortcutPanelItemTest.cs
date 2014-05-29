using LanExchange.Plugin.Shortcut;
using LanExchange.SDK;
using NUnit.Framework;

namespace LanExchange.Action
{
    [TestFixture]
    internal class ShortcutPanelItemTest
    {
        [Test]
        public void TestSerialize()
        {
            var item = new ShortcutPanelItem(new ShortcutRoot(), "F1", "Helo");
            string content = SerializeUtils.SerializeObjectToXml(item);
        }
    }
}