using System;
using System.Collections.Generic;
using System.Linq;
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

        private readonly ClassToInstanceMultimap<IEntity> aliveEntities;
        private readonly ClassToInstanceMultimap<IEntity> deadEntities;
        private readonly Queue<CollisionEvent> currentEvents;
        private Player player;
        private GameState currentState;
        private bool initialized;

        public (double Width, double Height) Dimensions { get; }
        public int Score { get; private set; }

        public World()
        {
            Dimensions = (Width: WORLD_WIDTH, Height: WORLD_HEIGHT);
            aliveEntities = new ClassToInstanceMultimap<IEntity>();
            deadEntities = new ClassToInstanceMultimap<IEntity>();
            currentEvents = new Queue<CollisionEvent>();
            currentState = GameState.IS_GOING;
            player = null;
            Score = 0;
            initialized = false;
        }

        public Queue<CollisionEvent> GetCurrentEvents() => currentEvents;

        public ICollection<UnmodifiableEntity> GetAliveEntities()
            => GetEntitiesStream<StaticEntity>(aliveEntities, e => new UnmodifiableEntity(e),
                                                new List<Type> { typeof(Platform), typeof(Ladder) })
               .Concat(GetDynamicEntitiesStream(aliveEntities))
               .Concat(GetPowerUpStream(aliveEntities))
               .ToList();

        public ICollection<UnmodifiableEntity> GetDeadEntities() => GetPowerUpStream(deadEntities)
                                                                    .Concat(GetDynamicEntitiesStream(deadEntities))
                                                                    .ToList();

        public bool HasPlayerWon() => currentState == GameState.PLAYER_WON;

        public bool IsGameOver() => currentState == GameState.GAME_OVER;

        public void InitLevel(ICollection<EntityProperties> entities)
        {
            entities.AsParallel()
                    .ForAll(entity =>
                        {
                            EntityCreator creator = EntityCreator.Values().First(c => c.EntityType.Equals(entity.Type));
                            Type entityClass = creator.ClassType;
                            aliveEntities.PutInstance(entityClass, creator.GetEntityBuilder()
                                                                          .SetDimensions(entity.Dimensions)
                                                                          .SetPosition(entity.Position)
                                                                          .SetAngle(entity.Angle)
                                                                          .SetShape(entity.Shape)
                                                                          .SetPowerUpType(entity.PowerUpType)
                                                                          .SetWalkingRange(entity.WalkingRange)
                                                                          .SetWorld(entity.Type == EntityType.ENEMY_GENERATOR 
                                                                                    ? this : null)
                                                                          .Build());
                            if (entity.Type == EntityType.PLAYER)
                            {
                                player = aliveEntities.GetInstances<Player>(typeof(Player)).ElementAt(0);
                            }
                        });
            initialized = true;
        }

        public bool MovePlayer(MovementType movement)
        {
            player?.Move(movement);
            return true;
        }

        public int GetPlayerLives() => player?.Lives ?? 0;

        public void Update()
        {
            CheckInitialization();
            currentEvents.Clear();
            deadEntities.Clear();
            aliveEntities.AsParallel().ForAll(list => list.Value.AsParallel().ForAll(entity => {
                if (!entity.Alive)
                {
                    deadEntities.PutInstance(list.Key, entity);
                    aliveEntities.Remove(list.Key, entity);
                }
            }));
            if (currentState == GameState.IS_GOING && player != null && !player.Alive)
            {
                currentState = GameState.GAME_OVER;
            }
            aliveEntities.GetInstances<WalkingEnemy>(typeof(WalkingEnemy)).AsParallel().ForAll(e => e.ComputeMovement());
            aliveEntities.GetInstances<EnemyGenerator>(typeof(EnemyGenerator)).AsParallel().ForAll(e => e.OnTimeAdvanced());
        }

        public void AddGeneratedRollingEnemy(RollingEnemy rollingEnemy)
            => aliveEntities.PutInstance(typeof(RollingEnemy), rollingEnemy);

        private void CheckInitialization()
        {
            if (!initialized)
            {
                throw new InvalidOperationException(NO_INIT_MSG);
            }
        }

        private IEnumerable<UnmodifiableEntity> GetDynamicEntitiesStream(IClassToInstanceMultimap<IEntity> multimap)
            => GetEntitiesStream<DynamicEntity>(multimap,
                                                 e => new UnmodifiableEntity(e),
                                                 new List<Type> { typeof(Player), typeof(RollingEnemy), typeof(WalkingEnemy) });

        private IEnumerable<UnmodifiableEntity> GetEntitiesStream<E>(IClassToInstanceMultimap<IEntity> multimap,
                                                                     Func<E, UnmodifiableEntity> mapper,
                                                                     ICollection<Type> keys) where E : IEntity
            => keys.SelectMany(type => GetEntityKeyStream(multimap, mapper, type));

        private IEnumerable<UnmodifiableEntity> GetPowerUpStream(IClassToInstanceMultimap<IEntity> multimap) 
            => GetEntityKeyStream<PowerUp>(multimap, e => new UnmodifiableEntity(e), typeof(PowerUp));

        private IEnumerable<UnmodifiableEntity> GetEntityKeyStream<E>(IClassToInstanceMultimap<IEntity> multimap,
                                                                      Func<E, UnmodifiableEntity> mapper,
                                                                      Type key) where E : IEntity 
            => multimap.GetInstances<E>(key).Select(mapper);

        public void NotifyCollision(CollisionEvent collisionType)
        {
            switch (collisionType)
            {
                case CollisionEvent.ROLLING_ENEMY_KILLED:
                    Score += ROLLING_POINTS;
                    break;
                case CollisionEvent.WALKING_ENEMY_KILLED:
                    Score += WALKING_POINTS;
                    break;
                case CollisionEvent.GOAL_HIT:
                    currentState = GameState.PLAYER_WON;
                    break;
                case CollisionEvent.PLAYER_KILLED:
                    currentState = GameState.GAME_OVER;
                    break;
            }
            currentEvents.Enqueue(collisionType);
        }
    }
}
