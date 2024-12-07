using System;

class Program
{
    static void Main(string[] args)
    {
        Random rnd = new Random();
        int number = rnd.Next(-10000, 10000);
        if (number > 0)
            Console.WriteLine($"{number} is positive");
        else if (number < 0)
            Console.WriteLine($"{number} is negative");
        else
            Console.WriteLine($"{number} is zero");
    }
}

