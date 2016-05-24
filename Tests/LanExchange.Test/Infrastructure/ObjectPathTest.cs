using System;
using FluentAssertions;
using NUnit.Framework;
using System.Linq;
using LanExchange.SDK.Domain;

namespace LanExchange.Infrastructure
{
    [TestFixture]
    class ObjectPathTest
    {
        IObjectPath<int> path;

        [SetUp]
        public void SetUp()
        {
            path = new ObjectPath<int>();
        }

        [Test]
        public void Clear_NonEmptyPath_ReturnsEmptyPath()
        {
            path.Push(1);
            path.Clear();
            path.Any().Should().BeFalse();
        }

        [Test]
        public void First_OfTwoItems_ReturnsNumberOne()
        {
            path.Push(1);
            path.Push(2);
            path.First().Should().Be(1);
        }

        [Test]
        public void Last_OfTwoItems_ReturnsNumberTwo()
        {
            path.Push(1);
            path.Push(2);
            path.Last().Should().Be(2);
        }

        [Test]
        public void Push_FiresChangedEvent()
        {
            var changedFired = false;
            path.Changed += (sender, e) => changedFired = true;

            path.Push(1);
            changedFired.Should().BeTrue();
        }

        [Test]
        public void Pop_FiresChangedEvent()
        {
            path.Push(1);
            var changedFired = false;
            path.Changed += (sender, e) => changedFired = true;

            path.Pop();
            changedFired.Should().BeTrue();
        }

        [Test]
        public void Pop_OnEmptyPath_NotFiresChangedEvent()
        {
            var changedFired = false;
            path.Changed += (sender, e) => changedFired = true;

            path.Pop();
            changedFired.Should().BeFalse();
        }

        [Test]
        public void Peek_OnEmptyPath_ReturnsEmptyOption()
        {
            path.Peek().Any().Should().BeFalse();
        }

        [Test]
        public void Peek_OnNonEmptyPath_ReturnsSingleValue()
        {
            path.Push(1);
            path.Push(2);
            path.Peek().Single().Should().Be(2);
        }

        [Test]
        public void Clear_EmptyPath_NotFiresChangedEvent()
        {
            var changedFired = false;
            path.Changed += (sender, e) => changedFired = true;

            path.Clear();
            changedFired.Should().BeFalse();
        }

        [Test]
        public void Clear_NonEmptyPath_FireChangedEvent()
        {
            path.Push(1);
            var changedFired = false;
            path.Changed += (sender, e) => changedFired = true;

            path.Clear();
            changedFired.Should().BeTrue();
        }
    }
}