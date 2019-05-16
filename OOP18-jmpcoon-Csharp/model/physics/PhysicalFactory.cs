using jmpcoon.model.entities;
using jmpcoon.model.world;

namespace jmpcoon.model.physics
{
    public class PhysicalFactory : IPhysicalFactory
    {
        public IUpdatablePhysicalWorld CreatePhysicalWorld(INotifiableWorld outerWorld, double width, double height)
            => new PhysicalWorld();

        public StaticPhysicalBody CreateStaticPhysicalBody((double X, double Y) position, double angle, BodyShape shape,
                                                           double width, double height, EntityType type, PowerUpType? powerUpType)
            => new StaticPhysicalBody(position, angle, shape, width, height, type);

        public DynamicPhysicalBody CreateDynamicPhysicalBody((double X, double Y) position, double angle, BodyShape shape,
                                                     double width, double height, EntityType type)
            => new DynamicPhysicalBody(position, angle, shape, width, height);

        public PlayerPhysicalBody CreatePlayerPhysicalBody((double X, double y) position, double angle, BodyShape shape,
                                                           double width, double height)
            => new PlayerPhysicalBody(position, angle, shape, width, height);
    }
}
