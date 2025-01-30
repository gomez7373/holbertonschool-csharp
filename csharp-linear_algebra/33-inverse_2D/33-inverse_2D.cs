using System;

class MatrixMath
{
    public static double[,] Inverse2D(double[,] matrix)
    {
        // Ensure the matrix is 2x2
        if (matrix.GetLength(0) != 2 || matrix.GetLength(1) != 2)
        {
            return new double[,] { { -1 } };
        }

        // Calculate the determinant
        double determinant = (matrix[0, 0] * matrix[1, 1]) - (matrix[0, 1] * matrix[1, 0]);

        // If the determinant is 0, the matrix is non-invertible
        if (determinant == 0)
        {
            return new double[,] { { -1 } };
        }

        // Calculate the inverse
        double inverseDet = 1 / determinant;
        double[,] inverse = new double[2, 2];

        inverse[0, 0] = inverseDet * matrix[1, 1];
        inverse[0, 1] = -inverseDet * matrix[0, 1];
        inverse[1, 0] = -inverseDet * matrix[1, 0];
        inverse[1, 1] = inverseDet * matrix[0, 0];

        return inverse;
    }
}
