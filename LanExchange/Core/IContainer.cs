using System;

namespace LanExchange.Core
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