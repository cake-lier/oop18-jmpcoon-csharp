namespace jmpcoon.model.entities
{
    public class PowerUp : IEntity
    {
        public PowerUp(PowerUpType type)
        {
        }

        public PowerUpType? PowerUpType { get; internal set; }
    }
}
