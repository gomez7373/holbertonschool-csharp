using NUnit.Framework;
using MyMath;

namespace MyMath.Tests
{
    /// <summary>
    /// Tests for the Operations class.
    /// </summary>
    [TestFixture]
    public class OperationsTests
    {
        /// <summary>
        /// Tests adding two positive integers.
        /// </summary>
        [Test]
        public void Add_TwoPositiveNumbers_ReturnsSum()
        {
            Assert.AreEqual(5, Operations.Add(2, 3));
        }

        /// <summary>
        /// Tests adding a positive and a negative integer.
        /// </summary>
        [Test]
        public void Add_PositiveAndNegativeNumber_ReturnsSum()
        {
            Assert.AreEqual(-1, Operations.Add(2, -3));
        }

        /// <summary>
        /// Tests adding two negative integers.
        /// </summary>
        [Test]
        public void Add_TwoNegativeNumbers_ReturnsSum()
        {
            Assert.AreEqual(-5, Operations.Add(-2, -3));
        }

        /// <summary>
        /// Tests adding zero and another integer.
        /// </summary>
        [Test]
        public void Add_ZeroAndNumber_ReturnsNumber()
        {
            Assert.AreEqual(7, Operations.Add(0, 7));
        }
    }
}