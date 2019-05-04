using System;
using jmpcoon.model.entities;

namespace jmpcoon.model.world
{
    public struct EntityCreator<E> where E : IEntity
    {
#pragma warning disable RECS0108 // Warns about static fields in generic types
        public static readonly EntityCreator<Ladder> LADDER
            = new EntityCreator<Ladder>(EntityType.LADDER, typeof(Ladder), () => EntityBuilderUtils.GetLadderBuilder());
        public static readonly EntityCreator<Player> PLAYER 
            = new EntityCreator<Player>(EntityType.PLAYER, typeof(Player), () => EntityBuilderUtils.GetPlayerBuilder());
        public static readonly EntityCreator<Platform> PLATFORM 
            = new EntityCreator<Platform>(EntityType.PLATFORM, typeof(Platform), () => EntityBuilderUtils.GetPlatformBuilder());
        public static readonly EntityCreator<PowerUp> POWERUP
            = new EntityCreator<PowerUp>(EntityType.POWERUP, typeof(PowerUp), () => EntityBuilderUtils.GetPowerUpBuilder());
        public static readonly EntityCreator<RollingEnemy> ROLLING_ENEMY
            = new EntityCreator<RollingEnemy>(EntityType.ROLLING_ENEMY, typeof(RollingEnemy), () => EntityBuilderUtils.GetRollingEnemyBuilder());
        public static readonly EntityCreator<WalkingEnemy> WALKING_ENEMY 
            = new EntityCreator<WalkingEnemy>(EntityType.WALKING_ENEMY, typeof(WalkingEnemy), () => EntityBuilderUtils.GetWalkingEnemyBuilder());
        public static readonly EntityCreator<EnemyGenerator> ENEMY_GENERATOR
            = new EntityCreator<EnemyGenerator>(EntityType.ENEMY_GENERATOR, typeof(EnemyGenerator), () => EntityBuilderUtils.GetEnemyGeneratorBuilder());
#pragma warning restore RECS0108 // Warns about static fields in generic types

        private delegate AbstractEntityBuilder<E> EntityBuilderSupplier();

        private readonly EntityBuilderSupplier supplier;
        private Type ClassType { get; }
        private EntityType EntityType { get; }

        private EntityCreator(EntityType associatedType, Type associatedClass, EntityBuilderSupplier supplier)
        {
            EntityType = associatedType;
            this.supplier = supplier;
            ClassType = associatedClass;
        }

        public AbstractEntityBuilder<E> GetEntityBuilder() => supplier.Invoke();
    }
}
