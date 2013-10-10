using System;

namespace LanTabs
{
    public class RegisteredObject
    {
        public RegisteredObject(Type typeToResolve, Type concreteType, LifeCycle lifeCycle)
        {
            TypeToResolve = typeToResolve;
            ConcreteType = concreteType;
            LifeCycle = lifeCycle;
        }

        public Type TypeToResolve { get; private set; }

        public Type ConcreteType { get; private set; }

        public object Instance { get; private set; }

        public LifeCycle LifeCycle { get; private set; }

        public void CreateInstance(params object[] args)
        {
            Instance = Activator.CreateInstance(ConcreteType, args);
        }
    }
}