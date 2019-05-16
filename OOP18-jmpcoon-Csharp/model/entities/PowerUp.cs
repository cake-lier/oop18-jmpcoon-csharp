using jmpcoon.model.physics;

namespace jmpcoon.model.entities
{
    public class PowerUp : StaticEntity
    {
        public PowerUp(StaticPhysicalBody body, PowerUpType type) : base(body)
        {
            PowerUpType = type;
        }

        public PowerUpType PowerUpType { get; }

        public override EntityType Type => EntityType.POWERUP;
    }
}
