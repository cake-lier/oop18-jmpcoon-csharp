using jmpcoon.model.physics;

namespace jmpcoon.model.entities
{
    public interface IUnmodifiableEntity
    {
        (double X, double Y) Position { get; }

        BodyShape Shape { get; }

        double Angle { get; }

        EntityType Type { get; }

        EntityState State { get; }

        (double Width, double Height) Dimensions { get; }

        bool Dynamic { get; }

        (double X, double Y) Velocity { get; }

        PowerUpType? PowerUpType { get; }
    }
}
