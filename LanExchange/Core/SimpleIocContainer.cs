using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using LanExchange.Intf;

namespace LanExchange.Core
{
    public class SimpleIocContainer : IIoCContainer
    {
        private readonly IList<RegisteredObject> registeredObjects = new List<RegisteredObject>();

        public void Register<TTypeToResolve, TConcrete>()
        {
            Register<TTypeToResolve, TConcrete>(LifeCycle.Singleton);
        }

        public void Register<TTypeToResolve, TConcrete>(LifeCycle lifeCycle)
        {
            registeredObjects.Add(new RegisteredObject(typeof (TTypeToResolve), typeof (TConcrete), lifeCycle));
        }

        public TTypeToResolve Resolve<TTypeToResolve>()
        {
            return (TTypeToResolve) ResolveObject(typeof (TTypeToResolve));
        }

        public object Resolve(Type typeToResolve)
        {
            return ResolveObject(typeToResolve);
        }

        [Localizable(false)]
        private object ResolveObject(Type typeToResolve)
        {

            RegisteredObject registeredObject = null;
            foreach(var obj in registeredObjects)
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
            if (registeredObject.Instance == null || 
                registeredObject.LifeCycle == LifeCycle.Transient)
            {
                var list = new List<object>();
                foreach(var param in ResolveConstructorParameters(registeredObject))
                    list.Add(param);
                registeredObject.CreateInstance(list.ToArray());
            }
            return registeredObject.Instance;
        }

        private IEnumerable<object> ResolveConstructorParameters(RegisteredObject registeredObject)
        {
            var constructors =
                registeredObject.ConcreteType.GetConstructors();
            if (constructors.Length == 0)
                throw new NotImplementedException("Constructor is not implemented or non-public in class " + registeredObject.ConcreteType.Name);
            var constructorInfo = constructors[0];
            foreach(var parameter in constructorInfo.GetParameters())
                yield return ResolveObject(parameter.ParameterType);
        }
    }
}