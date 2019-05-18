using jmpcoon.model.world;

namespace jmpcoon.model.entities
{
    public struct PowerUpType
    {
        public static readonly PowerUpType GOAL = new PowerUpType(CollisionEvent.GOAL_HIT);
        public static readonly PowerUpType EXTRA_LIFE = new PowerUpType(CollisionEvent.POWER_UP_HIT);
        public static readonly PowerUpType INVINCIBILITY = new PowerUpType(CollisionEvent.INVINCIBILITY_HIT);

        private PowerUpType(CollisionEvent associatedEvent) => AssociatedEvent = associatedEvent;

        public CollisionEvent AssociatedEvent { get; }

        public override bool Equals(object obj)
        {
            if (!(obj is PowerUpType))
            {
                return false;
            }
            var type = (PowerUpType)obj;
            return AssociatedEvent == type.AssociatedEvent;
        }

        public override int GetHashCode() => 798398441 + AssociatedEvent.GetHashCode();

        public static bool operator ==(PowerUpType firstType, PowerUpType secondType) => firstType.Equals(secondType);

        public static bool operator !=(PowerUpType firstType, PowerUpType secondType) => !(firstType == secondType);
    }
}