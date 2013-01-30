using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LanExchange.Plugin;

namespace Plugin.Users.Tests
{
    [TestClass]
    public class PathFixerTest
    {
        [TestMethod]
        public void FixLdapPath_Null()
        {
            var path = new PathFixer();
            var s = path.FixLdapPath(null);
            Assert.IsNotNull(s);
            Assert.AreEqual(String.Empty, s);
        }

        [TestMethod]
        public void FixLdapPath_Init()
        {
            var path = new PathFixer();
            var s = path.FixLdapPath("Ldap://qqq,www");
            Assert.AreEqual(@"www\qqq", s);
        }

        [TestMethod]
        public void FixLdapPath_LdapUpper()
        {
            var path = new PathFixer();
            var s = path.FixLdapPath("LDAP://qqq,WWW,aaa");
            Assert.AreEqual(@"aaa\WWW\qqq", s);
        }

        [TestMethod]
        public void FixLdapPath_LdapError()
        {
            var path = new PathFixer();
            var s = path.FixLdapPath("ldap:/qqq,www");
            Assert.AreEqual(String.Empty, s);
        }

        [TestMethod]
        public void FixLdapPath_DC_OU()
        {
            var path = new PathFixer();
            var s = path.FixLdapPath("ldap://dc=1,ou=2");
            Assert.AreEqual(@"2\1", s);
        }

    }
}
