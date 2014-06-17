using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using LanExchange.Misc;

namespace LanExchange.Ioc
{
    public class SimpleIocContainer : IIocContainer
    {
        private readonly IList<RegisteredObject> m_Objects = new List<RegisteredObject>();

        [Localizable(false)]
        public void Register<TTypeToResolve, TConcrete>(LifeCycle lifeCycle = LifeCycle.Singleton)
        {
            m_Objects.Add(new RegisteredObject(typeof (TTypeToResolve), typeof (TConcrete), lifeCycle));
            Debug.Print("IoC: Registering type #{0} {1} : {2}", m_Objects.Count, typeof(TConcrete).FullName, typeof(TTypeToResolve).Name);
        }

        [Localizable(false)]
        public void Unregister<TTypeToResolve>()
        {
            for (int index = m_Objects.Count - 1; index >= 0; index--)
            {
                var obj = m_Objects[index];
                if (obj.TypeToResolve == typeof (TTypeToResolve))
                {
                    Debug.Print("IoC: Unregistering type {0}", obj.TypeToResolve.Name);
                    m_Objects.RemoveAt(index);
                }
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
                throw new TypeNotRegisteredException(string.Format(CultureInfo.InvariantCulture, 
                    "The type {0} has not been registered", typeToResolve.Name));
            Debug.Print("IoC: Resolving {0} : {1}", registeredObject.ConcreteType.Name, typeToResolve.Name);
            return GetInstance(registeredObject);
        }

        private object GetInstance(RegisteredObject registeredObject)
        {
            if (registeredObject.Instance == null || registeredObject.LifeCycle == LifeCycle.Transient)
                registeredObject.CreateInstance(ResolveConstructorParameters(registeredObject).ToArray());
            return registeredObject.Instance;
        }

        [Localizable(false)]
        private IEnumerable<object> ResolveConstructorParameters(RegisteredObject registeredObject)
        {
            var constructors = registeredObject.ConcreteType.GetConstructors();

            if (constructors.Length == 0)
                throw new ApplicationException("Constructor is not implemented or non-public in class " + registeredObject.ConcreteType.Name);

            var constructorInfo = constructors[0];
            var result = new List<object>();
            foreach(var parameter in constructorInfo.GetParameters())
                result.Add(Resolve(parameter.ParameterType));
            return result;
        }
    }
}