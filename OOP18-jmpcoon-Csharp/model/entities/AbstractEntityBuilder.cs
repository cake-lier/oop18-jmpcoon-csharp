using System;
using System.Linq;
using jmpcoon.model.physics;
using jmpcoon.model.world;

namespace jmpcoon.model.entities
{
    public abstract class AbstractEntityBuilder<TEntity> : IEntityBuilder<TEntity> where TEntity : IEntity
    { 
        private const string INCOMPLETE_BUILDER_MSG = "Not all the fields have been initialized";
        private const string ALREADY_BUILT_MSG = "This builder has already been used";

        private (double X, double Y)? center;
        private (double Width, double Height)? dimensions;
        private BodyShape? shape;
        private double? angle;
        private PowerUpType? powerUpType;
        private double? walkingRange;
        private IModifiableWorld world;
        private IPhysicalFactory factory;
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

        public IEntityBuilder<TEntity> SetPosition((double X, double Y) center)
        {
            this.center = (center.X, center.Y);
            return this;
        }

        public IEntityBuilder<TEntity> SetDimensions((double Width, double Height) dimensions)
        {
            this.dimensions = (dimensions.Width, dimensions.Height);
            return this;
        }

        public IEntityBuilder<TEntity> SetShape(BodyShape shape)
        {
            this.shape = shape;
            return this;
        }

        public IEntityBuilder<TEntity> SetAngle(double angle)
        {
            this.angle = angle;
            return this;
        }

        public IEntityBuilder<TEntity> SetFactory(IPhysicalFactory factory)
        {
            this.factory = factory;
            return this;
        }

        public IEntityBuilder<TEntity> SetPowerUpType(PowerUpType? powerUpType)
        {
            this.powerUpType = powerUpType;
            return this;
        }

        public IEntityBuilder<TEntity> SetWalkingRange(double? walkingRange)
        {
            this.walkingRange = walkingRange;
            return this;
        }

        public IEntityBuilder<TEntity> SetWorld(IModifiableWorld world)
        {
            this.world = world;
            return this;
        }

        public TEntity Build()
        {
            CheckIfBuildable();
            built = true;
            return BuildEntity();
        }

        protected abstract TEntity BuildEntity();

        protected PowerUpType? GetPowerUpType() => powerUpType;

        protected double? GetWalkingRange() => walkingRange;

        protected IModifiableWorld GetWorld() => world;

        protected IPhysicalFactory GetPhysicalFactory() => factory;

        protected StaticPhysicalBody CreateStaticPhysicalBody(EntityType type)
            => factory.CreateStaticPhysicalBody(center.Value, angle.Value, shape.Value, dimensions.Value.Width, dimensions.Value.Height,
                                                type, powerUpType);

        protected DynamicPhysicalBody CreateDynamicPhysicalBody(EntityType type)
            => factory.CreateDynamicPhysicalBody(center.Value, angle.Value, shape.Value, dimensions.Value.Width,
                                                 dimensions.Value.Height, type);

        protected PlayerPhysicalBody CreatePlayerPhysicalBody()
            => factory.CreatePlayerPhysicalBody(center.Value, angle.Value, shape.Value, dimensions.Value.Width,
                                                dimensions.Value.Height);

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
