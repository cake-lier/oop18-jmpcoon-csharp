using System;

namespace jmpcoon.model.entities
{
    public static class EntityBuilderUtils
    {
        private const string NOT_REQ_FIELDS = "Not all the fields necessary to build have been initialized";

        public static IEntityBuilder<EnemyGenerator> GetEnemyGeneratorBuilder() => new EnemyGeneratorBuilder();

        private class EnemyGeneratorBuilder : AbstractEntityBuilder<EnemyGenerator>
        {
            protected override EnemyGenerator BuildEntity()
            {
                if (GetWorld() != null)
                {
                    return new EnemyGenerator(CreateStaticPhysicalBody(), GetWorld());
                }
                throw new InvalidOperationException(NOT_REQ_FIELDS);
            }
        }

        public static IEntityBuilder<Ladder> GetLadderBuilder() => new LadderBuilder();

        private class LadderBuilder : AbstractEntityBuilder<Ladder> {
            protected override Ladder BuildEntity() => new Ladder(CreateStaticPhysicalBody());
        }

        public static IEntityBuilder<Player> GetPlayerBuilder() => new PlayerBuilder();

        private class PlayerBuilder : AbstractEntityBuilder<Player> {
            protected override Player BuildEntity() => new Player(CreatePlayerPhysicalBody());
        }

        public static IEntityBuilder<Platform> GetPlatformBuilder() => new PlatformBuilder();

        private class PlatformBuilder : AbstractEntityBuilder<Platform> {
            protected override Platform BuildEntity() => new Platform(CreateStaticPhysicalBody());
        }

        public static IEntityBuilder<PowerUp> GetPowerUpBuilder() => new PowerUpBuilder();

        private class PowerUpBuilder : AbstractEntityBuilder<PowerUp> {
            protected override PowerUp BuildEntity()
            {
                if (GetPowerUpType().HasValue)
                {
                    return new PowerUp(CreateStaticPhysicalBody(), GetPowerUpType().Value);
                }
                throw new InvalidOperationException(NOT_REQ_FIELDS);
            }
        }

        public static IEntityBuilder<RollingEnemy> GetRollingEnemyBuilder() => new RollingEnemyBuilder();

        private class RollingEnemyBuilder : AbstractEntityBuilder<RollingEnemy> {
            protected override RollingEnemy BuildEntity() => new RollingEnemy(CreateDynamicPhysicalBody());
        }

        public static IEntityBuilder<WalkingEnemy> GetWalkingEnemyBuilder() => new WalkingEnemyBuilder();

        private class WalkingEnemyBuilder : AbstractEntityBuilder<WalkingEnemy> {
            protected override WalkingEnemy BuildEntity()
            {
                if (GetWalkingRange().HasValue)
                {
                    return new WalkingEnemy(CreateDynamicPhysicalBody(), GetWalkingRange().Value);
                }
                throw new InvalidOperationException(NOT_REQ_FIELDS);
            }
        }
    }
}
