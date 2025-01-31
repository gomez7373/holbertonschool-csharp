using System;

class MatrixMath
{
    public static double[,] Rotate2D(double[,] matrix, double angle)
    {
        if ((matrix.GetLength(0) != 2 || matrix.GetLength(1) != 2))
            return new double[,] {{-1}};

        double cos = Math.Cos(angle);
        double sin = Math.Sin(angle);
        double[,] rotM;

        rotM = new double[,] {{ cos, sin },{ -sin, cos } };

        return Multiply(matrix, rotM);
    }

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
                result[i, j] = Math.Round(result[i, j], 2);
            }

        return result;

    }
}
