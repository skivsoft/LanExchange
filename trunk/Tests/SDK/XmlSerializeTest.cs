using System;
using System.IO;
using System.Xml.Serialization;
using NUnit.Framework;

namespace LanExchange.SDK
{
    public class SomeBase
    {
        public string Hello;
    }

    public abstract class SomeAbstractBase
    {
        public abstract string Hello { get; set; }
    }

    public class ServerInfo
    {
        public string Name;
    }

    public class SomeDescendant : SomeAbstractBase
    {
        private readonly ServerInfo m_SI;

        public SomeDescendant()
        {
            m_SI = new ServerInfo();
        }

        public override string Hello
        {
            get { return m_SI.Name; }
            set { m_SI.Name = value; }
        }
    }

    [TestFixture]
    class XmlSerializeTest
    {
        [Test]
        public void TestSerializeAbstract()
        {
            SomeAbstractBase obj = new SomeDescendant();
            obj.Hello = "World";
            var extraTypes = new Type[] {typeof (SomeDescendant) };
            var ser = new XmlSerializer(typeof(SomeAbstractBase), extraTypes);
            string result;
            using (var sw = new StringWriter())
            {
                ser.Serialize(sw, obj);
                result = sw.ToString();
            }
            Assert.IsNotEmpty(result);
            using (var sr = new StringReader(result))
            {
                obj = (SomeAbstractBase)ser.Deserialize(sr);
                Assert.AreEqual(typeof(SomeDescendant), obj.GetType());
            }
        }

        [Test]
        public void TestSerializeNonAbstract()
        {
            var obj = new SomeBase();
            obj.Hello = "World";
            var ser = new XmlSerializer(obj.GetType());
            string result;
            using (var sw = new StringWriter())
            {
                ser.Serialize(sw, obj);
                result = sw.ToString();
            }
            Assert.IsNotEmpty(result);
        }
    }
}
