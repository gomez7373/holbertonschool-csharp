using NUnit.Framework;
using MyMath;

namespace MyMath.Tests
{
    [TestFixture]
    public class MatrixTests
    {
        [Test]
        public void Divide_ValidMatrixAndNumber_ReturnsCorrectResult()
        {
            int[,] matrix = { { 4, 8 }, { 16, 20 } };
            int[,] expected = { { 2, 4 }, { 8, 10 } };
            Assert.AreEqual(expected, Matrix.Divide(matrix, 2));
        }

        [Test]
        public void Divide_NumIsZero_ReturnsNull()
        {
            int[,] matrix = { { 4, 8 }, { 16, 20 } };
            Assert.IsNull(Matrix.Divide(matrix, 0));
        }

        [Test]
        public void Divide_NullMatrix_ReturnsNull()
        {
            Assert.IsNull(Matrix.Divide(null, 2));
        }
    }
}
