using NUnit.Framework;
using Text;

namespace Text.Tests
{
    [TestFixture]
    public class StrTests
    {
        [Test]
        public void CamelCase_ValidCamelCaseString_ReturnsWordCount()
        {
            Assert.AreEqual(5, Str.CamelCase("thisIsCamelCaseString"));
            Assert.AreEqual(1, Str.CamelCase("word"));
        }

        [Test]
        public void CamelCase_EmptyString_ReturnsZero()
        {
            Assert.AreEqual(0, Str.CamelCase(""));
        }

        [Test]
        public void CamelCase_SingleWordString_ReturnsOne()
        {
            Assert.AreEqual(1, Str.CamelCase("word"));
        }
    }
}
