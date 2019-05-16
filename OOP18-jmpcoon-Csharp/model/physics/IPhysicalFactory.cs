using jmpcoon.model.entities;
using jmpcoon.model.world;

namespace jmpcoon.model.physics
{
    public interface IPhysicalFactory
    {
        IUpdatablePhysicalWorld CreatePhysicalWorld(INotifiableWorld outerWorld, double width, double height);

        StaticPhysicalBody CreateStaticPhysicalBody((double X, double Y) position, double angle, BodyShape shape, double width,
                                                    double height, EntityType type, PowerUpType? powerUpType);

        DynamicPhysicalBody CreateDynamicPhysicalBody((double X, double Y) position, double angle, BodyShape shape,
                                                      double width, double height, EntityType type);

        PlayerPhysicalBody CreatePlayerPhysicalBody((double X, double y) position, double angle, BodyShape shape,
                                                    double width, double height);
    }
}
