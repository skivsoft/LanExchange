using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using LanExchange.SDK;

namespace LanExchange.Misc
{
    public class SimpleIocContainer : IIoCContainer
    {
        private readonly IList<RegisteredObject> m_Objects = new List<RegisteredObject>();

        public void Register<TTypeToResolve, TConcrete>(LifeCycle lifeCycle = LifeCycle.Singleton)
        {
            m_Objects.Add(new RegisteredObject(typeof (TTypeToResolve), typeof (TConcrete), lifeCycle));
        }

        public void Unregister<TTypeToResolve>()
        {
            for (int index = m_Objects.Count - 1; index >= 0; index--)
            {
                var obj = m_Objects[index];
                if (obj.TypeToResolve == typeof(TTypeToResolve))
                    m_Objects.RemoveAt(index);
            }
        }

        [Localizable(false)]
        public object Resolve(Type typeToResolve)
        {
            RegisteredObject registeredObject = null;
            foreach(var obj in m_Objects)
                if (obj.TypeToResolve == typeToResolve)
                {
                    registeredObject = obj;
                    break;
                }
            if (registeredObject == null)
            {
                throw new TypeNotRegisteredException(string.Format(CultureInfo.InvariantCulture, 
                    "The type {0} has not been registered", typeToResolve.Name));
            }
            return GetInstance(registeredObject);
        }

        private object GetInstance(RegisteredObject registeredObject)
        {
            if (registeredObject.Instance == null || registeredObject.LifeCycle == LifeCycle.Transient)
            {
                var list = new List<object>();
                foreach(var param in ResolveConstructorParameters(registeredObject))
                    list.Add(param);
                registeredObject.CreateInstance(list.ToArray());
            }
            return registeredObject.Instance;
        }

        [Localizable(false)]
        private IEnumerable<object> ResolveConstructorParameters(RegisteredObject registeredObject)
        {
            var constructors =
                registeredObject.ConcreteType.GetConstructors();
            if (constructors.Length == 0)
                throw new NotImplementedException("Constructor is not implemented or non-public in class " + registeredObject.ConcreteType.Name);
            var constructorInfo = constructors[0];
            foreach(var parameter in constructorInfo.GetParameters())
                yield return Resolve(parameter.ParameterType);
        }
    }
}