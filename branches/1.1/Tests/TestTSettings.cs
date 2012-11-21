using System;
using System.Collections.Generic;
using System.Text;
using LanExchange;
using NUnit.Framework;
using System.Windows.Forms;

namespace Tests
{
    [TestFixture]
    class TestTSettings
    {
        [Test]
        public void SetIntValue()
        {
            TSettings.SetIntValue("test_int", 1);
            int value = TSettings.GetIntValue("test_int", 0);
            Assert.AreEqual(1, value);
        }

        [Test]
        public void SetStrValue()
        {
            TSettings.SetStrValue("test_str", "test");
            string value = TSettings.GetStrValue("test_str", "");
            Assert.AreEqual("test", value);
        }

        [Test]
        public void SetListValue()
        {
            TSettings.SetListValue("test_list", null);
            List<string> value = TSettings.GetListValue("test_list");
            Assert.IsNotNull(value);
            Assert.AreEqual(0, value.Count);
        }

    }
}
