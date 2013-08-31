using System;
using NUnit.Framework;
using LanExchange.Intf;

namespace LanExchange.Core
{
    [TestFixture]
    public class SimpleIocContainerTests
    {
        [Test]
        public void should_resolve_object()
        {
            var container = new SimpleIocContainer();

            container.Register<ITypeToResolve, ConcreteType>();

            var instance = container.Resolve<ITypeToResolve>();

            Assert.IsInstanceOf<ConcreteType>(instance);
        }

        [Test, ExpectedException(typeof(TypeNotRegisteredException))]
        public void should_throw_exception_if_type_not_registered()
        {
            var container = new SimpleIocContainer();
            container.Resolve<ITypeToResolve>();
        }

        [Test]
        public void should_resolve_object_with_registered_constructor_parameters()
        {
            var container = new SimpleIocContainer();

            container.Register<ITypeToResolve, ConcreteType>();
            container.Register<ITypeToResolveWithConstructorParams, ConcreteTypeWithConstructorParams>();

            var instance = container.Resolve<ITypeToResolveWithConstructorParams>();

            Assert.IsInstanceOf<ConcreteTypeWithConstructorParams>(instance);
        }

        [Test]
        public void should_create_singleton_instance_by_default()
        {
            var container = new SimpleIocContainer();

            container.Register<ITypeToResolve, ConcreteType>();

            var instance = container.Resolve<ITypeToResolve>();

            Assert.That(container.Resolve<ITypeToResolve>(), Is.SameAs(instance));
        }

        [Test]
        public void can_create_transient_instance()
        {
            var container = new SimpleIocContainer();

            container.Register<ITypeToResolve, ConcreteType>(LifeCycle.Transient);

            var instance = container.Resolve<ITypeToResolve>();

            Assert.That(container.Resolve<ITypeToResolve>(), Is.Not.SameAs(instance));
        }
    }

    public interface ITypeToResolve
    {
    }

    public class ConcreteType : ITypeToResolve
    {
    }

    public interface ITypeToResolveWithConstructorParams
    {
    }

    public class ConcreteTypeWithConstructorParams : ITypeToResolveWithConstructorParams
    {
        public ConcreteTypeWithConstructorParams(ITypeToResolve typeToResolve)
        {
        }
    }
}