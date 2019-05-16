namespace jmpcoon.model.entities
{
    public interface IEntityProperties
    {
        EntityType Type { get; }

        BodyShape Shape { get; }

        (double X, double Y) Position { get; }

        (double Width, double Height) Dimensions { get; }

        double Angle { get; }

        PowerUpType? PowerUpType { get; }

        double? WalkingRange { get; }
    }
}
