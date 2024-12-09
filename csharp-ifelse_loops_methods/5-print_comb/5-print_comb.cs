using System;

class Program
{
    static void Main()
    {
        for (int number = 0; number <= 99; number++)
        {
            if (number == 99)
            {
                Console.Write($"{number:D2}\n");
            }
            else
            {
                Console.Write($"{number:D2}, ");
            }
        }
    }

}