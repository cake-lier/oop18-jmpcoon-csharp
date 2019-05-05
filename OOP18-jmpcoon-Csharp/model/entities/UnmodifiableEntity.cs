using System;
using System.Collections.Generic;

namespace jmpcoon.model.entities
{
    public class UnmodifiableEntity : IUnmodifiableEntity
    {
        private const string WRONG_CONSTRUCTOR = "Wrong constructor used, use the other one instead";

        private readonly IEntity innerEntity;

        public UnmodifiableEntity(IDynamicEntity entity)
        {
            innerEntity = entity.RequireNonNull();
            Dynamic = true;
            PowerUpType = null;
        }

        public UnmodifiableEntity(IStaticEntity entity)
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

        public Tuple<double, double> Position => innerEntity.Position;

        public BodyShape Shape => innerEntity.Shape;

        public double Angle => innerEntity.Angle;

        public EntityType Type => innerEntity.Type;

        public EntityState State => innerEntity.State;

        public Tuple<double, double> Dimensions => innerEntity.Dimensions;

        public bool Dynamic { get; }

        public Tuple<double, double> Velocity => innerEntity.Velocity;

        public PowerUpType? PowerUpType { get; }

        public override bool Equals(object obj) => Equals(obj as UnmodifiableEntity);

        public bool Equals(UnmodifiableEntity other) 
            => other != null && EqualityComparer<IEntity>.Default.Equals(innerEntity, other.innerEntity);

        public override int GetHashCode() => 548418112 + EqualityComparer<IEntity>.Default.GetHashCode(innerEntity);
    }
}
