using System;
using System.Collections.Generic;
using System.Linq;
using jmpcoon.model;
using jmpcoon.model.entities;
using jmpcoon.model.physics;
using Microsoft.Collections.Extensions;
using NUnit.Framework;

namespace jmpcoon.test
{
    [TestFixture]
    public class ClassToInstanceMultimapTest
    {
        private const double WORLD_WIDTH = 8;
        private const double WORLD_HEIGHT = 4.5;
        private const double STD_RANGE = 1.00;
        private const double STD_ANGLE = Math.PI / 4;
        private const double PRECISION = 0.001;
        private const string NOT_EMPTY = "The multimap should be empty";
        private const string EMPTY = "The multimap should not be empty";
        private const string NOT_SAME_NUMBER_ELEMENTS = "There isn't the same number of elements as presumed";
        private const string NOT_SAME_ELEMENTS = "Some or all the elements previously inserted are not the same as before";
        private const string ADD_OR_VALUES_FAILED = "The 'Add' method didn't insert elements correctly or the 'Values' "
                                                    + "property didn't return the correct values for this multimap";
        private const string NOT_SAME_NUMBER_VALUES = "There aren't the same number of values as presumed";
        private const string NOT_SAME_VALUES = "The values aren't the same as before";
        private const string MORE_INSTANCES = "No instances of this type should be inside the multimap";
        private const string NO_INSTANCES = "The multimap should contain an instance of this type";
        private const string NOT_SAME_NUMBER_DISTINCT_KEYS = "The distinct keys aren't in the same number as presumed";
        private const string NOT_SAME_DISTINCT_KEYS = "The distinct keys aren't the same as before";
        private const string NOT_SAME_NUMBER_KEYS = "The total keys aren't in the same number as presumed";
        private const string NOT_SAME_KEYS = "The total keys aren't the same as before";
        private const string NOT_SAME_NUMBER_ENTRIES = "There isn't the same number of entries as presumed";
        private const string NO_SAME_ENTRIES = "The multimap should contain every entry which already contains";
        private const string NOT_SAME_KEYS_IN_ENTRIES = "The keys in the entries aren't the same as before";
        private const string NOT_SAME_VALUES_IN_ENTRIES = "The values in the entries aren't the same as before";
        private const string NO_NEW_ELEMENTS = "The multimap shouldn't already contain a newly generated instance";
        private const string INDEXER_FAILED = "The indexer didn't obtain the two instances of Platform present";
        private const string TRYGETVALUE_FAILED = "The 'TryGetValue' method didn't correctly get the supposedly present values";
        private const string TRYGETVALUE_SUCCEEDED = "The 'TryGetValue' method succeeded instead of failing as it was supposed";
        private readonly (double X, double Y) FST_STD_POSITION = (X: WORLD_WIDTH / 2, Y: WORLD_HEIGHT / 2);
        private readonly (double X, double Y) SND_STD_POSITION = (X: WORLD_WIDTH / 2 + 1, Y: WORLD_HEIGHT / 2 + 1);
        private readonly (double Width, double Height) FST_STD_RECTANGULAR_DIMENSIONS = (Width: WORLD_WIDTH / 10,
                                                                                     Height: WORLD_HEIGHT / 5);
        private readonly (double Width, double Height) SND_STD_RECTANGULAR_DIMENSIONS = (Width: WORLD_WIDTH / 10 + 1,
                                                                                     Height: WORLD_HEIGHT / 5 + 1);
        private readonly (double Width, double Height) STD_CIRCULAR_DIMENSIONS = (Width: WORLD_WIDTH / 15,
                                                                                  Height: WORLD_WIDTH / 15);

        private readonly IPhysicalFactory factory;
        private readonly Player player;
        private readonly Platform firstPlatform;
        private readonly Platform secondPlatform;
        private ClassToInstanceMultimap<IEntity> testMultimap;

        public ClassToInstanceMultimapTest()
        {
            factory = new PhysicalFactory();
            player = EntityBuilderUtils.GetPlayerBuilder()
                                       .SetDimensions(FST_STD_RECTANGULAR_DIMENSIONS)
                                       .SetPosition(FST_STD_POSITION)
                                       .SetFactory(factory)
                                       .SetAngle(STD_ANGLE)
                                       .SetShape(BodyShape.RECTANGLE)
                                       .Build();
            firstPlatform = EntityBuilderUtils.GetPlatformBuilder()
                                              .SetPosition(FST_STD_POSITION)
                                              .SetFactory(factory)
                                              .SetDimensions(FST_STD_RECTANGULAR_DIMENSIONS)
                                              .SetAngle(STD_ANGLE)
                                              .SetShape(BodyShape.RECTANGLE)
                                              .Build();
            secondPlatform = EntityBuilderUtils.GetPlatformBuilder()
                                               .SetPosition(SND_STD_POSITION)
                                               .SetFactory(factory)
                                               .SetDimensions(FST_STD_RECTANGULAR_DIMENSIONS)
                                               .SetAngle(STD_ANGLE)
                                               .SetShape(BodyShape.RECTANGLE)
                                               .Build();
        }

