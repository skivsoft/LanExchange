using NUnit.Framework;

namespace LanExchange.Plugin.Users
{
    [TestFixture]
    public class PathDistinctTest
    {
        [Test]
        public void TestConstructorAndCountIsZero()
        {
            var instance = new PathDistinct();
            Assert.AreEqual(0, instance.Count);
        }

        [Test]
        public void TestAddTwoItemsDifferent()
        {
            var instance = new PathDistinct();
            instance.Add("a");
            instance.Add("b");
            Assert.AreEqual(2, instance.Count);
        }

        [Test]
        public void TestAddTwoItemsEqual()
        {
            var instance = new PathDistinct();
            instance.Add("b");
            instance.Add("B");
            Assert.AreEqual(1, instance.Count);
        }

        [Test]
        public void TestIsPrefixWorks()
        {
            var instance = new PathDistinct();
            instance.Add(@"HELLO\every\body");
            instance.Add(@"HelLo\ev.....");
            Assert.AreEqual(@"HELLO", instance.Prefix);
        }

        [Test]
        public void TestItems()
        {
            var instance = new PathDistinct();
            instance.Add(@"a\b");
            instance.Add(@"c\d");
            var enumerator = instance.Items.GetEnumerator();
            Assert.IsTrue(enumerator.MoveNext());
            Assert.AreEqual(@"a\b", enumerator.Current);
            Assert.IsTrue(enumerator.MoveNext());
            Assert.AreEqual(@"c\d", enumerator.Current);
            Assert.IsFalse(enumerator.MoveNext());
        }
    }
}
