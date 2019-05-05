﻿using System;

namespace jmpcoon.model.entities
{
    public interface IEntity
    {
        Tuple<double, double> Position { get; }

        BodyShape Shape { get; }

        double Angle { get; }

        EntityType Type { get; }

        EntityState State { get; }

        bool Alive { get; }

        Tuple<double, double> Dimensions { get; }

        Tuple<double, double> Velocity { get; }
    }
}
