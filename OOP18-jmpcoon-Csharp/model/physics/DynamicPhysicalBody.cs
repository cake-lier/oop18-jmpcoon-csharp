using jmpcoon.model.entities;

namespace jmpcoon.model.physics
{
    public class DynamicPhysicalBody : AbstractPhysicalBody
    {
        public DynamicPhysicalBody((double X, double Y) position, double angle, BodyShape shape,
                                   double width, double height) : base(position, angle, shape, (Width: width, Height: height))
        {
        }

        public override EntityState State => EntityState.IDLE;

        public void SetFixedVelocity(MovementType movement, double x, double y)
        {
        }
    }
}
