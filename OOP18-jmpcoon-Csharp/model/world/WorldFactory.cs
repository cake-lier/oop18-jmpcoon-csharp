using System;
namespace jmpcoon.model.world
{
    public class WorldFactory : IWorldFactory
    {
        private const string NO_TWO_WORLDS_MSG = "There should be only one instance of World";

        private bool worldCreated;

        public WorldFactory() => worldCreated = false;

        public IUpdatableWorld Create()
        {
            if (!worldCreated) {
                worldCreated = true;
                return new World();
            }
            throw new InvalidOperationException(NO_TWO_WORLDS_MSG);
        }
    }
}
