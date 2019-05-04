using System;

namespace jmpcoon.model.entities
{
    public static class EntityBuilderUtils
    {
        private const string NOT_REQ_FIELDS = "Not all the fields necessary to build have been initialized";

        public static AbstractEntityBuilder<EnemyGenerator> GetEnemyGeneratorBuilder() => new EnemyGeneratorBuilder();

        private class EnemyGeneratorBuilder : AbstractEntityBuilder<EnemyGenerator>
        {
            protected override EnemyGenerator BuildEntity()
            {
                if (GetWorld() != null)
                {
                    return new EnemyGenerator(GetWorld());
                }
                throw new InvalidOperationException(NOT_REQ_FIELDS);
            }
        }

        public static AbstractEntityBuilder<Ladder> GetLadderBuilder() => new LadderBuilder();

        private class LadderBuilder : AbstractEntityBuilder<Ladder> {
            protected override Ladder BuildEntity() => new Ladder();
        }

        public static AbstractEntityBuilder<Player> GetPlayerBuilder() => new PlayerBuilder();

        private class PlayerBuilder : AbstractEntityBuilder<Player> {
            protected override Player BuildEntity() => new Player();
        }

        public static AbstractEntityBuilder<Platform> GetPlatformBuilder() => new PlatformBuilder();

        private class PlatformBuilder : AbstractEntityBuilder<Platform> {
            protected override Platform BuildEntity() => new Platform();
        }

        public static AbstractEntityBuilder<PowerUp> GetPowerUpBuilder() => new PowerUpBuilder();

        private class PowerUpBuilder : AbstractEntityBuilder<PowerUp> {
            protected override PowerUp BuildEntity()
            {
                if (GetPowerUpType().HasValue)
                {
                    return new PowerUp(GetPowerUpType().Value);
                }
                throw new InvalidOperationException(NOT_REQ_FIELDS);
            }
        }

        public static AbstractEntityBuilder<RollingEnemy> GetRollingEnemyBuilder() => new RollingEnemyBuilder();

        private class RollingEnemyBuilder : AbstractEntityBuilder<RollingEnemy> {
            protected override RollingEnemy BuildEntity() => new RollingEnemy();
        }

        public static AbstractEntityBuilder<WalkingEnemy> GetWalkingEnemyBuilder() => new WalkingEnemyBuilder();

        private class WalkingEnemyBuilder : AbstractEntityBuilder<WalkingEnemy> {
            protected override WalkingEnemy BuildEntity()
            {
                if (GetWalkingRange().HasValue)
                {
                    return new WalkingEnemy(GetWalkingRange().Value);
                }
                throw new InvalidOperationException(NOT_REQ_FIELDS);
            }
        }
    }
}
