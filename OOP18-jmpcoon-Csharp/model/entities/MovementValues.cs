using System.Collections.Generic;

namespace jmpcoon.model.entities
{
    public struct MovementValues
    {
        public static readonly MovementValues CLIMB_DOWN = new MovementValues(MovementType.CLIMB_DOWN, 0, -0.6);
        public static readonly MovementValues CLIMB_UP = new MovementValues(MovementType.CLIMB_UP, 0, 0.6);
        public static readonly MovementValues JUMP = new MovementValues(MovementType.JUMP, 0, 11);
        public static readonly MovementValues MOVE_RIGHT = new MovementValues(MovementType.MOVE_RIGHT, 1.1, 0);
        public static readonly MovementValues MOVE_LEFT = new MovementValues(MovementType.MOVE_LEFT, -1.1, 0);

        private MovementValues(MovementType movementType, double impulseX, double impulseY)
        {
            MovementType = movementType;
            Impulse = (X: impulseX, Y: impulseY);
        }

        public MovementType MovementType { get; }

        public (double X, double Y) Impulse { get; }

        public static IList<MovementValues> Values() => new List<MovementValues> { CLIMB_DOWN, CLIMB_UP, JUMP, MOVE_RIGHT,
                                                                                   MOVE_LEFT };
       
        public override bool Equals(object obj)
        {
            if (!(obj is MovementValues))
            {
                return false;
            }
            var values = (MovementValues)obj;
            return EqualityComparer<MovementType>.Default.Equals(MovementType, values.MovementType)
                   && Impulse.Equals(values.Impulse);
        }

        public override int GetHashCode()
        {
            var hashCode = 1006734852;
            hashCode = hashCode * -1521134295 + EqualityComparer<MovementType>.Default.GetHashCode(MovementType);
            hashCode = hashCode * -1521134295 + Impulse.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(MovementValues firstValue, MovementValues secondValue) => firstValue.Equals(secondValue);

        public static bool operator !=(MovementValues firstValue, MovementValues secondValue) => !(firstValue == secondValue);
    }
}
