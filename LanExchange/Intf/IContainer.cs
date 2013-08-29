using System;

namespace LanExchange.Intf
{
    public interface IContainer
    {
        void Register<TTypeToResolve, TConcrete>();
        void Register<TTypeToResolve, TConcrete>(LifeCycle lifeCycle);
        TTypeToResolve Resolve<TTypeToResolve>();
        object Resolve(Type typeToResolve);
    }

    public enum LifeCycle
    {
        Singleton,
        Transient
    }
}