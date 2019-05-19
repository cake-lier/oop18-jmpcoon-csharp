using System;
using System.Linq;
using jmpcoon.model.physics;

namespace jmpcoon.model.entities
{
    [Serializable]
    public class Player : DynamicEntity
    {
        private readonly PlayerPhysicalBody body;

        public Player(PlayerPhysicalBody body) : base(body)
        {
            this.body = body;
        }

        public override EntityType Type => EntityType.PLAYER;

        public int Lives => body.Lives;

        public void Move(MovementType movement)
        {
            var moveValues = MovementValues.Values()
                                           .First(mValue => mValue.MovementType == movement);
            body.ApplyMovement(moveValues.MovementType, moveValues.Impulse.X, moveValues.Impulse.Y);
        }
    }
}
