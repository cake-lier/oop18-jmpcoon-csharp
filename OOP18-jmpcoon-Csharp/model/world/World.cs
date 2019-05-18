using System;
using System.Collections.Generic;
using System.Linq;
using jmpcoon.model.entities;
using jmpcoon.model.physics;

namespace jmpcoon.model.world
{
    public class World : IWorld
    {
        private const double WORLD_WIDTH = 8;
        private const double WORLD_HEIGHT = 4.5;
        private const int ROLLING_POINTS = 50;
        private const int WALKING_POINTS = 100;
        private const string NO_INIT_MSG = "It's needed to initialize this world by initLevel() before using it";

        private readonly IPhysicalFactory physicsFactory;
        private readonly IUpdatablePhysicalWorld innerWorld;
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
            physicsFactory = new PhysicalFactory();
            Dimensions = (Width: WORLD_WIDTH, Height: WORLD_HEIGHT);
            innerWorld = physicsFactory.CreatePhysicalWorld(this, Dimensions.Width, Dimensions.Height);
            aliveEntities = new ClassToInstanceMultimap<IEntity>();
            deadEntities = new ClassToInstanceMultimap<IEntity>();
            currentEvents = new Queue<CollisionEvent>();
            currentState = GameState.IS_GOING;
            player = null;
            Score = 0;
            initialized = false;
        }

        public Queue<CollisionEvent> GetCurrentEvents() => new Queue<CollisionEvent>(currentEvents);

        public ICollection<IUnmodifiableEntity> GetAliveEntities()
            => GetEntitiesStream<StaticEntity>(aliveEntities, e => new UnmodifiableEntity(e),
                                                new List<Type> { typeof(Platform), typeof(Ladder) })
               .Concat(GetDynamicEntitiesStream(aliveEntities))
               .Concat(GetPowerUpStream(aliveEntities))
               .ToHashSet();

        public ICollection<IUnmodifiableEntity> GetDeadEntities() => GetPowerUpStream(deadEntities)
                                                                    .Concat(GetDynamicEntitiesStream(deadEntities))
                                                                    .ToHashSet();

        public bool HasPlayerWon() => currentState == GameState.PLAYER_WON;

        public bool IsGameOver() => currentState == GameState.GAME_OVER;

