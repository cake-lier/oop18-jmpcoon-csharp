using jmpcoon.model.entities;
using jmpcoon.model.physics;
using jmpcoon.model.world;
using NUnit.Framework;
using System;
namespace jmpcoon.test
{
    [TestFixture]
    public class EntityCreationTest
    {
        private const double WORLD_WIDTH = 8;
        private const double WORLD_HEIGHT = 4.5;
        private const double STD_RANGE = 1.00;
        private const double STD_ANGLE = Math.PI / 4;
        private const double PRECISION = 0.001;
        private const string GIVEN_EQUALS_SET_MSG = "The value given during creation to the EntityBuilder doesn't equal the "
                                                       + "one set to the Entity";
        private readonly (double X, double Y) STD_POSITION = (X: WORLD_WIDTH / 2, Y: WORLD_HEIGHT / 2);
        private readonly (double Width, double Height) STD_RECTANGULAR_DIMENSIONS = (Width: WORLD_WIDTH / 10,
                                                                                     Height: WORLD_HEIGHT / 5);
        private readonly (double Width, double Height) STD_CIRCULAR_DIMENSIONS = (Width: WORLD_WIDTH / 15,
                                                                                  Height: WORLD_WIDTH / 15);

        private readonly IPhysicalFactory factory;
        private readonly IWorld world;

        public EntityCreationTest()
        {
            factory = new PhysicalFactory();
            world = (IWorld)new WorldFactory().Create();
            factory.CreatePhysicalWorld(world, WORLD_WIDTH, WORLD_HEIGHT);
        }

        [Test]
        public void LadderCreationTest()
        {
            IEntityBuilder<Ladder> ladderBuilder = EntityBuilderUtils.GetLadderBuilder()
                                                                     .SetPosition(STD_POSITION)
                                                                     .SetFactory(factory)
                                                                     .SetDimensions(STD_RECTANGULAR_DIMENSIONS)
                                                                     .SetAngle(STD_ANGLE)
                                                                     .SetShape(BodyShape.RECTANGLE);
            Ladder ladder = ladderBuilder.Build();
            Assert.AreEqual(STD_POSITION, ladder.Position, GIVEN_EQUALS_SET_MSG);
            Assert.AreEqual(STD_RECTANGULAR_DIMENSIONS, ladder.Dimensions, GIVEN_EQUALS_SET_MSG);
            Assert.AreEqual(STD_ANGLE, ladder.Angle, PRECISION, GIVEN_EQUALS_SET_MSG);
            Assert.AreEqual(BodyShape.RECTANGLE, ladder.Shape, GIVEN_EQUALS_SET_MSG);
            Assert.AreEqual(EntityType.LADDER, ladder.Type, GIVEN_EQUALS_SET_MSG);
            Assert.Throws<InvalidOperationException>(() => ladderBuilder.Build());
        }

        [Test]
        public void PlatformCreationTest()
        {
            IEntityBuilder<Platform> platformBuilder = EntityBuilderUtils.GetPlatformBuilder()
                                                                         .SetPosition(STD_POSITION)
                                                                         .SetFactory(factory)
                                                                         .SetDimensions(STD_RECTANGULAR_DIMENSIONS)
                                                                         .SetAngle(STD_ANGLE)
                                                                         .SetShape(BodyShape.RECTANGLE);
            Platform platform = platformBuilder.Build();
            Assert.AreEqual(STD_POSITION, platform.Position, GIVEN_EQUALS_SET_MSG);
            Assert.AreEqual(STD_RECTANGULAR_DIMENSIONS, platform.Dimensions, GIVEN_EQUALS_SET_MSG);
            Assert.AreEqual(STD_ANGLE, platform.Angle, PRECISION, GIVEN_EQUALS_SET_MSG);
            Assert.AreEqual(BodyShape.RECTANGLE, platform.Shape, GIVEN_EQUALS_SET_MSG);
            Assert.AreEqual(EntityType.PLATFORM, platform.Type, GIVEN_EQUALS_SET_MSG);
            Assert.Throws<InvalidOperationException>(() => platformBuilder.Build());
        }

