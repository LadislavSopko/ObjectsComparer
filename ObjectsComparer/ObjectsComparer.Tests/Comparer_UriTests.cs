using System;
using System.Linq;
using NUnit.Framework;

namespace ObjectsComparer.Tests
{
    [TestFixture]
    public class Comparer_UriTests
    {
        [Test]
        public void Equality()
        {
            var a1 = new { MyUri = new Uri("https://www.google.com/") };
            var a2 = new { MyUri = new Uri("https://www.google.com/") };
            var comparer = new Comparer();

            var isEqual = comparer.Compare(a1, a2);

            Assert.That(isEqual);
        }

        [Test]
        public void Inequality()
        {
            var a1 = new { MyUri = new Uri("https://www.google.com/") };
            var a2 = new { MyUri = new Uri("https://www.yahoo.com/") };
            var comparer = new Comparer();

            var isEqual = comparer.Compare(a1, a2, out var differencesEnum);
            var differences = differencesEnum.ToList();

            Assert.That(!isEqual);
            CollectionAssert.IsNotEmpty(differences);
            ClassicAssert.AreEqual(1, differences.Count);
            ClassicAssert.AreEqual("MyUri", differences.First().MemberPath);
        }
    }
}