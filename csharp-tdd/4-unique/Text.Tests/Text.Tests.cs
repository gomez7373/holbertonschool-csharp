using NUnit.Framework;
using Text;

namespace Text.Tests
{
    public class StrTests
    {
        [Test]
        public void UniqueChar_Index2_Return2()
        {
            string input = "aabcc";

            int result = Str.UniqueChar(input);

            Assert.AreEqual(2, result);
        }

        [Test]
        public void UniqueChar_None_ReturnMinus()
        {
            string input = "aabbcc";

            int result = Str.UniqueChar(input);

            Assert.AreEqual(-1, result);
        }

        [Test]
        public void UniqueChar_Empty_ReturnMinus()
        {
            string input = "";

            int result = Str.UniqueChar(input);

            Assert.AreEqual(-1, result);
        }

        [Test]
        public void UniqueChar_Null_ReturnMinus()
        {
            string input = null;

            int result = Str.UniqueChar(input);

            Assert.AreEqual(-1, result);
        }

        [Test]
        public void UniqueChar_Index0_Return0()
        {
            string input = "a";

            int result = Str.UniqueChar(input);

            Assert.AreEqual(0, result);
        }
    }
}