using System.Collections.Generic;
using jmpcoon.model.entities;

namespace jmpcoon.model.world
{
    public interface IUpdatableWorld
    {
        (double Width, double Height) Dimensions { get; }

        int Score { get; }

        void InitLevel(ICollection<IEntityProperties> entities);

        void Update();

        bool MovePlayer(MovementType movement);

        bool IsGameOver();

        bool HasPlayerWon();

        ICollection<IUnmodifiableEntity> GetAliveEntities();

        ICollection<IUnmodifiableEntity> GetDeadEntities();

        Queue<CollisionEvent> GetCurrentEvents();

        int GetPlayerLives();
    }
}
