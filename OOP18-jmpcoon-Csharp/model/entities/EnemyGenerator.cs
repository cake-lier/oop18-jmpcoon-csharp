using jmpcoon.model.physics;
using jmpcoon.model.world;

namespace jmpcoon.model.entities
{
    public class EnemyGenerator : StaticEntity
    {
        private static readonly (double Width, double Height) ROLLING_ENEMY_DIMENSIONS = (0.23, 0.23);
        private const int DELTA = 280;

        private readonly IModifiableWorld world;
        private int count;

        public EnemyGenerator(StaticPhysicalBody body, IModifiableWorld world) : base(body)
        {
            this.world = world;
            count = -1;
        }

        public override EntityType Type => EntityType.ENEMY_GENERATOR;

        public void OnTimeAdvanced()
        {
            if (CheckTime())
            {
                world.AddGeneratedRollingEnemy(CreateCompleteRollingEnemy());
            }
        }

        private RollingEnemy CreateCompleteRollingEnemy()
        {
            var enemy = CreateRollingEnemy();
            enemy.ApplyImpulse();
            return enemy;
        }

        private bool CheckTime()
        {
            count++;
            return count % DELTA == 0;
        }

        private RollingEnemy CreateRollingEnemy() => EntityBuilderUtils.GetRollingEnemyBuilder()
                                                                       .SetDimensions(ROLLING_ENEMY_DIMENSIONS)
                                                                       .SetAngle(0.0)
                                                                       .SetPosition(Position)
                                                                       .SetShape(BodyShape.CIRCLE)
                                                                       .Build();
    }
}
