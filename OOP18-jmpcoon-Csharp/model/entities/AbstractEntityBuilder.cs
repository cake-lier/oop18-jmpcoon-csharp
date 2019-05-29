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

        private bool built;

        protected AbstractEntityBuilder()
        {
            Center = null;
            Dimensions = null;
            Shape = null;
            Angle = null;
            PowerUpType = null;
            WalkingRange = null;
            World = null;
            Factory = null;
            built = false;
        }

        private (double X, double Y)? Center { get; set; }

        private (double Width, double Height)? Dimensions { get; set; }

        private BodyShape? Shape { get; set; }

        private double? Angle { get; set; }

        protected PowerUpType? PowerUpType { get; private set; }

        protected double? WalkingRange { get; private set; }

        protected IModifiableWorld World { get; private set; }

        protected IPhysicalFactory Factory { get; private set; }

        public IEntityBuilder<TEntity> SetPosition((double X, double Y) center)
        {
            Center = (center.X, center.Y);
            return this;
        }

        public IEntityBuilder<TEntity> SetDimensions((double Width, double Height) dimensions)
        {
            Dimensions = (dimensions.Width, dimensions.Height);
            return this;
        }

        public IEntityBuilder<TEntity> SetShape(BodyShape shape)
        {
            Shape = shape;
            return this;
        }

        public IEntityBuilder<TEntity> SetAngle(double angle)
        {
            Angle = angle;
            return this;
        }

        public IEntityBuilder<TEntity> SetFactory(IPhysicalFactory factory)
        {
            Factory = factory;
            return this;
        }

        public IEntityBuilder<TEntity> SetPowerUpType(PowerUpType? powerUpType)
        {
            PowerUpType = powerUpType;
            return this;
        }

        public IEntityBuilder<TEntity> SetWalkingRange(double? walkingRange)
        {
            WalkingRange = walkingRange;
            return this;
        }

        public IEntityBuilder<TEntity> SetWorld(IModifiableWorld world)
        {
            World = world;
            return this;
        }

        public TEntity Build()
        {
            CheckIfBuildable();
            built = true;
            return BuildEntity();
        }

        protected abstract TEntity BuildEntity();

        protected StaticPhysicalBody CreateStaticPhysicalBody(EntityType type)
            => Factory.CreateStaticPhysicalBody(Center.Value, Angle.Value, Shape.Value, Dimensions.Value.Width,
                                                Dimensions.Value.Height, type, PowerUpType);

        protected DynamicPhysicalBody CreateDynamicPhysicalBody(EntityType type)
            => Factory.CreateDynamicPhysicalBody(Center.Value, Angle.Value, Shape.Value, Dimensions.Value.Width,
                                                 Dimensions.Value.Height, type);

        protected PlayerPhysicalBody CreatePlayerPhysicalBody()
            => Factory.CreatePlayerPhysicalBody(Center.Value, Angle.Value, Shape.Value, Dimensions.Value.Width,
                                                Dimensions.Value.Height);

        private void CheckIfBuildable()
        {
            CheckFieldsPresence(Factory, Center, Dimensions, Shape, Angle);
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
