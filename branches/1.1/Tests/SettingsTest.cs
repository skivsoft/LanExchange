using System;
using System.Collections.Generic;
using System.Text;
using LanExchange;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    class SettingsTest
    {
        [Test]
        public void SetIntValue()
        {
            Settings.GetInstance().SetIntValue("test_int", 1);
            int value = Settings.GetInstance().GetIntValue("test_int", 0);
            Assert.AreEqual(1, value);
        }

        [Test]
        public void SetStrValue()
        {
            Settings.GetInstance().SetStrValue("test_str", "test");
            string value = Settings.GetInstance().GetStrValue("test_str", "");
            Assert.AreEqual("test", value);
        }

        [Test]
        public void SetListValue()
        {
            Settings.GetInstance().SetListValue("test_list", null);
            List<string> value = Settings.GetInstance().GetListValue("test_list");
            Assert.IsNotNull(value);
            Assert.AreEqual(0, value.Count);
        }

    }
}
