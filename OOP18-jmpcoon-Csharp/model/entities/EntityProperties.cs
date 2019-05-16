using jmpcoon.model.physics;

namespace jmpcoon.model.entities
{
    public class EntityProperties : IEntityProperties
    {
        public EntityProperties(EntityType type, BodyShape shape, double xCoord, double yCoord, double width, double height,
                                double angle, PowerUpType? powerUpType, double? walkingRange)
        {
            Type = type;
            Shape = shape;
            Position = (X : xCoord, Y: yCoord);
            Dimensions = (Width: width, Height: height);
            Angle = angle;
            PowerUpType = powerUpType;
            WalkingRange = walkingRange;
        }

        public EntityType Type { get; }

        public BodyShape Shape { get; }

        public (double X, double Y) Position { get; }

        public (double Width, double Height) Dimensions { get; }

        public double Angle { get; }

        public PowerUpType? PowerUpType { get; }

        public double? WalkingRange { get; }

        public override bool Equals(object obj) => obj is EntityProperties properties 
                                                   && Type == properties.Type
                                                   && Shape == properties.Shape
                                                   && Position.Equals(properties.Position)
                                                   && Dimensions.Equals(properties.Dimensions)
                                                   && Angle.CompareTo(properties.Angle) == 0;

        public override int GetHashCode()
        {
            var hashCode = 1648768757;
            hashCode = hashCode * -1521134295 + Type.GetHashCode();
            hashCode = hashCode * -1521134295 + Shape.GetHashCode();
            hashCode = hashCode * -1521134295 + Position.GetHashCode();
            hashCode = hashCode * -1521134295 + Dimensions.GetHashCode();
            hashCode = hashCode * -1521134295 + Angle.GetHashCode();
            return hashCode;
        }

        public override string ToString() => "EntityPropertiesImpl [type=" + Type + ", shape=" + Shape + ", position=" + Position
                                             + ", dimensions=" + Dimensions + ", Angle=" + Angle + "]";
    }
}
