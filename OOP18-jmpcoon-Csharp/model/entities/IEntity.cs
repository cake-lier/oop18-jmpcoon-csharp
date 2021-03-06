using jmpcoon.model.physics;

namespace jmpcoon.model.entities
{
    public interface IEntity
    {
        (double X, double Y) Position { get; }

        BodyShape Shape { get; }

        double Angle { get; }

        EntityType Type { get; }

        EntityState State { get; }

        bool Alive { get; }

        (double Width, double Height) Dimensions { get; }

        (double X, double Y) Velocity { get; }

        IPhysicalBody PhysicalBody { get; }
    }
}
