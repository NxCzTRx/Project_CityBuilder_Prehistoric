using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Scripts.Core
{
    public class ObjectResolver
    {
        private readonly HashSet<Type> _registrations = new();
        private readonly Dictionary<Type, object> _instancePerType = new();
        
        public void Register<T>()
        {
            _registrations.Add(typeof(T));
        }
        
        public void RegisterInstance<T>(T instance)
        {
            _instancePerType[typeof(T)] = instance;
            _registrations.Add(instance.GetType());
        }
    
        public void UnregisterInstance<T>()
        {
            _instancePerType.Remove(typeof(T));
            _registrations.Remove(typeof(T));
        }

        public T Resolve<T>()
        {
            return (T)Resolve(typeof(T));
        }
        
        private object Resolve(Type instanceType)
        {
            if (_instancePerType.TryGetValue(instanceType, out var instance))
            {
                return instance;
            }

            if (!_registrations.Contains(instanceType))
            {
                Debug.LogError($"Couldn't resolve instance of type {instanceType}");
                return default;   
            }

            var constructor = instanceType.GetConstructors().First();
            var args = constructor.GetParameters().Select(p => Resolve(p.ParameterType)).ToArray();
            instance = Activator.CreateInstance(instanceType, args);
            
            _instancePerType[instanceType] = instance;
            return instance;
        }
        
        public void Clear()
        {
            _instancePerType.Clear();
            _registrations.Clear();
        }
    }
}
