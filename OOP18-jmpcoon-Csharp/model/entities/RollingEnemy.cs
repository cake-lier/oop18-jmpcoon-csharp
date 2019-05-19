using System;
using jmpcoon.model.physics;

namespace jmpcoon.model.entities
{
    [Serializable]
    public class RollingEnemy : DynamicEntity
    {
        private const double ROLLING_ENEMY_SPEED = 3;

        private readonly DynamicPhysicalBody body;

        public RollingEnemy(DynamicPhysicalBody body) : base(body) => this.body = body;

        public override EntityType Type => EntityType.ROLLING_ENEMY;

        public void ApplyImpulse() => body.SetFixedVelocity(MovementType.MOVE_RIGHT, ROLLING_ENEMY_SPEED, 0);
    }
}
