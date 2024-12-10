﻿using System;

class Array
{
    public static int[] CreatePrint(int size)
    {

        if (size < 0)
        {
            Console.WriteLine("Size cannot be negative");
            return null;
        }

        int[] myArray = new int[size];

        if (size == 0)
        {
            Console.Write("\n");
            return myArray;
        }

        for (int i = 0; i < size; i++)
        {
            Console.Write("{0}{1}", i, i != size - 1 ? " " : "");
            myArray[i] = i; 
        }
        Console.Write("\n");
        return myArray;
    }
}