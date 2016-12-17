using System.IO;
using System.Xml.Serialization;
using NUnit.Framework;

namespace LanExchange.SDK
{
    public class TestSomeBase
    {
        public string Hello;
    }

    public abstract class TestSomeAbstractBase
    {
        public abstract string Hello { get; set; }
    }

    public class TestServerInfo
    {
        public string Name;
    }

    public class TestSomeDescendant : TestSomeAbstractBase
    {
        private readonly TestServerInfo m_SI;

        public TestSomeDescendant()
        {
            m_SI = new TestServerInfo();
        }

        public override string Hello
        {
            get { return m_SI.Name; }
            set { m_SI.Name = value; }
        }
    }

    [TestFixture]
    internal class XmlSerializeTest
    {
        [Test]
        public void TestSerializeAbstract()
        {
            TestSomeAbstractBase obj = new TestSomeDescendant();
            obj.Hello = "World";
            var extraTypes = new[] { typeof(TestSomeDescendant) };

           var ser = new XmlSerializer(typeof(TestSomeAbstractBase), extraTypes);
            string result;
            using (var sw = new StringWriter())
            {
                ser.Serialize(sw, obj);
                result = sw.ToString();
            }
            Assert.IsNotEmpty(result);
            using (var sr = new StringReader(result))
            {
                obj = (TestSomeAbstractBase)ser.Deserialize(sr);
                Assert.AreEqual(typeof(TestSomeDescendant), obj.GetType());
            }
        }

        [Test]
        public void TestSerializeNonAbstract()
        {
            var obj = new TestSomeBase();
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