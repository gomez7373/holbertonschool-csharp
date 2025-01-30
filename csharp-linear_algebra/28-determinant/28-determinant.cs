using System;

class MatrixMath
{
    public static double Determinant(double[,] matrix)
    {

        int rows = matrix.GetLength(0);
        if (!MatrixCheck(matrix))
            return -1;

        if (rows == 2)
            return Math.Round(matrix[0,0] * matrix[1,1] - matrix[1,0] * matrix[0,1], 2);

        double a, b, c, d, e, f, g, h, i;
        a = matrix[0,0];
        b = matrix[0,1];
        c = matrix[0,2];
        d = matrix[1,0];
        e = matrix[1,1];
        f = matrix[1,2];
        g = matrix[2,0];
        h = matrix[2,1];
        i = matrix[2,2];
        return Math.Round(a*(e*i - f*h)- b*(d*i- f*g) + c*(d*h- e*g), 2);
    }

    private static bool MatrixCheck(double[,] matrix)
    {
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);
        return (rows == 2 && cols == 2) || (rows == 3 && cols == 3);
    }
}