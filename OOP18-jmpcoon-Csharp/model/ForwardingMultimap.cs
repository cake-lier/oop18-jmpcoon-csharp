using System;
using System.Collections.Generic;
using Microsoft.Collections.Extensions;

namespace jmpcoon.model
{
    public abstract class ForwardingMultimap<K, V> : MultiValueDictionary<K, V>
    {
        protected ForwardingMultimap()
        {
        }

        public new IReadOnlyCollection<V> this[K key] => Delegate()[key];

        public new virtual void Add(K key, V value) => Delegate().Add(key, value);

        public new virtual void AddRange(K key, IEnumerable<V> values) => Delegate().AddRange(key, values);

        public new virtual void Clear() => Delegate().Clear();

        public new virtual bool Contains(K key, V value) => Delegate().Contains(key, value);

        public new virtual bool ContainsKey(K key) => Delegate().ContainsKey(key);

        public new virtual bool ContainsValue(V value) => Delegate().ContainsValue(value);

        public new virtual IEnumerator<KeyValuePair<K, IReadOnlyCollection<V>>> GetEnumerator() => Delegate().GetEnumerator();

        public new virtual bool Remove(K key, V value) => Delegate().Remove(key, value);

        public new virtual bool Remove(K key) => Delegate().Remove(key);

        public new virtual bool TryGetValue(K key, out IReadOnlyCollection<V> value) => Delegate().TryGetValue(key, out value);

        public new virtual int Count => Delegate().Count;

        public new virtual IEnumerable<K> Keys => Delegate().Keys;

        public new virtual IEnumerable<IReadOnlyCollection<V>> Values => Delegate().Values;

        public override bool Equals(object obj) => Delegate().Equals(obj);

        public override int GetHashCode() => Delegate().GetHashCode();

        public override string ToString() => Delegate().ToString();

        public new virtual Type GetType() => Delegate().GetType();

        protected abstract MultiValueDictionary<K, V> Delegate();
    }
}