        [SetUp]
        public void InitializeMultimap()
        {
            testMultimap = new ClassToInstanceMultimap<IEntity>();
            testMultimap.PutInstance(typeof(Player), player);
            testMultimap.PutInstance(typeof(Platform), firstPlatform);
            testMultimap.PutInstance(typeof(Platform), secondPlatform);
        }

        [Test]
        public void EmptyMultimapTest()
        {
            var newMultimap = new ClassToInstanceMultimap<IEntity>();
            Assert.AreEqual(0, newMultimap.Count, NOT_EMPTY);
            var suppliedMultimap = new ClassToInstanceMultimap<IEntity>(new MultiValueDictionary<Type, IEntity>());
            Assert.AreEqual(0, suppliedMultimap.Count, NOT_EMPTY);
        }

        [Test]
        public void InstancesInsertionTest()
        {
            Assert.AreEqual(3, testMultimap.Values.SelectMany(e => e).Count(), NOT_SAME_NUMBER_ELEMENTS);
            var players = testMultimap.GetInstances<Player>(typeof(Player)).ToList();
            Assert.AreEqual(1, players.Count, NOT_SAME_NUMBER_ELEMENTS);
            var expectedPlayers = new List<Player> { player };
            Assert.IsTrue(players.All(e => expectedPlayers.Contains(e)));
            Assert.AreEqual(0, testMultimap.GetInstances<Ladder>(typeof(Ladder)).Count, MORE_INSTANCES);
        }

        [Test]
        public void MultimapInsertionTest()
        {
            var multimap = new ClassToInstanceMultimap<IEntity>
            {
                { typeof(Player), player },
                { typeof(Platform), firstPlatform },
                { typeof(Platform), secondPlatform }
            };
            var values = multimap.Values.SelectMany(e => e).ToList();
            var expectedValues = new List<IEntity> { player, firstPlatform, secondPlatform };
            Assert.IsTrue(values.All(e => expectedValues.Contains(e)), ADD_OR_VALUES_FAILED);
            Assert.AreEqual(3, values.Count, NOT_SAME_NUMBER_VALUES);
            var expectedPlatforms = new List<IEntity> { firstPlatform, secondPlatform };
            var platforms = multimap[typeof(Platform)].ToList();
            Assert.IsTrue(platforms.All(e => expectedPlatforms.Contains(e)), INDEXER_FAILED);
            Assert.AreEqual(2, platforms.Count, NOT_SAME_NUMBER_ELEMENTS);
            IReadOnlyCollection<IEntity> actualPlatforms = new List<IEntity>().AsReadOnly();
            Assert.IsTrue(testMultimap.TryGetValue(typeof(Platform), out actualPlatforms), TRYGETVALUE_FAILED);
            Assert.IsTrue(actualPlatforms.All(e => expectedPlatforms.Contains(e)), TRYGETVALUE_FAILED);
            Assert.AreEqual(2, actualPlatforms.Count, NOT_SAME_NUMBER_ELEMENTS);
            IReadOnlyCollection<IEntity> actualRolling = new List<IEntity>().AsReadOnly();
            Assert.IsFalse(testMultimap.TryGetValue(typeof(RollingEnemy), out actualRolling), TRYGETVALUE_SUCCEEDED);
            Assert.IsNull(actualRolling, TRYGETVALUE_SUCCEEDED);
            multimap.AddRange(typeof(RollingEnemy), CreateCoupleRollingEnemies());
            Assert.AreEqual(2, multimap[typeof(RollingEnemy)].Count, NOT_SAME_NUMBER_ELEMENTS);
        }

        [Test]
        public void WrongTypeInsertionTest()
            => Assert.Throws<InvalidCastException>(() => testMultimap.Add(typeof(Platform), CreateSecondPlayer()));

        [Test]
        public void WrongTypeMultipleInsertionTest() 
            => Assert.Throws<InvalidCastException>(() => testMultimap.AddRange(typeof(Player), CreateCoupleRollingEnemies()));

        [Test]
        public void WrongTypeInsertionFromOtherMapTest()
        {
            var extMultimap = new MultiValueDictionary<Type, IEntity>
            {
                { typeof(Player), EntityBuilderUtils.GetPlatformBuilder()
                                                    .SetPosition(SND_STD_POSITION)
                                                    .SetFactory(factory)
                                                    .SetDimensions(SND_STD_RECTANGULAR_DIMENSIONS)
                                                    .SetAngle(STD_ANGLE)
                                                    .SetShape(BodyShape.RECTANGLE)
                                                    .Build() }
            };
            Assert.Throws<InvalidCastException>(() => new ClassToInstanceMultimap<IEntity>(extMultimap));
        }

