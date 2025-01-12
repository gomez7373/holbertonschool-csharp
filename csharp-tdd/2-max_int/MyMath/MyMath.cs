using System.Collections.Generic;

namespace MyMath
{
    /// <summary>
    /// Contains methods for list operations.
    /// </summary>
    public static class Operations
    {
        /// <summary>
        /// Returns the maximum integer in a list.
        /// </summary>
        /// <param name="nums">List of integers.</param>
        /// <returns>Maximum integer, or 0 if the list is empty.</returns>
        public static int Max(List<int> nums)
        {
            if (nums == null || nums.Count == 0)
                return 0;

            int max = nums[0];
            foreach (int num in nums)
            {
                if (num > max)
                    max = num;
            }
            return max;
        }
    }
}
