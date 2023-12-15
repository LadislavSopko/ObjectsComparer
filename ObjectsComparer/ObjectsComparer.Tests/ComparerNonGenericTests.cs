using System;
using System.Linq;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using ObjectsComparer.Tests.TestClasses;

namespace ObjectsComparer.Tests
{
    [TestFixture]
    public class ComparerNonGenericTests
    {
        [Test]
        public void PropertyEquality()
        {
            var a1 = new A { IntProperty = 10, DateTimeProperty = new DateTime(2017, 1, 1), Property3 = 5 };
            var a2 = new A { IntProperty = 10, DateTimeProperty = new DateTime(2017, 1, 1), Property3 = 8 };
            var comparer = new Comparer();

            var isEqual = comparer.Compare(a1, a2);

            Assert.That(isEqual);
        }

        [Test]
        public void PropertyInequality()
        {
            var date1 = new DateTime(2017, 1, 1);
            var date2 = new DateTime(2017, 1, 2);
            var a1 = new A { IntProperty = 10, DateTimeProperty = date1 };
            var a2 = new A { IntProperty = 8, DateTimeProperty = date2 };
            var comparer = new Comparer();

            var differences = comparer.CalculateDifferences(a1, a2).ToList();

            CollectionAssert.IsNotEmpty(differences);
            ClassicAssert.AreEqual("IntProperty", differences[0].MemberPath);
            ClassicAssert.AreEqual("10", differences[0].Value1);
            ClassicAssert.AreEqual("8", differences[0].Value2);
            ClassicAssert.AreEqual("DateTimeProperty", differences[1].MemberPath);
            // ReSharper disable once SpecifyACultureInStringConversionExplicitly
            ClassicAssert.AreEqual(date1.ToString(), differences[1].Value1);
            // ReSharper disable once SpecifyACultureInStringConversionExplicitly
            ClassicAssert.AreEqual(date2.ToString(), differences[1].Value2);
        }
    }
}
