using NUnit.Framework;

namespace LanExchange.SDK.Tests
{
    
    
    /// <summary>
    ///This is a test class for ConcreteSubjectComparerTest and is intended
    ///to contain all ConcreteSubjectComparerTest Unit Tests
    ///</summary>
    [TestFixture]
    public class ConcreteSubjectComparerTest
    {
        /// <summary>
        ///A test for Equals
        ///</summary>
        [Test]
        public void EqualsTest()
        {
            ConcreteSubjectComparer target = new ConcreteSubjectComparer();
            Assert.AreEqual(true, target.Equals(null, null));
            Assert.AreEqual(false, target.Equals(ConcreteSubject.s_Root, null));
            Assert.AreEqual(false, target.Equals(null, ConcreteSubject.s_Root));
            Assert.AreEqual(true, target.Equals(ConcreteSubject.s_Root, ConcreteSubject.s_Root));
            Assert.AreEqual(false, target.Equals(ConcreteSubject.s_NotSubscribed, ConcreteSubject.s_Root));
        }

        /// <summary>
        ///A test for GetHashCode
        ///</summary>
        [Test]
        public void GetHashCodeTest()
        {
            ConcreteSubjectComparer target = new ConcreteSubjectComparer(); // TODO: Initialize to an appropriate value
            Assert.AreEqual(0, target.GetHashCode(null));
            Assert.AreEqual(ConcreteSubject.s_Root.Subject.GetHashCode(), target.GetHashCode(ConcreteSubject.s_Root));
        }
    }
}
