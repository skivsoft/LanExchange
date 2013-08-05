using System;
using NUnit.Framework;
using LanExchange.Plugin.Users;

namespace Plugin.Users.Tests
{
    [TestFixture]
    public class PathFixerTest
    {
        [Test]
        public void FixLdapPath_Null()
        {
            var path = new PathFixer();
            var s = path.FixLdapPath(null);
            Assert.IsNotNull(s);
            Assert.AreEqual(string.Empty, s);
        }

        [Test]
        public void FixLdapPath_Init()
        {
            var path = new PathFixer();
            var s = path.FixLdapPath("Ldap://qqq,www");
            Assert.AreEqual(@"www\qqq", s);
        }

        [Test]
        public void FixLdapPath_LdapUpper()
        {
            var path = new PathFixer();
            var s = path.FixLdapPath("LDAP://qqq,WWW,aaa");
            Assert.AreEqual(@"aaa\WWW\qqq", s);
        }

        [Test]
        public void FixLdapPath_LdapError()
        {
            var path = new PathFixer();
            var s = path.FixLdapPath("ldap:/qqq,www");
            Assert.AreEqual(string.Empty, s);
        }

        [Test]
        public void FixLdapPath_DC_OU()
        {
            var path = new PathFixer();
            var s = path.FixLdapPath("ldap://dc=1,ou=2");
            Assert.AreEqual(@"2\1", s);
        }

    }
}
