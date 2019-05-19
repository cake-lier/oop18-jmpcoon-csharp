using System;
using jmpcoon.model.physics;

namespace jmpcoon.model.entities
{
    [Serializable]
    public abstract class DynamicEntity : AbstractEntity
    {
        protected DynamicEntity(DynamicPhysicalBody body) : base(body)
        {
        }
    }
}
