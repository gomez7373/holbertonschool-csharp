using System;

class MatrixMath
{
    public static double[,] Multiply(double[,] matrix1, double[,] matrix2)
    {
        int rows = matrix1.GetLength(0);
        int cols = matrix1.GetLength(1);
        int colsB = matrix2.GetLength(1);

        if (cols != matrix2.GetLength(0))
            return new double[,] {{-1}};

        double[,] result = new double[rows, colsB];

        for (int i = 0; i < rows; i++)
            for (int j = 0; j < colsB; j++)
            {
                result[i, j] = 0;
                for (int k = 0; k < cols; k++)
                    result[i, j] += matrix1[i, k] * matrix2[k, j];
            }

        return result;

    }
}
