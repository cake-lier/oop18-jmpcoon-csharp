using System;
using System.Linq;
using jmpcoon.model.world;

namespace jmpcoon.model.entities
{
    public abstract class AbstractEntityBuilder<E> where E : IEntity
    { 
        private const string INCOMPLETE_BUILDER_MSG = "Not all the fields have been initialized";
        private const string ALREADY_BUILT_MSG = "This builder has already been used";

        private Tuple<double, double> center;
        private Tuple<double, double> dimensions;
        private BodyShape? shape;
        private double? angle;
        private PowerUpType? powerUpType;
        private double? walkingRange;
        private IModifiableWorld world;
        private bool built;

        protected AbstractEntityBuilder()
        {
            center = null;
            dimensions = null;
            shape = null;
            angle = null;
            powerUpType = null;
            walkingRange = null;
            world = null;
            built = false;
        }

        public AbstractEntityBuilder<E> SetPosition(Tuple<double, double> center)
        {
            this.center = Tuple.Create(center.Item1, center.Item2);
            return this;
        }

        public AbstractEntityBuilder<E> SetDimensions(Tuple<double, double> dimensions)
        {
            this.dimensions = Tuple.Create(dimensions.Item1, dimensions.Item2);
            return this;
        }

        public AbstractEntityBuilder<E> SetShape(BodyShape shape)
        {
            this.shape = shape;
            return this;
        }

        public AbstractEntityBuilder<E> SetAngle(double angle)
        {
            this.angle = angle;
            return this;
        }

        public AbstractEntityBuilder<E> SetPowerUpType(PowerUpType? powerUpType)
        {
            this.powerUpType = powerUpType;
            return this;
        }

        public AbstractEntityBuilder<E> SetWalkingRange(double? walkingRange)
        {
            this.walkingRange = walkingRange;
            return this;
        }

        public AbstractEntityBuilder<E> SetWorld(IModifiableWorld world)
        {
            this.world = world;
            return this;
        }

        public E Build()
        {
            CheckIfBuildable();
            built = true;
            return BuildEntity();
        }

        protected abstract E BuildEntity();

        protected PowerUpType? GetPowerUpType() => powerUpType;

        protected double? GetWalkingRange() => walkingRange;

        protected IModifiableWorld GetWorld() => world;

        private void CheckIfBuildable()
        {
            CheckFieldsPresence(center, dimensions, shape, angle);
            if (built)
            {
                throw new InvalidOperationException(ALREADY_BUILT_MSG);
            }
        }

        private void CheckFieldsPresence(params object[] optionals)
        {
            if (!optionals.All(o => o != null))
            {
                throw new InvalidOperationException(INCOMPLETE_BUILDER_MSG);
            }
        }
    }
}
