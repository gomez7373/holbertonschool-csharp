using System;
using System.Collections.Generic;

namespace Text
{
    /// <summary>
    /// Str class
    /// </summary>
    public class Str
    {
        /// <summary>
        /// Find the first non-repeating character in a string
        /// </summary>
        /// <param name="s">string to search in</param>
        /// <returns> index of first unique char, or -1 if none/null/empty</returns>
        public static int UniqueChar(string s)
        {
            if (s == null || s == "")
                return -1;
            
            Dictionary<char, int> charCount = new Dictionary<char, int>();

            foreach (char character in s)
            {
                if (charCount.ContainsKey(character))
                    charCount[character]++;
                else
                    charCount[character] = 1;
            }

            for (int index = 0; index < s.Length; index++)
            {
                if (charCount[s[index]] == 1)
                    return index;
            }

            return -1;
        }
    }
}