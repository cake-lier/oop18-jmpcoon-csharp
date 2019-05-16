using System.Collections.Generic;

namespace jmpcoon.model.physics
{
    public interface IUpdatablePhysicalWorld
    {
        bool AreBodiesInContact(IPhysicalBody first, IPhysicalBody second);

        void RemoveBody(IPhysicalBody body);

        ICollection<(IPhysicalBody Body, (double X, double Y) CollisionPoint)> GetCollidingBodies(IPhysicalBody body);

        void Update();
    }
}
