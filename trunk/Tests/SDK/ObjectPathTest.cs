﻿using System;
using NUnit.Framework;

namespace LanExchange.SDK
{
    //[TestFixture]
    //class ObjectPathTest
    //{
    //    private ObjectPath<int> m_Path;

    //    [SetUp]
    //    public void SetUp()
    //    {
    //        m_Path = new ObjectPath<int>();    
    //    }

    //    [TearDown]
    //    public void TearDown()
    //    {
    //        m_Path = null;
    //    }

    //    [Test]
    //    public void TestClear()
    //    {
    //        m_Path.Clear();
    //        Assert.IsTrue(m_Path.IsEmptyOrRoot);
    //    }

    //    [Test]
    //    public void TestPushPeekPop()
    //    {
    //        m_Path.Push(Int32.Parse("1"));
    //        Assert.IsFalse(m_Path.IsEmptyOrRoot);
    //        var value = m_Path.Peek();
    //        Assert.AreEqual(value, Int32.Parse("1"));
    //        m_Path.Pop();
    //        Assert.IsTrue(m_Path.IsEmptyOrRoot);
    //    }

    //    [Test]
    //    public void TestToString()
    //    {
    //        m_Path.Push(Int32.Parse("1"));
    //        m_Path.Push(Int32.Parse("2"));
    //        Assert.AreEqual(@"1\2", m_Path.ToString());
    //    }

    //    private bool changedFired = false;

    //    private void Event_Changed(object sender, EventArgs args)
    //    {
    //        changedFired = true;
    //    }

    //    [Test]
    //    public void TestChanged()
    //    {
    //        m_Path.Changed += Event_Changed;
    //        changedFired = false;
    //        m_Path.Push(Int32.Parse("1"));
    //        Assert.IsTrue(changedFired);
    //    }
    //}
}
