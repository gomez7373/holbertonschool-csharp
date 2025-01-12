using System;

namespace Text
{
    /// <summary>
    /// Class to count the number of words in a camel case string.
    /// </summary>
    public class Str
    {
        /// <summary>
        /// Count the number of words in a camel case string.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int CamelCase(string s)
        {
            if (s == null || s == "")
                return 0;
            int count = 1;

            foreach (char character in s)
            {
                if (char.IsUpper(character))
                    count++;
            }

            return count;
        }
    }
}