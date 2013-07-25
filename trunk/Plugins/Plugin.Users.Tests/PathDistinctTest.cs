using NUnit.Framework;
using Plugins.Plugin;

namespace Plugin.Users.Tests
{
    [TestFixture]
    public class PathDistinctTest
    {
        [Test]
        public void ConstructorAndCountIsZero()
        {
            var instance = new PathDistinct();
            Assert.AreEqual(0, instance.Count);
        }

        [Test]
        public void AddTwoItemsDifferent()
        {
            var instance = new PathDistinct();
            instance.Add("a");
            instance.Add("b");
            Assert.AreEqual(2, instance.Count);
        }

        [Test]
        public void AddTwoItemsEqual()
        {
            var instance = new PathDistinct();
            instance.Add("b");
            instance.Add("B");
            Assert.AreEqual(1, instance.Count);
        }

        [Test]
        public void IsPrefixWorks()
        {
            var instance = new PathDistinct();
            instance.Add(@"HELLO\every\body");
            instance.Add(@"HelLo\ev.....");
            Assert.AreEqual(@"HELLO", instance.Prefix);
        }
    }
}
