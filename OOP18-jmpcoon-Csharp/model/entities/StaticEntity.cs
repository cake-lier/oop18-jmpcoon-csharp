using jmpcoon.model.physics;

namespace jmpcoon.model.entities
{
    public abstract class StaticEntity : AbstractEntity
    {
        protected StaticEntity(StaticPhysicalBody body) : base(body)
        {
        }
    }
}
