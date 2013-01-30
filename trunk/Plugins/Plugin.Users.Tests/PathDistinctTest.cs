using LanExchange.Plugin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Plugin.Users.Tests
{
    [TestClass]
    public class PathDistinctTest
    {
        [TestMethod]
        public void ConstructorAndCountIsZero()
        {
            var instance = new PathDistinct();
            Assert.AreEqual(0, instance.Count);
        }

        [TestMethod]
        public void AddTwoItemsDifferent()
        {
            var instance = new PathDistinct();
            instance.Add("a");
            instance.Add("b");
            Assert.AreEqual(2, instance.Count);
        }

        [TestMethod]
        public void AddTwoItemsEqual()
        {
            var instance = new PathDistinct();
            instance.Add("b");
            instance.Add("B");
            Assert.AreEqual(1, instance.Count);
        }

        [TestMethod]
        public void IsPrefixWorks()
        {
            var instance = new PathDistinct();
            instance.Add(@"HELLO\every\body");
            instance.Add(@"HelLo\ev.....");
            Assert.AreEqual(@"HELLO", instance.Prefix);
        }
    }
}
