using jmpcoon.model.entities;

namespace jmpcoon.model.physics
{
    public class PlayerPhysicalBody : DynamicPhysicalBody
    {
        public int Lives { get; private set; }

        public PlayerPhysicalBody((double X, double Y) position, double angle, BodyShape shape,
                                  double width, double height) : base(position, angle, shape, width, height)
        {
            Lives = 1;
        }

        public override EntityState State => EntityState.IDLE;

        public void ApplyMovement(MovementType movementType, double impulseX, double impulseY)
        {
        }
    }
}