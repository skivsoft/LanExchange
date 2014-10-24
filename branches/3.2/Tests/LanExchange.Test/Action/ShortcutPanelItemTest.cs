using LanExchange.Helpers;
using LanExchange.Plugin.Shortcut;
using LanExchange.SDK;
using NUnit.Framework;

namespace LanExchange.Actions
{
    [TestFixture]
    internal class ShortcutPanelItemTest
    {
        [Test]
        public void TestSerialize()
        {
            var item = new ShortcutPanelItem(new ShortcutRoot(), "F1", "Helo");
            string content = SerializeHelper.SerializeObjectToXml(item);
        }
    }
}