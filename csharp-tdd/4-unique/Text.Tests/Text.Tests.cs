using NUnit.Framework;
using Text;

namespace Text.Tests
{
    [TestFixture]
    public class StrTests
    {
        [Test]
        public void UniqueChar_ValidString_ReturnsIndex()
        {
            Assert.AreEqual(0, Str.UniqueChar("abcdef"));
            Assert.AreEqual(4, Str.UniqueChar("swiss"));
        }

        [Test]
        public void UniqueChar_NoUniqueChar_ReturnsNegativeOne()
        {
            Assert.AreEqual(-1, Str.UniqueChar("aabbcc"));
        }

        [Test]
        public void UniqueChar_EmptyString_ReturnsNegativeOne()
        {
            Assert.AreEqual(-1, Str.UniqueChar(""));
        }

        [Test]
        public void UniqueChar_SingleCharacterString_ReturnsZero()
        {
            Assert.AreEqual(0, Str.UniqueChar("a"));
        }
    }
}
