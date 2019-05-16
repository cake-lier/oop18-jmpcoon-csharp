using jmpcoon.model.entities;

namespace jmpcoon.model.physics
{
    public abstract class AbstractPhysicalBody : IPhysicalBody
    {
        protected AbstractPhysicalBody((double X, double Y) position, double angle, BodyShape shape,
                                       (double Width, double Height) dimensions)
        {
            Position = position;
            Angle = angle;
            Shape = shape;
            Exists = true;
            Velocity = (X: 0.0, Y: 0.0);
            Dimensions = dimensions;
        }

        public (double X, double Y) Position { get; }

        public double Angle { get; }

        public BodyShape Shape { get; }

        public abstract EntityState State { get; }

        public bool Exists { get; }

        public (double X, double Y) Velocity { get; }

        public (double Width, double Height) Dimensions { get; }

        public override bool Equals(object obj) => obj is AbstractPhysicalBody body
                                                   && Position.Equals(body.Position)
                                                   && Angle.CompareTo(body.Angle) == 0
                                                   && Shape == body.Shape
                                                   && State == body.State
                                                   && Exists == body.Exists
                                                   && Dimensions.Equals(body.Dimensions);

        public override int GetHashCode()
        {
            var hashCode = 258616307;
            hashCode = hashCode * -1521134295 + Position.GetHashCode();
            hashCode = hashCode * -1521134295 + Angle.GetHashCode();
            hashCode = hashCode * -1521134295 + Shape.GetHashCode();
            hashCode = hashCode * -1521134295 + State.GetHashCode();
            hashCode = hashCode * -1521134295 + Exists.GetHashCode();
            hashCode = hashCode * -1521134295 + Dimensions.GetHashCode();
            return hashCode;
        }

        public override string ToString() => "Position: (" + Position.X + ", " + Position.Y + "); Dimensions: " + Dimensions.Width
                                             + "x" + Dimensions.Height + "; Angle: " + Angle;
    }
}
