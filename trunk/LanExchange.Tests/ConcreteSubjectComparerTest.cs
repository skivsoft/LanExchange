using LanExchange.SDK;
using NUnit.Framework;

namespace Sdk.Tests
{
    
    
    /// <summary>
    ///This is a test class for ConcreteSubjectComparerTest and is intended
    ///to contain all ConcreteSubjectComparerTest Unit Tests
    ///</summary>
    [TestFixture]
    public class ConcreteSubjectComparerTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


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
        }
    }
}
