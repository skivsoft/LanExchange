using System;

namespace LanExchange.Intf
{
    public interface IIoCContainer
    {
        void Register<TTypeToResolve, TConcrete>(LifeCycle lifeCycle = LifeCycle.Singleton);
        object Resolve(Type typeToResolve);
    }
}