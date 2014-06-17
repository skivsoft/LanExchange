using System;

namespace LanExchange.Ioc
{
    public class RegisteredObject
    {
        private readonly Type m_TypeToResolve;
        private readonly Type m_ConcreteType;
        private readonly LifeCycle m_LifeCycle;
        private object m_Instance;

        public RegisteredObject(Type typeToResolve, Type concreteType, LifeCycle lifeCycle)
        {
            m_TypeToResolve = typeToResolve;
            m_ConcreteType = concreteType;
            m_LifeCycle = lifeCycle;
        }

        public Type TypeToResolve
        {
            get { return m_TypeToResolve; }
        }

        public Type ConcreteType
        {
            get { return m_ConcreteType; }
        }

        public LifeCycle LifeCycle
        {
            get { return m_LifeCycle; }
        }

        public object Instance
        {
            get { return m_Instance; }
        }

        public void CreateInstance(params object[] args)
        {
            m_Instance = Activator.CreateInstance(ConcreteType, args);
        }
    }
}