        [Test]
        public void RollingEnemyCreationTest()
        {
            IEntityBuilder<RollingEnemy> rollingBuilder = EntityBuilderUtils.GetRollingEnemyBuilder()
                                                                            .SetDimensions(STD_CIRCULAR_DIMENSIONS)
                                                                            .SetPosition(STD_POSITION)
                                                                            .SetFactory(factory)
                                                                            .SetAngle(STD_ANGLE)
                                                                            .SetShape(BodyShape.CIRCLE);
            RollingEnemy rollingEnemy = rollingBuilder.Build();
            Assert.AreEqual(STD_POSITION, rollingEnemy.Position, GIVEN_EQUALS_SET_MSG);
            Assert.AreEqual(STD_CIRCULAR_DIMENSIONS, rollingEnemy.Dimensions, GIVEN_EQUALS_SET_MSG);
            Assert.AreEqual(STD_ANGLE, rollingEnemy.Angle, PRECISION, GIVEN_EQUALS_SET_MSG);
            Assert.AreEqual(BodyShape.CIRCLE, rollingEnemy.Shape, GIVEN_EQUALS_SET_MSG);
            Assert.AreEqual(EntityType.ROLLING_ENEMY, rollingEnemy.Type, GIVEN_EQUALS_SET_MSG);
            Assert.Throws<InvalidOperationException>(() => rollingBuilder.Build());
        }

        [Test]
        public void WalkingEnemyCreationTest()
        {
            IEntityBuilder<WalkingEnemy> walkingBuilder = EntityBuilderUtils.GetWalkingEnemyBuilder()
                                                                            .SetDimensions(STD_RECTANGULAR_DIMENSIONS)
                                                                            .SetPosition(STD_POSITION)
                                                                            .SetFactory(factory)
                                                                            .SetAngle(STD_ANGLE)
                                                                            .SetShape(BodyShape.RECTANGLE)
                                                                            .SetWalkingRange(STD_RANGE);
            WalkingEnemy walkingEnemy = walkingBuilder.Build();
            Assert.AreEqual(STD_POSITION, walkingEnemy.Position, GIVEN_EQUALS_SET_MSG);
            Assert.AreEqual(STD_RECTANGULAR_DIMENSIONS, walkingEnemy.Dimensions, GIVEN_EQUALS_SET_MSG);
            Assert.AreEqual(STD_ANGLE, walkingEnemy.Angle, PRECISION, GIVEN_EQUALS_SET_MSG);
            Assert.AreEqual(BodyShape.RECTANGLE, walkingEnemy.Shape, GIVEN_EQUALS_SET_MSG);
            Assert.AreEqual(EntityType.WALKING_ENEMY, walkingEnemy.Type, GIVEN_EQUALS_SET_MSG);
            Assert.Throws<InvalidOperationException>(() => walkingBuilder.Build());
        }

        [Test]
        public void WalkingEnemyCreationWithoutWalkingRangeFail()
        {
            Assert.Throws<InvalidOperationException>(() => EntityBuilderUtils.GetWalkingEnemyBuilder()
                                                                             .SetPosition(STD_POSITION)
                                                                             .SetFactory(factory)
                                                                             .SetDimensions(STD_RECTANGULAR_DIMENSIONS)
                                                                             .SetShape(BodyShape.RECTANGLE)
                                                                             .SetAngle(STD_ANGLE)
                                                                             .Build());
        }

