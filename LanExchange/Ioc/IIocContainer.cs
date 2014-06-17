using System;

namespace LanExchange.Ioc
{
    public interface IIocContainer
    {
        void Register<TTypeToResolve, TConcrete>(LifeCycle lifeCycle = LifeCycle.Singleton);
        void Unregister<TTypeToResolve>();
        object Resolve(Type typeToResolve);
    }
}