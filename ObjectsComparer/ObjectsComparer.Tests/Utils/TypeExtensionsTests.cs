using System;
using System.Collections.Generic;
using NUnit.Framework;
using ObjectsComparer.Utils;

namespace ObjectsComparer.Tests.Utils
{
    [TestFixture]
    public class TypeExtensionsTests
    {
        [Test]
        public void InheritsFromFirstTypeNull()
        {
            var result = ((Type) null).InheritsFrom(typeof(string));

            Assert.That(!result);
        }

        [Test]
        public void InheritsFromSecondTypeNull()
        {
            var result = typeof(string).InheritsFrom(null);

            Assert.That(!result);
        }

        [Test]
        public void InheritsFromEqualTypes()
        {
            var result = typeof(string).InheritsFrom(typeof(string));

            Assert.That(result);
        }

        [Test]
        public void InheritsFromEqualGenericTypes()
        {
            var result = typeof(List<string>).InheritsFrom(typeof(List<string>));

            Assert.That(result);
        }

        [Test]
        public void InheritsFromEqualGenericTypeAndGenericDefinition()
        {
            var result = typeof(List<string>).InheritsFrom(typeof(List<>));

            Assert.That(result);
        }

        [Test]
        public void InheritsFromEqualGenericTypeAndGenericInterface()
        {
            var result = typeof(List<string>).InheritsFrom(typeof(IList<>));

            Assert.That(result);
        }

        [Test]
        public void InheritsFromEqualTypeAndBaseType()
        {
            var result = typeof(string).InheritsFrom(typeof(object));

            Assert.That(result);
        }
    }
}