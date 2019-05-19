using System;
using System.Collections.Generic;
using jmpcoon.model.physics;
using jmpcoon.utils;

namespace jmpcoon.model.entities
{
    public class UnmodifiableEntity : IUnmodifiableEntity
    {
        private const string WRONG_CONSTRUCTOR = "Wrong constructor used, use the other one instead";

        private readonly IEntity innerEntity;

        public UnmodifiableEntity(DynamicEntity entity)
        {
            innerEntity = entity.RequireNonNull();
            Dynamic = true;
            PowerUpType = null;
        }

        public UnmodifiableEntity(StaticEntity entity)
        {
            if (entity.RequireNonNull().Type == EntityType.POWERUP) {
                throw new ArgumentException(WRONG_CONSTRUCTOR);
            }
            innerEntity = entity;
            Dynamic = false;
            PowerUpType = null;
        }

        public UnmodifiableEntity(PowerUp powerUp)
        {
            innerEntity = powerUp.RequireNonNull();
            Dynamic = false;
            PowerUpType = powerUp.PowerUpType;
        }

        public (double X, double Y) Position => innerEntity.Position;

        public BodyShape Shape => innerEntity.Shape;

        public double Angle => innerEntity.Angle;

        public EntityType Type => innerEntity.Type;

        public EntityState State => innerEntity.State;

        public (double Width, double Height) Dimensions => innerEntity.Dimensions;

        public bool Dynamic { get; }

        public (double X, double Y) Velocity => innerEntity.Velocity;

        public PowerUpType? PowerUpType { get; }

        public override string ToString() => innerEntity.ToString();

        public override bool Equals(object obj) => obj is UnmodifiableEntity entity 
                                                   && EqualityComparer<IEntity>.Default.Equals(innerEntity, entity.innerEntity);

        public override int GetHashCode() => 548418112 + EqualityComparer<IEntity>.Default.GetHashCode(innerEntity);
    }
}
