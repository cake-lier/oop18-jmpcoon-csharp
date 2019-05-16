using jmpcoon.model.entities;

namespace jmpcoon.model.physics
{
    public interface IPhysicalBody
    {
        (double X, double Y) Position { get; }

        double Angle { get; }

        EntityState State { get; }

        bool Exists { get; }

        BodyShape Shape { get; }

        (double Width, double Height) Dimensions { get; }

        (double X, double Y) Velocity { get; }
    }
}
