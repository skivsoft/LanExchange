using System;
using FluentAssertions;
using LanExchange.Presentation.Interfaces;
using Moq;
using NUnit.Framework;

namespace LanExchange.Application.Presenters
{
    [TestFixture]
    public class PresenterBaseTest
    {
        [Test]
        public void Initialize_NullView_Throw()
        {
            var mock = new Mock<PresenterBase<IView>>();
            Assert.Throws<ArgumentNullException>(() => mock.Object.Initialize(null));
        }

        [Test]
        public void Initialize_InitializePresenterCalled()
        {
            var mockView = new Mock<IView>();
            var presenter = new PresenterBaseSpy<IView>();
            presenter.Initialize(mockView.Object);
            presenter.InitializePresenterCalled.Should().BeTrue();
        }
    }
}
