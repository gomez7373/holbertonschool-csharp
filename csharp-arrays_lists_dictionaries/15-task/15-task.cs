using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        int[,] myMatrix = new int[,] { {0, 1, 2}, {3, 4, 5}, {6, 7, 8} };\n        Matrix.Square(myMatrix);
    }
}
