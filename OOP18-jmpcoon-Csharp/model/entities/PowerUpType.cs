using jmpcoon.model.world;

namespace jmpcoon.model.entities
{
    public struct PowerUpType
    {
        public static readonly PowerUpType GOAL = new PowerUpType(CollisionEvent.GOAL_HIT);
        public static readonly PowerUpType EXTRA_LIFE = new PowerUpType(CollisionEvent.POWER_UP_HIT);
        public static readonly PowerUpType INVINCIBILITY = new PowerUpType(CollisionEvent.INVINCIBILITY_HIT);

        public CollisionEvent AssociatedEvent { get; }

        private PowerUpType(CollisionEvent associatedEvent) => AssociatedEvent = associatedEvent;
    }
}