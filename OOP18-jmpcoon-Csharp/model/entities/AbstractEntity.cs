using System.Collections.Generic;
using jmpcoon.model.physics;

namespace jmpcoon.model.entities
{
    public abstract class AbstractEntity : IEntity
    {
        protected AbstractEntity(IPhysicalBody body) => PhysicalBody = body.RequireNonNull();

        public (double X, double Y) Position => PhysicalBody.Position;

        public BodyShape Shape => PhysicalBody.Shape;

        public double Angle => PhysicalBody.Angle;

        public abstract EntityType Type { get; }

        public EntityState State => PhysicalBody.State;

        public bool Alive => PhysicalBody.Exists;

        public (double Width, double Height) Dimensions => PhysicalBody.Dimensions;

        public (double X, double Y) Velocity => PhysicalBody.Velocity;

        public IPhysicalBody PhysicalBody { get; }

        public override string ToString() => "Type: " + Type + "; Shape: " + Shape + "; Position: (" + Position.X + ", "
                                             + Position.Y + "); Dimensions: " + Dimensions.Width + "x" + Dimensions.Height
                                             + "; Angle: " + Angle;

        public override bool Equals(object obj)
            => obj is AbstractEntity entity
               && EqualityComparer<IPhysicalBody>.Default.Equals(PhysicalBody, entity.PhysicalBody);

        public override int GetHashCode() => 169287901 + EqualityComparer<IPhysicalBody>.Default.GetHashCode(PhysicalBody);
    }
}
