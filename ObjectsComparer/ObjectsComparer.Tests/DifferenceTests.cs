using NUnit.Framework;

namespace ObjectsComparer.Tests
{
    [TestFixture]
    public class DifferenceTests
    {
        [Test]
        public void DifferenceToString()
        {
            var difference = new Difference("Property1", "12345", "12346");

            var toString = difference.ToString();

            Assert.That(toString.Contains("Property1"));
            Assert.That(toString.Contains("12345"));
            Assert.That(toString.Contains("12346"));
        }
    }
}