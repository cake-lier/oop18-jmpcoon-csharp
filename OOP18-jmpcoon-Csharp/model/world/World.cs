using System;
using System.Collections.Generic;
using jmpcoon.model.entities;

namespace jmpcoon.model.world
{
    public class World : IWorld
    {
        private const double WORLD_WIDTH = 8;
        private const double WORLD_HEIGHT = 4.5;
        private const int ROLLING_POINTS = 50;
        private const int WALKING_POINTS = 100;
        private const string NO_INIT_MSG = "It's needed to initialize this world by initLevel() before using it";

        private readonly IClassToInstanceMultimap<IEntity> aliveEntities;
        private readonly IClassToInstanceMultimap<IEntity> deadEntities;
        private readonly Queue<CollisionEvent> currentEvents;
        private Player player;
        private GameState currentState;
        private bool initialized;

        public Tuple<double, double> Dimensions { get; }
        public int Score { get; private set; }

        public World()
        {
            Dimensions = Tuple.Create(WORLD_WIDTH, WORLD_HEIGHT);
            aliveEntities = new ClassToInstanceMultimap<IEntity>();
            deadEntities = new ClassToInstanceMultimap<IEntity>();
            currentEvents = new Queue<CollisionEvent>();
            currentState = GameState.IS_GOING;
            player = null;
            Score = 0;
            initialized = false;
        }

        public ICollection<UnmodifiableEntity> GetAliveEntities()
        {
            throw new NotImplementedException();
        }

        public Queue<CollisionEvent> GetCurrentEvents()
        {
            throw new NotImplementedException();
        }

        public ICollection<UnmodifiableEntity> GetDeadEntities()
        {
            throw new NotImplementedException();
        }

        public int GetPlayerLives()
        {
            throw new NotImplementedException();
        }

        public bool HasPlayerWon() => currentState == GameState.PLAYER_WON;

        public bool IsGameOver() => currentState == GameState.GAME_OVER;

        public void InitLevel(ICollection<EntityProperties> entities)
        {
            throw new NotImplementedException();
        }

        public bool MovePlayer(MovementType movement)
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}