        [Test]
        public void RemovalTest()
        {
            testMultimap.Remove(typeof(Player), player);
            Assert.AreEqual(2, testMultimap.Values.SelectMany(e => e).Count(), NOT_SAME_NUMBER_ELEMENTS);
            var values = testMultimap.Values.SelectMany(e => e).ToList();
            Assert.IsTrue(new List<Platform> { firstPlatform, secondPlatform }.All(e => values.Contains(e)), NOT_SAME_ELEMENTS);
            testMultimap.PutInstance(typeof(Player), CreateSecondPlayer());
            testMultimap.Remove(typeof(Platform));
            Assert.AreEqual(0, testMultimap.GetInstances<Platform>(typeof(Platform)).Count, MORE_INSTANCES);
            testMultimap.Clear();
            Assert.AreEqual(0, testMultimap.Count, NOT_EMPTY);
        }

        [Test]
        public void MapAndMultimapGettersTest()
        {
            var multimapEnumerator = testMultimap.GetEnumerator();
            var entries = new List<(Type Key, IEntity Value)>();
            while (multimapEnumerator.MoveNext())
            {
                var entry = multimapEnumerator.Current;
                entries.AddRange(entry.Value.Select(value => (entry.Key, Value: value)));
            }
            Assert.AreEqual(3, entries.Select(pair => pair.Key).Count(), NOT_SAME_NUMBER_KEYS);
            var keys = new List<Type> { typeof(Player), typeof(Platform), typeof(Platform) };
            Assert.IsTrue(keys.All(e => entries.Select(pair => pair.Key).Contains(e)), NOT_SAME_KEYS);
            Assert.AreEqual(2, testMultimap.Keys.Count(), NOT_SAME_NUMBER_DISTINCT_KEYS);
            Assert.IsTrue(new List<Type> { typeof(Player), typeof(Platform) }
                          .All(e => testMultimap.Keys.ToList().Contains(e)), NOT_SAME_DISTINCT_KEYS);
            Assert.AreEqual(3, entries.Count, NOT_SAME_NUMBER_ENTRIES);
            Assert.IsTrue(testMultimap.Keys.All(e => entries.Select(entry => entry.Key).Contains(e)), NOT_SAME_KEYS_IN_ENTRIES);
            Assert.IsTrue(testMultimap.Values
                                      .SelectMany(e => e)
                                      .All(e => entries.Select(entry => entry.Value).Contains(e)), NOT_SAME_VALUES_IN_ENTRIES);
            entries.ForEach(entry => entry.Key.IsAssignableFrom(entry.Value.GetType()));
        }

        [Test]
        public void ContainsMethodsTest()
        {
            Assert.IsTrue(testMultimap.ContainsKey(typeof(Player)), NO_INSTANCES);
            Assert.IsFalse(testMultimap.ContainsKey(typeof(RollingEnemy)), MORE_INSTANCES);
            Assert.IsTrue(testMultimap.ContainsValue(firstPlatform), NO_INSTANCES);
            var secondPlayer = CreateSecondPlayer();
            Assert.IsFalse(testMultimap.ContainsValue(secondPlayer), NO_NEW_ELEMENTS);
            var multimapEnumerator = testMultimap.GetEnumerator();
            var entries = new List<(Type Key, IEntity Value)>();
            while (multimapEnumerator.MoveNext())
            {
                var entry = multimapEnumerator.Current;
                entries.AddRange(entry.Value.Select(value => (entry.Key, Value: value)));
            }
            entries.ForEach(entry => Assert.IsTrue(testMultimap.Contains(entry.Key, entry.Value), NO_SAME_ENTRIES));
            Assert.IsFalse(testMultimap.Contains(typeof(Player), secondPlayer), NO_NEW_ELEMENTS);
        }

        private ICollection<RollingEnemy> CreateCoupleRollingEnemies() 
            => new List<RollingEnemy> { EntityBuilderUtils.GetRollingEnemyBuilder()
                                                          .SetDimensions(STD_CIRCULAR_DIMENSIONS)
                                                          .SetPosition(FST_STD_POSITION)
                                                          .SetFactory(factory)
                                                          .SetAngle(STD_ANGLE)
                                                          .SetShape(BodyShape.CIRCLE)
                                                          .Build(),
                                        EntityBuilderUtils.GetRollingEnemyBuilder()
                                                          .SetDimensions(STD_CIRCULAR_DIMENSIONS)
                                                          .SetPosition(SND_STD_POSITION)
                                                          .SetFactory(factory)
                                                          .SetAngle(STD_ANGLE)
                                                          .SetShape(BodyShape.CIRCLE)
                                                          .Build()
                                      };

        private Player CreateSecondPlayer() => EntityBuilderUtils.GetPlayerBuilder()
                                                                 .SetDimensions(SND_STD_RECTANGULAR_DIMENSIONS)
                                                                 .SetPosition(SND_STD_POSITION)
                                                                 .SetFactory(factory)
                                                                 .SetAngle(STD_ANGLE)
                                                                 .SetShape(BodyShape.RECTANGLE)
                                                                 .Build();
    }
}
