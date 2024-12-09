using System;

class Program
{
    static void Main()
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = i + 1; j < 10; j++)
            {
                if (i == 8 && j == 9)
                    Console.WriteLine($"{i}{j}");
                else
                    Console.Write($"{i}{j}, ");
            }
        }
    }
}