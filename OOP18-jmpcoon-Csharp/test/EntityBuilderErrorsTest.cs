using jmpcoon.model.entities;
using jmpcoon.model.physics;
using jmpcoon.model.world;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace jmpcoon.test
{
    [TestFixture]
    public class EntityBuilderErrorsTest
    {
        private const double WORLD_WIDTH = 8;
        private const double WORLD_HEIGHT = 4.5;
        private const double STD_ANGLE = Math.PI / 4;
        private const string NOT_BUILDABLE_MSG = "The building shouldn't have been allowed";
        private static readonly (double X, double Y) STD_POSITION = (X: WORLD_WIDTH / 2, Y: WORLD_HEIGHT / 2);
        private static readonly IPhysicalFactory FACTORY = new PhysicalFactory();
        private static readonly (double Width, double Height) STD_CIRCULAR_DIMENSIONS = (Width: WORLD_WIDTH / 10,
                                                                                         Height: WORLD_HEIGHT / 10);
        private static readonly BodyShape STD_SHAPE = BodyShape.CIRCLE;

        [TestCaseSource("Builders")]
        public void CreationWithOnlyPositionFail(IEntityBuilder<IEntity> builder)
        {
            builder.SetPosition(STD_POSITION);
            Assert.Throws<InvalidOperationException>(() => builder.Build(), NOT_BUILDABLE_MSG);
        }

        [TestCaseSource("Builders")]
        public void CreationWithOnlyFactoryFail(IEntityBuilder<IEntity> builder)
        {
            builder.SetFactory(FACTORY);
            Assert.Throws<InvalidOperationException>(() => builder.Build(), NOT_BUILDABLE_MSG);
        }

        [TestCaseSource("Builders")]
        public void CreationWithOnlyDimensionsFail(IEntityBuilder<IEntity> builder)
        {
            builder.SetDimensions(STD_CIRCULAR_DIMENSIONS);
            Assert.Throws<InvalidOperationException>(() => builder.Build(), NOT_BUILDABLE_MSG);
        }

        [TestCaseSource("Builders")]
        public void CreationWithOnlyShapeFail(IEntityBuilder<IEntity> builder)
        {
            builder.SetShape(STD_SHAPE);
            Assert.Throws<InvalidOperationException>(() => builder.Build(), NOT_BUILDABLE_MSG);
        }

        [TestCaseSource("Builders")]
        public void CreationWithOnlyAngleFail(IEntityBuilder<IEntity> builder)
        {
            builder.SetAngle(STD_ANGLE);
            Assert.Throws<InvalidOperationException>(() => builder.Build(), NOT_BUILDABLE_MSG);
        }

        [TestCaseSource("Builders")]
        public void CreationWithOnlyPowerUpTypeFail(IEntityBuilder<IEntity> builder)
        {
            builder.SetPowerUpType(PowerUpType.INVINCIBILITY);
            Assert.Throws<InvalidOperationException>(() => builder.Build(), NOT_BUILDABLE_MSG);
        }

        [TestCaseSource("Builders")]
        public void CreationWithOnlyWalkingRangeFail(IEntityBuilder<IEntity> builder)
        {
            builder.SetWalkingRange(1.0);
            Assert.Throws<InvalidOperationException>(() => builder.Build(), NOT_BUILDABLE_MSG);
        }

        [TestCaseSource("Builders")]
        public void CreationWithOnlyModifiableWorldFail(IEntityBuilder<IEntity> builder)
        {
            builder.SetWorld((IWorld)new WorldFactory().Create());
            Assert.Throws<InvalidOperationException>(() => builder.Build(), NOT_BUILDABLE_MSG);
        }

        public static ICollection<IEntityBuilder<IEntity>> Builders()
        {
            return new List<IEntityBuilder<IEntity>> { EntityBuilderUtils.GetLadderBuilder(),
                                                       EntityBuilderUtils.GetPlatformBuilder(),
                                                       EntityBuilderUtils.GetEnemyGeneratorBuilder(),
                                                       EntityBuilderUtils.GetPowerUpBuilder(),
                                                       EntityBuilderUtils.GetWalkingEnemyBuilder(),
                                                       EntityBuilderUtils.GetPlayerBuilder(),
                                                       EntityBuilderUtils.GetRollingEnemyBuilder() };
        }
    }
}