        [Test]
        public void PlayerCreationTest()
        {
            IEntityBuilder<Player> playerBuilder = EntityBuilderUtils.GetPlayerBuilder()
                                                                     .SetDimensions(STD_RECTANGULAR_DIMENSIONS)
                                                                     .SetPosition(STD_POSITION)
                                                                     .SetFactory(factory)
                                                                     .SetAngle(STD_ANGLE)
                                                                     .SetShape(BodyShape.RECTANGLE);
            Player player = playerBuilder.Build();
            Assert.AreEqual(STD_POSITION, player.Position, GIVEN_EQUALS_SET_MSG);
            Assert.AreEqual(STD_RECTANGULAR_DIMENSIONS, player.Dimensions, GIVEN_EQUALS_SET_MSG);
            Assert.AreEqual(STD_ANGLE, player.Angle, PRECISION, GIVEN_EQUALS_SET_MSG);
            Assert.AreEqual(BodyShape.RECTANGLE, player.Shape, GIVEN_EQUALS_SET_MSG);
            Assert.AreEqual(EntityType.PLAYER, player.Type, GIVEN_EQUALS_SET_MSG);
            Assert.Throws<InvalidOperationException>(() => playerBuilder.Build());
        }

        [Test]
        public void PowerUpExtraLifeCreationTest()
        {
            IEntityBuilder<PowerUp> powerUpBuilder 
                = EntityBuilderUtils.GetPowerUpBuilder()
                                    .SetPosition(STD_POSITION)
                                    .SetDimensions(STD_RECTANGULAR_DIMENSIONS)
                                    .SetFactory(factory)
                                    .SetAngle(STD_ANGLE)
                                    .SetShape(BodyShape.RECTANGLE)
                                    .SetPowerUpType(PowerUpType.EXTRA_LIFE);
            PowerUp powerUp = powerUpBuilder.Build();
            Assert.AreEqual(STD_POSITION, powerUp.Position, GIVEN_EQUALS_SET_MSG);
            Assert.AreEqual(STD_RECTANGULAR_DIMENSIONS, powerUp.Dimensions, GIVEN_EQUALS_SET_MSG);
            Assert.AreEqual(STD_ANGLE, powerUp.Angle, PRECISION, GIVEN_EQUALS_SET_MSG);
            Assert.AreEqual(BodyShape.RECTANGLE, powerUp.Shape, GIVEN_EQUALS_SET_MSG);
            Assert.AreEqual(EntityType.POWERUP, powerUp.Type, GIVEN_EQUALS_SET_MSG);
            Assert.AreEqual(PowerUpType.EXTRA_LIFE, powerUp.PowerUpType, GIVEN_EQUALS_SET_MSG);
            Assert.Throws<InvalidOperationException>(() => powerUpBuilder.Build());
        }

        [Test]
        public void PowerUpGoalCreationTest()
        {
            IEntityBuilder<PowerUp> powerUpBuilder = EntityBuilderUtils.GetPowerUpBuilder()
                                                                       .SetPosition(STD_POSITION)
                                                                       .SetDimensions(STD_RECTANGULAR_DIMENSIONS)
                                                                       .SetFactory(factory)
                                                                       .SetAngle(STD_ANGLE)
                                                                       .SetShape(BodyShape.RECTANGLE)
                                                                       .SetPowerUpType(PowerUpType.GOAL);
            PowerUp powerUp = powerUpBuilder.Build();
            Assert.AreEqual(STD_POSITION, powerUp.Position, GIVEN_EQUALS_SET_MSG);
            Assert.AreEqual(STD_RECTANGULAR_DIMENSIONS, powerUp.Dimensions, GIVEN_EQUALS_SET_MSG);
            Assert.AreEqual(STD_ANGLE, powerUp.Angle, PRECISION, GIVEN_EQUALS_SET_MSG);
            Assert.AreEqual(BodyShape.RECTANGLE, powerUp.Shape, GIVEN_EQUALS_SET_MSG);
            Assert.AreEqual(EntityType.POWERUP, powerUp.Type, GIVEN_EQUALS_SET_MSG);
            Assert.AreEqual(PowerUpType.GOAL, powerUp.PowerUpType, GIVEN_EQUALS_SET_MSG);
            Assert.Throws<InvalidOperationException>(() => powerUpBuilder.Build());
        }

