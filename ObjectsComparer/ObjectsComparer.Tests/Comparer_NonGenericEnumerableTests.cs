using System.Collections;
using System.Linq;
using NUnit.Framework;
using ObjectsComparer.Tests.TestClasses;

namespace ObjectsComparer.Tests
{
    [TestFixture]
    public class ComparerNonGenericEnumerableTests
    {
        [Test]
        public void Equality()
        {
            var a1 = new A { NonGenericEnumerable = new ArrayList { new B { Property1 = "Str1" }, new B { Property1 = "Str2" } } };
            var a2 = new A { NonGenericEnumerable = new ArrayList { new B { Property1 = "Str1" }, new B { Property1 = "Str2" } } };
            var comparer = new Comparer<A>();

            var isEqual = comparer.Compare(a1, a2);

            Assert.That(isEqual);
        }

        [Test]
        public void InequalityCount()
        {
            var a1 = new A { NonGenericEnumerable = new ArrayList { new B { Property1 = "Str1" }, new B { Property1 = "Str2" } } };
            var a2 = new A { NonGenericEnumerable = new ArrayList { new B { Property1 = "Str1" } } };
            var comparer = new Comparer<A>();

            var differences = comparer.CalculateDifferences(a1, a2).ToList();

            CollectionAssert.IsNotEmpty(differences);
            ClassicAssert.AreEqual("NonGenericEnumerable", differences.First().MemberPath);
            ClassicAssert.AreEqual(DifferenceTypes.NumberOfElementsMismatch, differences.First().DifferenceType);
            ClassicAssert.AreEqual("2", differences.First().Value1);
            ClassicAssert.AreEqual("1", differences.First().Value2);
        }

        [Test]
        public void InequalityProperty()
        {
            var a1 = new A { NonGenericEnumerable = new ArrayList { new B { Property1 = "Str1" }, new B { Property1 = "Str2" } } };
            var a2 = new A { NonGenericEnumerable = new ArrayList { new B { Property1 = "Str1" }, new B { Property1 = "Str3" } } };
            var comparer = new Comparer<A>();

            var differences = comparer.CalculateDifferences(a1, a2).ToList();

            CollectionAssert.IsNotEmpty(differences);
            ClassicAssert.AreEqual("NonGenericEnumerable[1].Property1", differences.First().MemberPath);
            ClassicAssert.AreEqual("Str2", differences.First().Value1);
            ClassicAssert.AreEqual("Str3", differences.First().Value2);
        }

        [Test]
        public void NullElementsEquality()
        {
            var a1 = new A { NonGenericEnumerable = new ArrayList { null } };
            var a2 = new A { NonGenericEnumerable = new ArrayList { null } };
            var comparer = new Comparer<A>();

            var isEqual = comparer.Compare(a1, a2);

            Assert.That(isEqual);
        }

        [Test]
        public void NullAndNotNullElementsInequality()
        {
            var a1 = new A { NonGenericEnumerable = new ArrayList { null, "Str1" } };
            var a2 = new A { NonGenericEnumerable = new ArrayList { "Str2", null } };
            var comparer = new Comparer<A>();

            var differences = comparer.CalculateDifferences(a1, a2).ToList();

            ClassicAssert.AreEqual(2, differences.Count);
            ClassicAssert.AreEqual("NonGenericEnumerable[0]", differences[0].MemberPath);
            ClassicAssert.AreEqual(string.Empty, differences[0].Value1);
            ClassicAssert.AreEqual("Str2", differences[0].Value2);
            ClassicAssert.AreEqual("NonGenericEnumerable[1]", differences[1].MemberPath);
            ClassicAssert.AreEqual("Str1", differences[1].Value1);
            ClassicAssert.AreEqual(string.Empty, differences[1].Value2);
        }

        [Test]
        public void InequalityType()
        {
            var a1 = new A { NonGenericEnumerable = new ArrayList { new B { Property1 = "Str1" }, new B { Property1 = "Str2" } } };
            var a2 = new A { NonGenericEnumerable = new ArrayList { new B { Property1 = "Str1" }, "Str3" } };
            var comparer = new Comparer<A>();

            var differences = comparer.CalculateDifferences(a1, a2).ToList();

            CollectionAssert.IsNotEmpty(differences);
            ClassicAssert.AreEqual("NonGenericEnumerable[1]", differences.First().MemberPath);
            ClassicAssert.AreEqual("ObjectsComparer.Tests.TestClasses.B", differences.First().Value1);
            ClassicAssert.AreEqual("Str3", differences.First().Value2);
        }

        [Test]
        public void DerivedClassEquality()
        {
            var a1 = new A { NonGenericEnumerableImplementation = new EnumerableImplementation(new ArrayList { new B { Property1 = "Str1" }, new B { Property1 = "Str2" } }) };
            var a2 = new A { NonGenericEnumerableImplementation = new EnumerableImplementation(new ArrayList { new B { Property1 = "Str1" }, new B { Property1 = "Str2" } }) };
            var comparer = new Comparer<A>();

            var isEqual = comparer.Compare(a1, a2);

            Assert.That(isEqual);
        }

        [Test]
        public void DerivedClassInequalityProperty()
        {
            var a1 = new A { NonGenericEnumerableImplementation = new EnumerableImplementation(new ArrayList { new B { Property1 = "Str1" }, new B { Property1 = "Str2" } }) { Property1 = "Str3" } };
            var a2 = new A { NonGenericEnumerableImplementation = new EnumerableImplementation(new ArrayList { new B { Property1 = "Str1" }, new B { Property1 = "Str2" } }) { Property1 = "Str4" } };
            var comparer = new Comparer<A>();

            var differences = comparer.CalculateDifferences(a1, a2).ToList();

            CollectionAssert.IsNotEmpty(differences);
            ClassicAssert.AreEqual("NonGenericEnumerableImplementation.Property1", differences.First().MemberPath);
            ClassicAssert.AreEqual("Str3", differences.First().Value1);
            ClassicAssert.AreEqual("Str4", differences.First().Value2);
        }

        [Test]
        public void NullAndEmptyInequality()
        {
            var a1 = new A { NonGenericEnumerable = new ArrayList() };
            var a2 = new A();
            var comparer = new Comparer<A>();

            var differences = comparer.CalculateDifferences(a1, a2).ToList();

            CollectionAssert.IsNotEmpty(differences);
            ClassicAssert.AreEqual("NonGenericEnumerable[]", differences.First().MemberPath);
            ClassicAssert.AreEqual("System.Collections.ArrayList", differences.First().Value1);
            ClassicAssert.AreEqual(string.Empty, differences.First().Value2);
        }

        [Test]
        public void NullAndEmptyEquality()
        {
            var a1 = new A { NonGenericEnumerable = new ArrayList() };
            var a2 = new A();
            var comparer = new Comparer<A>(new ComparisonSettings { EmptyAndNullEnumerablesEqual = true });

            var isEqual = comparer.Compare(a1, a2);

            Assert.That(isEqual);
        }
    }
}
