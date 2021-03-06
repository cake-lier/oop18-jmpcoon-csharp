using System;
using System.Collections.Generic;

namespace jmpcoon.model
{
    public interface IClassToInstanceMultimap<B>
    {
        void PutInstance<T>(Type type, T value) where T : B;

        IReadOnlyCollection<T> GetInstances<T>(Type type) where T : B;
    }
}
