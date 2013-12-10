using System;
using LanExchange.Core;
using LanExchange.SDK;
using LanExchange.SDK.Addon;
using LanExchange.UI.WinForms.Impl;
using NUnit.Framework;

namespace LanExchange.Misc.Impl
{
    [TestFixture]
    class AddonManagerTest
    {
        private AddonManagerImpl m_Manager;

        [SetUp]
        public void SetUp()
        {
            m_Manager = new AddonManagerImpl();
        }
        
        [TearDown]
        public void TearDown()
        {
            m_Manager = null;
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void DuplicateProgramKeyException()
        {
            App.SetContainer(ContainerBuilder.Build());
            m_Manager.Programs.Add("calc", new AddonProgram("calc", @"%SystemRoot%\system32\calc.exe"));
            m_Manager.Programs.Add("calc", new AddonProgram("notepad", @"%SystemRoot%\system32\notepad.exe"));
        }

        [Test]
        public void DuplicateProgramId()
        {
            //var addon1 = new AddonProgram("calc", @"%SystemRoot%\system32\calc.exe");
            //var addon2 = new AddonProgram("calc", @"%SystemRoot%\system32\notepad.exe");
            //m_Manager.Programs.Add("calc1", addon1);
        }
    }
}
