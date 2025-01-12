using System.Collections.Generic;

namespace Text
{
    /// <summary>
    /// Contains methods for string analysis.
    /// </summary>
    public static class Str
    {
        /// <summary>
        /// Finds the index of the first non-repeating character in a string.
        /// </summary>
        /// <param name="s">Input string.</param>
        /// <returns>Index of first unique character, or -1 if none exists.</returns>
        public static int UniqueChar(string s)
        {
            if (string.IsNullOrEmpty(s))
                return -1;

            Dictionary<char, int> charCounts = new Dictionary<char, int>();
            for (int i = 0; i < s.Length; i++)
            {
                if (charCounts.ContainsKey(s[i]))
                    charCounts[s[i]]++;
                else
                    charCounts[s[i]] = 1;
            }

            for (int i = 0; i < s.Length; i++)
            {
                if (charCounts[s[i]] == 1)
                    return i;
            }

            return -1;
        }
    }
}
