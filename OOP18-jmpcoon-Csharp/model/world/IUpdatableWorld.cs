using System;
using System.Collections.Generic;
using jmpcoon.model.entities;

namespace jmpcoon.model.world
{
    public interface IUpdatableWorld
    {
        Tuple<double, double> Dimensions { get; }

        int Score { get; }

        void InitLevel(ICollection<EntityProperties> entities);

        void Update();

        bool MovePlayer(MovementType movement);

        bool IsGameOver();

        bool HasPlayerWon();

        ICollection<UnmodifiableEntity> GetAliveEntities();

        ICollection<UnmodifiableEntity> GetDeadEntities();

        Queue<CollisionEvent> GetCurrentEvents();

        int GetPlayerLives();
    }
}
