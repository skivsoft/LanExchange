using LanExchange.Plugin.Shortcut;
using LanExchange.Presentation.WinForms.Helpers;
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