using System;
using System.Collections.Generic;
using jmpcoon.model.entities;

namespace jmpcoon.model.world
{
    public struct EntityCreator
    {
        public static readonly EntityCreator LADDER
            = new EntityCreator(EntityType.LADDER, typeof(Ladder), () => EntityBuilderUtils.GetLadderBuilder());
        public static readonly EntityCreator PLAYER
            = new EntityCreator(EntityType.PLAYER, typeof(Player), () => EntityBuilderUtils.GetPlayerBuilder());
        public static readonly EntityCreator PLATFORM
            = new EntityCreator(EntityType.PLATFORM, typeof(Platform), () => EntityBuilderUtils.GetPlatformBuilder());
        public static readonly EntityCreator POWERUP
            = new EntityCreator(EntityType.POWERUP, typeof(PowerUp), () => EntityBuilderUtils.GetPowerUpBuilder());
        public static readonly EntityCreator ROLLING_ENEMY
            = new EntityCreator(EntityType.ROLLING_ENEMY, typeof(RollingEnemy), 
                                () => EntityBuilderUtils.GetRollingEnemyBuilder());
        public static readonly EntityCreator WALKING_ENEMY
            = new EntityCreator(EntityType.WALKING_ENEMY, typeof(WalkingEnemy),
                                () => EntityBuilderUtils.GetWalkingEnemyBuilder());
        public static readonly EntityCreator ENEMY_GENERATOR
            = new EntityCreator(EntityType.ENEMY_GENERATOR, typeof(EnemyGenerator),
                                () => EntityBuilderUtils.GetEnemyGeneratorBuilder());

        public delegate IEntityBuilder<TEntity> EntityBuilderSupplier<out TEntity>() where TEntity : IEntity;

        private readonly EntityBuilderSupplier<IEntity> supplier;

        public Type ClassType { get; }
        public EntityType EntityType { get; }

        private EntityCreator(EntityType associatedType, Type associatedClass, EntityBuilderSupplier<IEntity> supplier)
        {
            EntityType = associatedType;
            this.supplier = supplier;
            ClassType = associatedClass;
        }

        public static IList<EntityCreator> Values()
            => new List<EntityCreator> { LADDER, PLAYER, PLATFORM, POWERUP, ROLLING_ENEMY, WALKING_ENEMY, ENEMY_GENERATOR };

        public IEntityBuilder<IEntity> GetEntityBuilder() => supplier.Invoke();
    }
}
