using System;

namespace jmpcoon.model.entities
{
    public interface IUnmodifiableEntity
    {
        Tuple<double, double> Position { get; }

        BodyShape Shape { get; }

        double Angle { get; }

        EntityType Type { get; }

        EntityState State { get; }

        Tuple<double, double> Dimensions { get; }

        bool Dynamic { get; }

        Tuple<double, double> Velocity { get; }

        PowerUpType? PowerUpType { get; }
    }
}
