using jmpcoon.model.entities;
using jmpcoon.model.physics;
using jmpcoon.model.world;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace jmpcoon.test
{
    [TestFixture]
    public class WorldTest
    {
        private const double WORLD_WIDTH = 8;
        private const double WORLD_HEIGHT = 4.5;
        private const double PLATFORM_WIDTH = WORLD_WIDTH / 2;
        private const double PLATFORM_HEIGHT = 0.29;
        private const double PLAYER_DIMENSION = 0.3;
        private const double LADDER_HEIGHT = 1;
        private const double LADDER_WIDTH = 0.3;
        private const double ANGLE = 0;
        private const double PRECISION = 0.007;
        private const int SHORT_UPDATE_STEPS = 10;
        private const int LONG_UPDATE_STEPS = 100;
        private const string WRONG_DIMENSIONS = "The world created had the wrong dimensions";
        private const string NO_PLAYER = "No player was inserted in the world";
        private const string PLAYER_MOVE = "The player shouldn't have moved when prompted";
        private const string WRONG_ENTITIES_NUMBER = "A different number of entities were created";
        private const string NO_PLAYER_RIGHT = "The player didn't move right";
        private const string NO_PLAYER_LEFT = "The player didn't move left";
        private const string NO_PLAYER_JUMP = "The player didn't jump";
        private const string NO_PLAYER_CLIMB_UP = "The player didn't climb up when prompted";
        private const string NO_PLAYER_CLIMB_DOWN = "The player didn't climb down when prompted";
        private const string NO_DEAD_ENTITIES = "There shouldn't be dead entities";
        private const string SCORE_ZERO = "The score should be zero";
        private const string NO_EVENTS = "There should be no events in queue";
        private const string GAME_ONGOING = "The game should be ongoing";
        private const string INITIAL_LIVES = "The player should not have more than one life";
        private const string ZERO_LIVES = "The player should have zero lives";

        private readonly IEntityProperties platformProperties;
        private readonly IEntityProperties playerProperties;
        private readonly IEntityProperties ladderProperties;
        private IUpdatableWorld world;

        public WorldTest() {
            platformProperties = new EntityProperties(EntityType.PLATFORM, BodyShape.RECTANGLE, WORLD_WIDTH / 2, WORLD_HEIGHT / 2,
                                                      PLATFORM_WIDTH, PLATFORM_HEIGHT, ANGLE, null, null);
            playerProperties = new EntityProperties(EntityType.PLAYER, BodyShape.RECTANGLE, WORLD_WIDTH / 2,
                                                    WORLD_HEIGHT / 2 + PLATFORM_HEIGHT / 2 + PLAYER_DIMENSION / 2,
                                                    PLAYER_DIMENSION, PLAYER_DIMENSION, ANGLE, null, null);
            ladderProperties = new EntityProperties(EntityType.LADDER, BodyShape.RECTANGLE, WORLD_WIDTH / 2,
                                                    WORLD_HEIGHT / 2 + LADDER_HEIGHT / 2, LADDER_WIDTH,
                                                    LADDER_HEIGHT, ANGLE, null, null);
        }

        [SetUp]
        public void InitializeWorld() {
            world = new WorldFactory().Create();
        }

        [Test]
        public void FailedCreationWorldTwiceTest() {
            WorldFactory factory = new WorldFactory();
            factory.Create();
            Assert.Throws<InvalidOperationException>(() => factory.Create());
        }

        [Test]
        public void WorldUpdateWithoutInitializatonExceptionTest() {
            Assert.Throws<InvalidOperationException>(world.Update);
        }

        [Test]
        public void WorldMovePlayerWithoutInitializatonExceptionTest() {
            Assert.Throws<InvalidOperationException>(() => world.MovePlayer(MovementType.MOVE_RIGHT));
        }

        [Test]
        public void WorldPlayerLivesWithoutInitializatonExceptionTest() {
            Assert.Throws<InvalidOperationException>(() => world.GetPlayerLives());
        }

        [Test]
        public void PlayerCreationTest() {
            (double Xinitial, double Yinitial) = playerProperties.Position;
            world.InitLevel(new List<IEntityProperties> { playerProperties });
            var player = world.GetAliveEntities()
                            .First(e => e.Type == EntityType.PLAYER);
            Assert.NotNull(player, NO_PLAYER);
            (double Xcurrent, double Ycurrent) = player.Position;
            Assert.AreEqual(Xinitial, Xcurrent, PRECISION, PLAYER_MOVE);
            Assert.AreEqual(Yinitial, Ycurrent, PRECISION, PLAYER_MOVE);
        }

        [Test]
        public void WorldStatusAtCreationTest() {
            var entities = new List<IEntityProperties> { playerProperties, platformProperties, ladderProperties };
            world.InitLevel(entities);
            Assert.AreEqual(entities.Count, world.GetAliveEntities().Count, WRONG_ENTITIES_NUMBER);
            Assert.AreEqual(0, world.GetDeadEntities().Count, NO_DEAD_ENTITIES);
            Assert.AreEqual(0, world.Score, SCORE_ZERO);
            Assert.AreEqual(0, world.GetCurrentEvents().Count(), NO_EVENTS);
            Assert.IsFalse(world.IsGameOver(), GAME_ONGOING);
            Assert.IsFalse(world.HasPlayerWon(), GAME_ONGOING);
            Assert.AreEqual(1, world.GetPlayerLives(), INITIAL_LIVES);
            Assert.AreEqual(world.Dimensions, (WORLD_WIDTH, WORLD_HEIGHT), WRONG_DIMENSIONS);
        }
    }
}