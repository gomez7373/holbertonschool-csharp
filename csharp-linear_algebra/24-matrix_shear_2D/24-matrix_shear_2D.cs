using System;

class MatrixMath
{
    public static double[,] Shear2D(double[,] matrix, char direction, double factor)
    {
        if ((matrix.GetLength(0) != 2 || matrix.GetLength(1) != 2) || (direction != 'x' && direction != 'y'))
            return new double[,] {{-1}};

        int k = 0;
        double[,] result = new double[2,2];

        if (direction == 'y')
            k = 1;

        for (int i = 0; i < 2; i++)
        {
            result[i, 0] = matrix[i, 0];
            result[i, 1] = matrix[i, 1];
            result[i, k] += matrix[i, 1 - k] * factor;
        }


        return result;
    }
}
