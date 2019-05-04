using System;
using System.Collections.Generic;

namespace jmpcoon.model.entities
{
    public class EntityProperties : IEntityProperties
    {
        public EntityProperties(EntityType type, BodyShape shape, double xCoord, double yCoord, double width, double height,
                                double angle, PowerUpType? powerUpType, double? walkingRange)
        {
            Type = type;
            Shape = shape;
            Position = Tuple.Create(xCoord, yCoord);
            Dimensions = Tuple.Create(width, height);
            Angle = angle;
            PowerUpType = powerUpType;
            WalkingRange = walkingRange;
        }

        public EntityType Type { get; }

        public BodyShape Shape { get; }

        public Tuple<double, double> Position { get; }

        public Tuple<double, double> Dimensions { get; }

        public double Angle { get; }

        public PowerUpType? PowerUpType { get; }

        public double? WalkingRange { get; }

        public override bool Equals(object obj) => Equals(obj as EntityProperties);

        public bool Equals(EntityProperties other) 
            => other != null && Type == other.Type && Shape == other.Shape && Math.Abs(Angle - other.Angle) < double.Epsilon
               && EqualityComparer<Tuple<double, double>>.Default.Equals(Position, other.Position)
               && EqualityComparer<Tuple<double, double>>.Default.Equals(Dimensions, other.Dimensions);

        public override int GetHashCode()
        {
            int hashCode = 1648768757;
            hashCode = hashCode * -1521134295 + Type.GetHashCode();
            hashCode = hashCode * -1521134295 + Shape.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<Tuple<double, double>>.Default.GetHashCode(Position);
            hashCode = hashCode * -1521134295 + EqualityComparer<Tuple<double, double>>.Default.GetHashCode(Dimensions);
            hashCode = hashCode * -1521134295 + Angle.GetHashCode();
            return hashCode;
        }

        public override string ToString() => "EntityPropertiesImpl [type=" + Type + ", shape=" + Shape + ", position=" + Position
                                             + ", dimensions=" + Dimensions + ", Angle=" + Angle + "]";
    }
}
