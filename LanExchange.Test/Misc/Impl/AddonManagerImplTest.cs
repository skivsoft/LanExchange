// using System;

// using LanExchange.Base;

// using LanExchange.Plugin.WinForms.Impl;

// using NUnit.Framework;


// namespace LanExchange.Misc.Impl

// {

// [TestFixture]

// internal class AddonManagerTest

// {

// [SetUp]

// public void SetUp()

// {

// m_Manager = new AddonManagerImpl();

// }


// [TearDown]

// public void TearDown()

// {

// m_Manager = null;

// }


// private AddonManagerImpl m_Manager;


// [Test]

// public void DuplicateProgramId()

// {

// // var addon1 = new AddonProgram("calc", @"%SystemRoot%\system32\calc.exe");


// // var addon2 = new AddonProgram("calc", @"%SystemRoot%\system32\notepad.exe");


// // m_Manager.Programs.Add("calc1", addon1);


// }


// [Test]

// public void DuplicateProgramKeyException()

// {

// App.SetContainer(ContainerBuilder.Build());

// m_Manager.Programs.Add("calc", new AddonProgram("calc", @"%SystemRoot%\system32\calc.exe"));

// Assert.Throws<ArgumentException>(() => m_Manager.Programs.Add("calc", new AddonProgram("notepad", @"%SystemRoot%\system32\notepad.exe")));

// }

// }

// }
