using System;
using jmpcoon.model.physics;

namespace jmpcoon.model.entities
{
    [Serializable]
    public class WalkingEnemy : DynamicEntity
    {
        private const double WALKING_SPEED = 0.4;

        private MovementType currentMovement;
        private (double X, double Y) extremePosition;
        private readonly double walkingRange;
        private readonly DynamicPhysicalBody body;

        public WalkingEnemy(DynamicPhysicalBody body, double walkingRange) : base(body)
        {
            this.body = body;
            this.walkingRange = walkingRange;
            currentMovement = MovementType.MOVE_RIGHT;
            extremePosition = (body.Position.X, body.Position.Y);
        }

        public override EntityType Type => EntityType.WALKING_ENEMY;

        public void ComputeMovement()
        {
            if (!CheckDistanceFromExtreme())
            {
                extremePosition.X = body.Position.X;
                extremePosition.Y = body.Position.Y;
                currentMovement = GetOppositeMovement();
            }
            body.SetFixedVelocity(currentMovement, GetDelta() * WALKING_SPEED, 0);
        }

        private MovementType GetOppositeMovement() => currentMovement == MovementType.MOVE_RIGHT ? MovementType.MOVE_LEFT 
                                                      : MovementType.MOVE_RIGHT;

        private int GetDelta() => currentMovement == MovementType.MOVE_RIGHT ? 1 : -1;

        private bool CheckDistanceFromExtreme()
        {
            var (X, Y) = body.Position;
            return Math.Sqrt(Math.Pow(X - extremePosition.X, 2) + Math.Pow(Y - extremePosition.Y, 2)) < walkingRange;
        }
    }
}
