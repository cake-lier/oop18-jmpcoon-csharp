using System.Collections.Generic;

namespace jmpcoon.model.physics
{
    public class PhysicalWorld : IPhysicalWorld
    {
        public bool AreBodiesInContact(IPhysicalBody first, IPhysicalBody second) => false;

        public ICollection<(IPhysicalBody Body, (double X, double Y) CollisionPoint)> GetCollidingBodies(IPhysicalBody body) 
            => new List<(IPhysicalBody Body, (double CollisionX, double CollisionY))>();

        public void RemoveBody(IPhysicalBody body)
        {
        }

        public void Update()
        {
        }
    }
}
