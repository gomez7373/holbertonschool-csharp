using NUnit.Framework;
using MyMath;

namespace MyMath.Tests
{
    [TestFixture]
    public class OperationsTests
    {
        [Test]
        public void Add_TwoPositiveNumbers_ReturnsCorrectSum()
        {
            Assert.AreEqual(5, Operations.Add(2, 3));
        }

        [Test]
        public void Add_PositiveAndNegativeNumbers_ReturnsCorrectSum()
        {
            Assert.AreEqual(1, Operations.Add(3, -2));
        }

        [Test]
        public void Add_TwoNegativeNumbers_ReturnsCorrectSum()
        {
            Assert.AreEqual(-5, Operations.Add(-2, -3));
        }

        [Test]
        public void Add_ZeroAndNumber_ReturnsNumber()
        {
            Assert.AreEqual(3, Operations.Add(0, 3));
        }
    }
}
