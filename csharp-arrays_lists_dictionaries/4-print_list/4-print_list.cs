using System;
using System.Collections.Generic;

class List
{
    public static List<int> CreatePrint(int size)
    {
        if (size < 0)
        {
            Console.WriteLine("Size cannot be negative");
            return null;
        }

        List<int> newList = new List<int>();

        if (size == 0)
        {
            Console.WriteLine();
            return newList;
        }

        for (int i = 0; i < size; i++)
        {
            Console.Write("{0}{1}", i, i != size - 1 ? " ": "" );
            newList.Add(i);
        }
        Console.WriteLine();
        return newList;
    }
}