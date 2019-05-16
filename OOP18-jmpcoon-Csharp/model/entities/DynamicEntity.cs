using jmpcoon.model.physics;

namespace jmpcoon.model.entities
{
    public abstract class DynamicEntity : AbstractEntity
    {
        protected DynamicEntity(DynamicPhysicalBody body) : base(body)
        {
        }
    }
}