        public void InitLevel(ICollection<IEntityProperties> entities)
        {
            entities.AsParallel()
                    .ForAll(entity =>
                        {
                            EntityCreator creator = EntityCreator.Values().First(c => c.EntityType == entity.Type);
                            Type entityClass = creator.ClassType;
                            aliveEntities.PutInstance(entityClass, creator.GetEntityBuilder()
                                                                          .SetFactory(physicsFactory)
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
            CheckInitialization();
            if (player != null)
            {
                IPhysicalBody playerBody = player.PhysicalBody;
                EntityState playerState = playerBody.State;
                bool isPlayerAtBottom(IPhysicalBody ladderBody) => PhysicsUtils.IsBodyAtBottomHalf(playerBody, ladderBody);
                if (currentState == GameState.IS_GOING
                    && ((movement == MovementType.JUMP && IsBodyStanding(playerBody))
                        || (movement == MovementType.CLIMB_UP
                            && (IsBodyInFrontLadder(playerBody, isPlayerAtBottom)
                                || playerState == EntityState.CLIMBING_UP || playerState == EntityState.CLIMBING_DOWN))
                        || (movement == MovementType.CLIMB_DOWN
                            && (IsBodyInFrontLadder(playerBody, (e) => !isPlayerAtBottom(e))
                                || playerState == EntityState.CLIMBING_UP || playerState == EntityState.CLIMBING_DOWN))
                        || ((movement == MovementType.MOVE_LEFT || movement == MovementType.MOVE_RIGHT)
                            && playerState != EntityState.CLIMBING_DOWN && playerState != EntityState.CLIMBING_UP)))
                {
                    player.Move(movement);
                    return true;
                }
            }
            return false;
        }

        public int GetPlayerLives()
        {
            CheckInitialization();
            return player?.Lives ?? 0;
        }

        public void Update()
        {
            CheckInitialization();
            currentEvents.Clear();
            deadEntities.Clear();
            innerWorld.Update();
            aliveEntities.AsParallel().ForAll(list => list.Value.AsParallel().ForAll(entity => {
                if (!entity.Alive)
                {
                    deadEntities.PutInstance(list.Key, entity);
                    aliveEntities.Remove(list.Key, entity);
                    innerWorld.RemoveBody(entity.PhysicalBody);
                }
            }));
            if (currentState == GameState.IS_GOING && player != null && !player.Alive)
            {
                currentState = GameState.GAME_OVER;
            }
            aliveEntities.GetInstances<WalkingEnemy>(typeof(WalkingEnemy)).AsParallel().ForAll(e => e.ComputeMovement());
            aliveEntities.GetInstances<EnemyGenerator>(typeof(EnemyGenerator)).AsParallel().ForAll(e => e.OnTimeAdvanced());
        }

        public void AddGeneratedRollingEnemy(RollingEnemy generatedEnemy)
            => aliveEntities.PutInstance(typeof(RollingEnemy), generatedEnemy);

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

        private void CheckInitialization()
        {
            if (!initialized)
            {
                throw new InvalidOperationException(NO_INIT_MSG);
            }
        }

        private IEnumerable<IUnmodifiableEntity> GetDynamicEntitiesStream(IClassToInstanceMultimap<IEntity> multimap)
            => GetEntitiesStream<DynamicEntity>(multimap,
                                                 e => new UnmodifiableEntity(e),
                                                 new List<Type> { typeof(Player), typeof(RollingEnemy), typeof(WalkingEnemy) });

        private IEnumerable<IUnmodifiableEntity> GetEntitiesStream<TEntity>(IClassToInstanceMultimap<IEntity> multimap,
                                                                            Func<TEntity, IUnmodifiableEntity> mapper,
                                                                            ICollection<Type> keys) where TEntity : IEntity
            => keys.SelectMany(type => GetEntityKeyStream(multimap, mapper, type));

        private IEnumerable<IUnmodifiableEntity> GetPowerUpStream(IClassToInstanceMultimap<IEntity> multimap) 
            => GetEntityKeyStream<PowerUp>(multimap, e => new UnmodifiableEntity(e), typeof(PowerUp));

        private IEnumerable<IUnmodifiableEntity> GetEntityKeyStream<TEntity>(IClassToInstanceMultimap<IEntity> multimap,
                                                                             Func<TEntity, IUnmodifiableEntity> mapper,
                                                                             Type key) where TEntity : IEntity 
            => multimap.GetInstances<TEntity>(key).Select(mapper);

        private bool IsBodyStanding(IPhysicalBody body)
        {
            ICollection<IPhysicalBody> platformsBodies = aliveEntities.GetInstances<Platform>(typeof(Platform))
                                                                      .Select(e => e.PhysicalBody)
                                                                      .ToHashSet();
            return innerWorld.GetCollidingBodies(body)
                             .Where(collision => platformsBodies.Contains(collision.Body))
                             .Any(platformStand => PhysicsUtils.IsBodyOnTop(body, platformStand.Body,
                                                                            platformStand.CollisionPoint))
                   && body.State != EntityState.JUMPING;
        }

        private bool IsBodyInFrontLadder(IPhysicalBody body, Predicate<IPhysicalBody> where)
        {
            return aliveEntities.GetInstances<Ladder>(typeof(Ladder)).Select(e => e.PhysicalBody)
                                                                     .Any(ladderBody =>
                                                                          innerWorld.AreBodiesInContact(body, ladderBody)
                                                                          && where.Invoke(ladderBody)
                                                                          && PhysicsUtils.IsBodyInside(body, ladderBody));
        }
    }
}
