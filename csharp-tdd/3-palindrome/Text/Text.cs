using System.Text.RegularExpressions;

namespace Text
{
    /// <summary>
    /// Contains methods for string analysis.
    /// </summary>
    public static class Str
    {
        /// <summary>
        /// Determines if a string is a palindrome.
        /// </summary>
        /// <param name="s">Input string.</param>
        /// <returns>True if the string is a palindrome, false otherwise.</returns>
        public static bool IsPalindrome(string s)
        {
            if (string.IsNullOrEmpty(s))
                return true;

            s = Regex.Replace(s.ToLower(), @"[^a-z0-9]", "");
            char[] chars = s.ToCharArray();
            System.Array.Reverse(chars);
            return s == new string(chars);
        }
    }
}
