using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Collections.Extensions;

namespace jmpcoon.model
{
    public class ClassToInstanceMultimap<TUpper> : ForwardingMultimap<Type, TUpper>, IClassToInstanceMultimap<TUpper>
    {
        private const string NO_NULL = "A null key is not accepted!";
        private const string CAST_ERROR = "Wrong type has been inserted";

        private readonly MultiValueDictionary<Type, TUpper> backingMap;

        public ClassToInstanceMultimap() => backingMap = new MultiValueDictionary<Type, TUpper>();

        public ClassToInstanceMultimap(MultiValueDictionary<Type, TUpper> backingMap)
        {
            CheckMultimapEntries(backingMap.RequireNonNull());
            this.backingMap = backingMap;
        }

        public override void Add(Type key, TUpper value)
        {
            Cast(key.RequireNonNull(), value.RequireNonNull());
            base.Add(key, value);
        }

        public override void AddRange(Type key, IEnumerable<TUpper> values)
        {
            CheckIterableValues(key, values);
            base.AddRange(key, values);
        }

        public ICollection<TElem> GetInstances<TElem>(Type type) where TElem : TUpper
            => this[type.RequireNonNull()].Cast<TElem>().ToList();

        public void PutInstance<TElem>(Type type, TElem value) where TElem : TUpper => Add(type, value);

        protected override MultiValueDictionary<Type, TUpper> Delegate() => backingMap;

        private void CheckMultimapEntries(MultiValueDictionary<Type, TUpper> multimap)
            => multimap.SelectMany(list => list.Value.Select(value => new KeyValuePair<Type, TUpper>(list.Key, value)))
                       .AsParallel()
                       .ForAll(entry => Cast(entry.Key.RequireNonNull(), entry.Value.RequireNonNull()));

        private void CheckIterableValues(Type type, IEnumerable<TUpper> values)
            => values.RequireNonNull()
                     .AsParallel()
                     .ForAll(value => Cast(type.RequireNonNull(), value.RequireNonNull()));

        private void Cast(Type type, TUpper value) 
        {
            if (!typeof(TUpper).IsAssignableFrom(type) || !type.IsAssignableFrom(value.GetType()))
            {
                throw new InvalidCastException(CAST_ERROR);
            }
        }
    }
}
