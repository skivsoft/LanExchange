using NUnit.Framework;

namespace LanExchange.Plugin.Users
{
    [TestFixture]
    public class PathFixerTest
    {
        [Test]
        public void FixLdapPath_Null()
        {
            var s = PathFixer.FixLdapPath(null);
            Assert.IsNotNull(s);
            Assert.AreEqual(string.Empty, s);
        }

        [Test]
        public void FixLdapPath_Init()
        {
            var s = PathFixer.FixLdapPath("Ldap://qqq,www");
            Assert.AreEqual(@"www\qqq", s);
        }

        [Test]
        public void FixLdapPath_LdapUpper()
        {
            var s = PathFixer.FixLdapPath("LDAP://qqq,WWW,aaa");
            Assert.AreEqual(@"aaa\WWW\qqq", s);
        }

        [Test]
        public void FixLdapPath_LdapError()
        {
            var s = PathFixer.FixLdapPath("ldap:/qqq,www");
            Assert.AreEqual(string.Empty, s);
        }

        [Test]
        public void FixLdapPath_DC_OU()
        {
            var s = PathFixer.FixLdapPath("ldap://dc=1,ou=2");
            Assert.AreEqual(@"2\1", s);
        }

    }
}
