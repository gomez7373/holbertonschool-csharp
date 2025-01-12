using NUnit.Framework;
using Text;

namespace TextTests
{
    public class StrTests
    {
        [Test]
        public void CamelCase_InputFive_ResultFive()
        {
            string input = "bofLeTestDrivenDevelopment";

            int result = Str.CamelCase(input);

            Assert.AreEqual(5, result);
        }

        [Test]
        public void CamelCase_Null_ResultZero()
        {
            string input = null;

            int result = Str.CamelCase(input);

            Assert.AreEqual(0, result);
        }

        [Test]
        public void CamelCase_Empty_ResultFive()
        {
            string input = "";

            int result = Str.CamelCase(input);

            Assert.AreEqual(0, result);
        }
    }
}