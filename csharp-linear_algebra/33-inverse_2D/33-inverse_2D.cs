using System;

class MatrixMath
{
    public static double[,] Inverse2D(double[,] matrix)
    {
        if (matrix.GetLength(0) != 2 || matrix.GetLength(1) != 2)
            return new double[,] {{-1}};

        double det = matrix[0,0] * matrix[1,1] - matrix[1,0] * matrix[0,1];

        if (det == 0)
            return new double[,] {{-1}};

        double[,] invert = new double[,] {{matrix[1,1] / det, -matrix[0,1] / det}, {-matrix[1,0] / det, matrix[0,0] / det}};

        for (int i = 0; i < 2; i++)
            for (int j = 0; j < 2; j++)
                invert[i, j] = Math.Round(invert[i, j], 2);

        return invert;
    }
}