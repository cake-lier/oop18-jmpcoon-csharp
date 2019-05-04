using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Collections.Extensions;

namespace jmpcoon.model
{
    public class ClassToInstanceMultimap<B> : ForwardingMultimap<Type, B>, IClassToInstanceMultimap<B>
    {
        private const string CAST_ERROR = "Wrong type has been inserted";

        private readonly MultiValueDictionary<Type, B> backingMap;

        public ClassToInstanceMultimap() => backingMap = new MultiValueDictionary<Type, B>();

        public ClassToInstanceMultimap(MultiValueDictionary<Type, B> multimap)
        {
            multimap.AsParallel().ForAll(pair => CheckCollection(pair.Key, pair.Value));
            backingMap = multimap;
        }

        public override void Add(Type key, B value)
        {
            CheckType(key);
            CheckCouple(key, value);
            base.Add(key, value);
        }

        public override void AddRange(Type key, IEnumerable<B> values)
        {
            CheckCollection(key, values);
            base.AddRange(key, values);
        }

        public ICollection<T> GetInstances<T>(Type type) where T : B
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

        protected override MultiValueDictionary<Type, B> Delegate() => backingMap;

        private void CheckCastValidity(bool condition)
        {
            if (!condition)
            {
                throw new InvalidCastException(CAST_ERROR);
            }
        }

        private void CheckType(Type key) => CheckCastValidity(typeof(B).IsAssignableFrom(key));

        private void CheckCouple<T>(Type key, T value) where T : B => CheckCastValidity(key.IsAssignableFrom(value.GetType()));

        private void CheckCollection<T>(Type key, IEnumerable<T> value) where T : B => value.AsParallel()
                                                                                            .ForAll(el =>
                                                                                                        {
                                                                                                            CheckType(key);
                                                                                                            CheckCouple(key, el);
                                                                                                        });
    }
}
