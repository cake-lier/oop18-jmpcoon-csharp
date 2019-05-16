using jmpcoon.model.physics;

namespace jmpcoon.model.entities
{ 
public class Platform : StaticEntity
    {
        public Platform(StaticPhysicalBody body) : base(body)
        {
        }

        public override EntityType Type => EntityType.PLATFORM;
    }
}
