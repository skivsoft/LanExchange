using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using LanExchange.Presentation.Interfaces;

namespace LanExchange.Presenter
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
