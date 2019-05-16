using jmpcoon.model.physics;
using jmpcoon.model.world;

namespace jmpcoon.model.entities
{
    public interface IEntityBuilder<out TEntity> where TEntity : IEntity
    {
        IEntityBuilder<TEntity> SetFactory(IPhysicalFactory factory);

        IEntityBuilder<TEntity> SetPosition((double X, double Y) center);

        IEntityBuilder<TEntity> SetDimensions((double Width, double Height) dimensions);

        IEntityBuilder<TEntity> SetShape(BodyShape shape);

        IEntityBuilder<TEntity> SetAngle(double angle);

        IEntityBuilder<TEntity> SetPowerUpType(PowerUpType? powerUpType);

        IEntityBuilder<TEntity> SetWalkingRange(double? walkingRange);

        IEntityBuilder<TEntity> SetWorld(IModifiableWorld world);

        TEntity Build();
    }
}
