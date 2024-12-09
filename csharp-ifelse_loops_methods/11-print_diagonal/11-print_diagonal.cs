using System;

class Line
{
    public static void PrintDiagonal(int length)
    {
        if (length <= 0)
        {
            Console.WriteLine();
        }
        else
        {
            for (int x = 0; x < length; x++)
            {
                Console.Write("\\");
            }
            Console.WriteLine();
        }
    }
}