        [Test]
        public void PowerUpInvincibilityCreationTest()
        {
            IEntityBuilder<PowerUp> powerUpBuilder =
                    EntityBuilderUtils.GetPowerUpBuilder()
                                      .SetPosition(STD_POSITION)
                                      .SetDimensions(STD_RECTANGULAR_DIMENSIONS)
                                      .SetFactory(factory)
                                      .SetAngle(STD_ANGLE)
                                      .SetShape(BodyShape.RECTANGLE)
                                      .SetPowerUpType(PowerUpType.INVINCIBILITY);
            PowerUp powerUp = powerUpBuilder.Build();
            Assert.AreEqual(STD_POSITION, powerUp.Position, GIVEN_EQUALS_SET_MSG);
            Assert.AreEqual(STD_RECTANGULAR_DIMENSIONS, powerUp.Dimensions, GIVEN_EQUALS_SET_MSG);
            Assert.AreEqual(STD_ANGLE, powerUp.Angle, PRECISION, GIVEN_EQUALS_SET_MSG);
            Assert.AreEqual(BodyShape.RECTANGLE, powerUp.Shape, GIVEN_EQUALS_SET_MSG);
            Assert.AreEqual(EntityType.POWERUP, powerUp.Type, GIVEN_EQUALS_SET_MSG);
            Assert.AreEqual(PowerUpType.INVINCIBILITY, powerUp.PowerUpType, GIVEN_EQUALS_SET_MSG);
            Assert.Throws<InvalidOperationException>(() => powerUpBuilder.Build());
        }

        [Test]
        public void CreationWithoutTypeFail()
        {
            Assert.Throws<InvalidOperationException>(() => EntityBuilderUtils.GetPowerUpBuilder()
                                                                             .SetPosition(STD_POSITION)
                                                                             .SetFactory(factory)
                                                                             .SetDimensions(STD_RECTANGULAR_DIMENSIONS)
                                                                             .SetShape(BodyShape.RECTANGLE)
                                                                             .SetAngle(STD_ANGLE)
                                                                             .Build());
        }

        [Test]
        public void EnemyGeneratorCreationTest()
        {
            IEntityBuilder<EnemyGenerator> enemyGeneratorBuilder =
                EntityBuilderUtils.GetEnemyGeneratorBuilder()
                                  .SetPosition(STD_POSITION)
                                  .SetDimensions(STD_CIRCULAR_DIMENSIONS)
                                  .SetFactory(factory)
                                  .SetAngle(STD_ANGLE)
                                  .SetShape(BodyShape.CIRCLE)
                                  .SetWorld(world);
            EnemyGenerator enemyGenerator = enemyGeneratorBuilder.Build();
            Assert.AreEqual(STD_POSITION, enemyGenerator.Position, GIVEN_EQUALS_SET_MSG);
            Assert.AreEqual(STD_CIRCULAR_DIMENSIONS, enemyGenerator.Dimensions, GIVEN_EQUALS_SET_MSG);
            Assert.AreEqual(STD_ANGLE, enemyGenerator.Angle, PRECISION, GIVEN_EQUALS_SET_MSG);
            Assert.AreEqual(BodyShape.CIRCLE, enemyGenerator.Shape, GIVEN_EQUALS_SET_MSG);
            Assert.AreEqual(EntityType.ENEMY_GENERATOR, enemyGenerator.Type, GIVEN_EQUALS_SET_MSG);
            Assert.Throws<InvalidOperationException>(() => enemyGeneratorBuilder.Build());
        }

        [Test]
        public void EnemyGeneratorCreationWithoutWorldFail()
        {
            Assert.Throws<InvalidOperationException>(() => EntityBuilderUtils.GetEnemyGeneratorBuilder()
                                                                             .SetPosition(STD_POSITION)
                                                                             .SetFactory(factory)
                                                                             .SetDimensions(STD_CIRCULAR_DIMENSIONS)
                                                                             .SetShape(BodyShape.CIRCLE)
                                                                             .SetAngle(STD_ANGLE)
                                                                             .Build());
        }
    }
}
