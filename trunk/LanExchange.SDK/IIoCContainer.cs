using System;

namespace LanExchange.SDK
{
    public interface IIoCContainer
    {
        void Register<TTypeToResolve, TConcrete>(LifeCycle lifeCycle = LifeCycle.Singleton);
        void Unregister<TTypeToResolve>();
        object Resolve(Type typeToResolve);
    }
}