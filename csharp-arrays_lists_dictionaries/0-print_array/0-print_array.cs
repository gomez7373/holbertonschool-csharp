using System;

class Array
{
    public static int[] CreatePrint(int size)
    {
        if (size < 0)
        {
            Console.WriteLine("Size cannot be negative");
            return null;
        }

        int[] array = new int[size];

        for (int x = 0; x < size; x++)
        {
            array[x] = x;
            Console.Write(x);

            if (x < size - 1)
            {
                Console.Write(" ");
            }
        }

        Console.WriteLine();
        return array;
    }
}