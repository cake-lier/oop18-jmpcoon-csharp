namespace jmpcoon.model.world
{
    public interface INotifiableWorld
    {
        void NotifyCollision(CollisionEvent collisionType);
    }
}
