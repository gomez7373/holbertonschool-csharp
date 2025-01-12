using NUnit.Framework;
using MyMath;
using System.Collections.Generic;

namespace MyMath.Tests
{
    [TestFixture]
    public class OperationsTests
    {
        [Test]
        public void Max_ValidList_ReturnsMaxValue()
        {
            List<int> nums = new List<int> { 1, 2, 3, 4, 5 };
            Assert.AreEqual(5, Operations.Max(nums));
        }

        [Test]
        public void Max_EmptyList_ReturnsZero()
        {
            List<int> nums = new List<int>();
            Assert.AreEqual(0, Operations.Max(nums));
        }

        [Test]
        public void Max_NullList_ReturnsZero()
        {
            Assert.AreEqual(0, Operations.Max(null));
        }

        [Test]
        public void Max_ListWithNegativeNumbers_ReturnsMaxValue()
        {
            List<int> nums = new List<int> { -5, -1, -10 };
            Assert.AreEqual(-1, Operations.Max(nums));
        }
    }
}
