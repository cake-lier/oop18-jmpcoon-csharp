using System;

namespace jmpcoon.model.entities
{
    public interface IEntityProperties
    {
        EntityType Type { get; }

        BodyShape Shape { get; }

        Tuple<double, double> Position { get; }

        Tuple<double, double> Dimensions { get; }

        double Angle { get; }

        PowerUpType? PowerUpType { get; }

        double? WalkingRange { get; }
    }
}
