using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Collections.Extensions;

namespace OOP18jmpcoonCsharp
{
    public class ClassToInstanceMultimap<B> : MultiValueDictionary<Type, B>
    {
        private readonly string CAST_ERROR = "Wrong type has been inserted!";

        private readonly MultiValueDictionary<Type, B> backingMap;

        public ClassToInstanceMultimap()
        {
            backingMap = new MultiValueDictionary<Type, B>();
        }

        public ClassToInstanceMultimap(MultiValueDictionary<Type, B> multimap)
        {
            multimap.AsParallel().ForAll(pair => CheckCollection(pair.Key, pair.Value));
            backingMap = multimap;
        }

        new public void Add(Type key, B value)
        {
            CheckType(key);
            CheckCouple(key, value);
            backingMap.Add(key, value);
        }

        new public void AddRange(Type key, IEnumerable<B> values)
        {
            CheckCollection(key, values);
            backingMap.AddRange(key, values);
        }

        new public void Remove(Type key)
        {
            CheckType(key);
            backingMap.Remove(key);
        }

        public IReadOnlyCollection<T> GetInstances<T>(Type type) where T : B
        {
            CheckType(type);
            return backingMap[type].Cast<T>().ToList();
        }

        public void PutInstance<T>(Type type, T value) where T : B
        {
            CheckType(type);
            CheckCouple(type, value);
            backingMap.Add(type, value);
        }

        private void CheckCastValidity(bool condition)
        {
            if (!condition)
            {
                throw new InvalidCastException(CAST_ERROR);
            }
        }

        private void CheckType(Type key) => CheckCastValidity(key.IsAssignableFrom(typeof(B)));

        private void CheckCouple<T>(Type key, T value) where T : B => CheckCastValidity(value.GetType().IsSubclassOf(key));

        private void CheckCollection<T>(Type key, IEnumerable<T> value) where T : B => value.AsParallel()
                                                                                            .ForAll(e => 
                                                                                                {
                                                                                                    CheckType(key);
                                                                                                    CheckCouple(key, e);
                                                                                                });
    }
}
