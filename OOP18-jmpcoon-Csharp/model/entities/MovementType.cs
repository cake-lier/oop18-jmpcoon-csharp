namespace jmpcoon.model.entities
{
    public struct MovementType
    {
        public static readonly MovementType CLIMB_DOWN = new MovementType(EntityState.CLIMBING_DOWN);
        public static readonly MovementType CLIMB_UP = new MovementType(EntityState.CLIMBING_UP);
        public static readonly MovementType JUMP = new MovementType(EntityState.JUMPING);
        public static readonly MovementType MOVE_RIGHT = new MovementType(EntityState.MOVING_RIGHT);
        public static readonly MovementType MOVE_LEFT = new MovementType(EntityState.MOVING_LEFT);

        private MovementType(EntityState state) => CorrespondingState = state;

        public EntityState CorrespondingState { get; }

        public override bool Equals(object obj)
        {
            if (!(obj is MovementType))
            {
                return false;
            }

            var type = (MovementType)obj;
            return CorrespondingState == type.CorrespondingState;
        }

        public override int GetHashCode() => 741771041 + CorrespondingState.GetHashCode();

        public static bool operator ==(MovementType firstType, MovementType secondType) => firstType.Equals(secondType);

        public static bool operator !=(MovementType firstType, MovementType secondType) => !(firstType == secondType);
    }
}
