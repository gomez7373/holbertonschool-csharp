﻿using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        int[,] arrayOfNumbers = new int[5, 5];

        for (int i= 0; i < 5; i++)
        {
            for (int j=0; j < 5; j++)
                arrayOfNumbers[i, j] = 0;
        }

        arrayOfNumbers[2, 2] = 1;

        for (int i= 0; i < 5; i++)
        {
            for (int j=0; j < 5; j++)
                Console.Write("{0}{1}", arrayOfNumbers[i, j], j < 4 ? " ": "");
            Console.WriteLine();
        }
    }
}
