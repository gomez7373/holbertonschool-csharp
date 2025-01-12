namespace MyMath
{
    /// <summary>
    /// Contains matrix operations.
    /// </summary>
    public static class Matrix
    {
        /// <summary>
        /// Divides all elements of a matrix by a number.
        /// </summary>
        /// <param name="matrix">The matrix.</param>
        /// <param name="num">The divisor.</param>
        /// <returns>New matrix with divided elements, or null if division is not possible.</returns>
        public static int[,] Divide(int[,] matrix, int num)
        {
            if (matrix == null || num == 0)
            {
                System.Console.WriteLine("Num cannot be 0");
                return null;
            }

            int[,] result = new int[matrix.GetLength(0), matrix.GetLength(1)];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    result[i, j] = matrix[i, j] / num;
                }
            }
            return result;
        }
    }
}
