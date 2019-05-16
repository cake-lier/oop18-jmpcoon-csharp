namespace jmpcoon.model.physics
{
    public static class PhysicsUtils
    {
        public static bool IsBodyAtBottomHalf(IPhysicalBody playerBody, IPhysicalBody ladderBody) => false;

        public static bool IsBodyOnTop(IPhysicalBody body1, IPhysicalBody body2, (double X, double Y) collisionPoint) => false;

        public static bool IsBodyInside(IPhysicalBody body, IPhysicalBody ladderBody) => false;
    }
}
