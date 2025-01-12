namespace Text
{
    /// <summary>
    /// Contains methods for string analysis.
    /// </summary>
    public static class Str
    {
        /// <summary>
        /// Counts the number of words in a camelCase string.
        /// </summary>
        /// <param name="s">CamelCase string.</param>
        /// <returns>Number of words in the string.</returns>
        public static int CamelCase(string s)
        {
            if (string.IsNullOrEmpty(s))
                return 0;

            int wordCount = 1; // Starts with 1 assuming valid camelCase
            foreach (char c in s)
            {
                if (char.IsUpper(c))
                    wordCount++;
            }

            return wordCount;
        }
    }
}
