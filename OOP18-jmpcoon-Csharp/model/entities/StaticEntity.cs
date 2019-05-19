using System;
using jmpcoon.model.physics;

namespace jmpcoon.model.entities
{
    [Serializable]
    public abstract class StaticEntity : AbstractEntity
    {
        protected StaticEntity(StaticPhysicalBody body) : base(body)
        {
        }
    }
}
