using jmpcoon.model.physics;

namespace jmpcoon.model.entities
{
    public class Ladder : StaticEntity
    {
        public Ladder(StaticPhysicalBody body) : base(body)
        {
        }

        public override EntityType Type => EntityType.LADDER;
    }
}
