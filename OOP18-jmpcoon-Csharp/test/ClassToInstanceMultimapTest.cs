using jmpcoon.model;
using Microsoft.Collections.Extensions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace jmpcoon.test
{
    [TestFixture]
    public class ClassToInstanceMultimapTest
    {
        private const string NOT_EMPTY = "The multimap should be empty";
        private const string EMPTY = "The multimap should not be empty";
        private const string NOT_SAME_NUMBER_ELEMENTS = "There isn't the same number of elements as presumed";
        private const string NOT_SAME_ELEMENTS = "Some or all the elements previously inserted are not the same as before";
        private const string PUT_OR_VALUES_FAILED = "The 'put' method didn't insert elements correctly or the 'values' "
                                                    + "method didn't return the correct values for this multimap";
        private const string NOT_SAME_NUMBER_VALUES = "There aren't the same number of values as presumed";
        private const string NOT_SAME_VALUES = "The values aren't the same as before";
        private const string GET_FAILED = "The 'get' method didn't obtain the two instances of Double present";
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
        private const string NO_REPLACED_DOUBLE = "There isn't the newly replaced Double instance inside the multimap";

        private readonly NullReferenceException nullException;
        private readonly InvalidCastException firstCastException;
        private readonly InvalidCastException secondCastException;
        private ClassToInstanceMultimap<Exception> testMultimap;

        public ClassToInstanceMultimapTest()
        {
            nullException = new NullReferenceException();
            firstCastException = new InvalidCastException("0");
            secondCastException = new InvalidCastException("1");
        }

        [SetUp]
        public void InitializeMultimap()
        {
            testMultimap = new ClassToInstanceMultimap<Exception>();
            testMultimap.PutInstance(typeof(NullReferenceException), nullException);
            testMultimap.PutInstance(typeof(InvalidCastException), firstCastException);
            testMultimap.PutInstance(typeof(InvalidCastException), secondCastException);
        }

        [Test]
        public void EmptyMultimapTest()
        {
            var newMultimap = new ClassToInstanceMultimap<Exception>();
            Assert.AreEqual(0, newMultimap.Count, NOT_EMPTY);
            var suppliedMultimap = new ClassToInstanceMultimap<Exception>(new MultiValueDictionary<Type, Exception>());
            Assert.AreEqual(0, suppliedMultimap.Count, NOT_EMPTY);
        }

        [Test]
        public void InstancesInsertionTest()
        {
            Assert.AreEqual(3, testMultimap.Count, NOT_SAME_NUMBER_ELEMENTS);
            Assert.AreNotEqual(0, testMultimap.Count, EMPTY);
            var nullExceptions = testMultimap.GetInstances<NullReferenceException>(typeof(NullReferenceException)).ToList();
            Assert.AreEqual(1, nullExceptions.Count, NOT_SAME_NUMBER_ELEMENTS);
            Assert.IsTrue(nullExceptions.TrueForAll(e => new List<NullReferenceException> { nullException }.Contains(e)));
            Assert.AreEqual(0, testMultimap.GetInstances<OutOfMemoryException>(typeof(OutOfMemoryException)), MORE_INSTANCES);
        }

        [Test]
        public void MultimapInsertionTest()
        {
            var multimap = new ClassToInstanceMultimap<Exception>
            {
                { typeof(NullReferenceException), nullException },
                { typeof(InvalidCastException), firstCastException },
                { typeof(InvalidCastException), secondCastException }
            };
            var values = multimap.Values.SelectMany(e => e).ToList();
            Assert.IsTrue(values.TrueForAll(e =>
                                                new List<Exception> { nullException, firstCastException, secondCastException }
                                                .Contains(e)),
                          PUT_OR_VALUES_FAILED);
            Assert.AreEqual(3, values.Count, NOT_SAME_NUMBER_VALUES);
            var casts = multimap[typeof(InvalidCastException)].ToList();
            Assert.IsTrue(casts.TrueForAll(e =>
                                               new List<Exception> { firstCastException, secondCastException }.Contains(e)));
            Assert.AreEqual(2, casts.Count, NOT_SAME_NUMBER_ELEMENTS);
            multimap.AddRange(typeof(StackOverflowException),
                              new List<StackOverflowException> { new StackOverflowException("0"),
                                                                 new StackOverflowException("1") });
            Assert.AreEqual(2, multimap[typeof(StackOverflowException)].Count, NOT_SAME_NUMBER_ELEMENTS);
        }

        [Test]
        public void WrongTypeInsertionTest() 
            => Assert.Throws(typeof(InvalidCastException),
                             () => testMultimap.Add(typeof(InvalidCastException), new NullReferenceException("0")));

        [Test]
        public void WrongTypeMultipleInsertionTest()
        {
            Assert.Throws(typeof(InvalidCastException),
                          () => testMultimap.AddRange(typeof(NullReferenceException), 
                                                      new List<Exception> { new StackOverflowException("0"),
                                                                            new StackOverflowException("1") }));
        }

        [Test]
        public void RemovalTest()
        {
            testMultimap.Remove(typeof(NullReferenceException), nullException);
            Assert.AreEqual(2, testMultimap.Count, NOT_SAME_NUMBER_ELEMENTS);
            var values = testMultimap.Values.SelectMany(e => e).ToList();
            Assert.IsTrue(new List<InvalidCastException> { firstCastException, secondCastException }
                          .TrueForAll(e => values.Contains(e)),
                          NOT_SAME_ELEMENTS);
            testMultimap.PutInstance(typeof(NullReferenceException), new NullReferenceException("-1"));
            testMultimap.Remove(typeof(InvalidCastException));
            Assert.AreEqual(0, testMultimap.GetInstances<InvalidCastException>(typeof(InvalidCastException)).Count,
                            MORE_INSTANCES);
            testMultimap.Clear();
            Assert.AreEqual(0, testMultimap.Count, NOT_EMPTY);
        }

        [Test]
        public void MapAndMultimapGettersTest()
        {
            Assert.AreEqual(2, testMultimap.Keys.ToList().Count, NOT_SAME_NUMBER_DISTINCT_KEYS);
            Assert.IsTrue(new List<Type> { typeof(NullReferenceException), typeof(InvalidCastException) }
                          .TrueForAll(e => testMultimap.Keys.ToList().Contains(e)), NOT_SAME_DISTINCT_KEYS);
            Assert.AreEqual(3, testMultimap.SelectMany(pair => pair.Value
                                                                   .Select(v => new KeyValuePair<Type, Exception>(pair.Key, v)))
                                           .Count(),
                            NOT_SAME_NUMBER_ENTRIES);
            Assert.IsTrue(testMultimap.Keys.All(e => testMultimap.SelectMany(pair => pair.Value
                                                                 .Select(v => new KeyValuePair<Type, Exception>(pair.Key, v)))
                                                                 .Select(entry => entry.Key)
                                                                 .ToList()
                                                                 .Contains(e)),
                          NOT_SAME_KEYS_IN_ENTRIES);
            Assert.IsTrue(testMultimap.Values
                                      .SelectMany(e => e)
                                      .All(e => testMultimap.SelectMany(pair => pair.Value
                                                                                    .Select(v => new KeyValuePair<Type, Exception>(pair.Key, v)))
                                                            .Select(entry => entry.Value)
                                                            .ToList()
                                                            .Contains(e)),
                          NOT_SAME_VALUES_IN_ENTRIES);
            testMultimap.SelectMany(pair => pair.Value.Select(v => new KeyValuePair<Type, Exception>(pair.Key, v)))
                        .ToList()
                        .ForEach(entry => entry.Key.IsAssignableFrom(entry.Value.GetType()));
        }

        [Test]
        public void ContainsMethodsTest()
        {
            Assert.IsTrue(testMultimap.ContainsKey(typeof(NullReferenceException)), NO_INSTANCES);
            Assert.IsFalse(testMultimap.ContainsKey(typeof(StackOverflowException)), MORE_INSTANCES);
            Assert.IsTrue(testMultimap.ContainsValue(firstCastException), NO_INSTANCES);
            Assert.IsFalse(testMultimap.ContainsValue(new NullReferenceException("-1")), NO_NEW_ELEMENTS);
            testMultimap.SelectMany(pair => pair.Value.Select(v => new KeyValuePair<Type, Exception>(pair.Key, v)))
                        .ToList()
                        .ForEach(entry =>
                        {
                            Assert.IsTrue(testMultimap.Contains(entry.Key, entry.Value), NO_SAME_ENTRIES);
                        });
            Assert.IsFalse(testMultimap.Contains(typeof(NullReferenceException), new NullReferenceException("-1")),
                           NO_NEW_ELEMENTS);
        }
    }

}
