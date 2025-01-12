using NUnit.Framework;
using Text;

namespace Text.Tests
{
    [TestFixture]
    public class StrTests
    {
        [Test]
        public void IsPalindrome_ValidPalindrome_ReturnsTrue()
        {
            Assert.IsTrue(Str.IsPalindrome("Racecar"));
        }

        [Test]
        public void IsPalindrome_ValidPalindromeWithPunctuation_ReturnsTrue()
        {
            Assert.IsTrue(Str.IsPalindrome("A man, a plan, a canal: Panama."));
        }

        [Test]
        public void IsPalindrome_EmptyString_ReturnsTrue()
        {
            Assert.IsTrue(Str.IsPalindrome(""));
        }

        [Test]
        public void IsPalindrome_NotAPalindrome_ReturnsFalse()
        {
            Assert.IsFalse(Str.IsPalindrome("Hello"));
        }
    }
}
