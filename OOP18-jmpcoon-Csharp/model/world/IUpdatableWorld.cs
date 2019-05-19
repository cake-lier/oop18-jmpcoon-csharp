using System.Collections.Generic;
using System.Collections.Immutable;
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

        IReadOnlyCollection<IUnmodifiableEntity> GetAliveEntities();

        IReadOnlyCollection<IUnmodifiableEntity> GetDeadEntities();

        ImmutableQueue<CollisionEvent> GetCurrentEvents();

        int GetPlayerLives();
    }
}
