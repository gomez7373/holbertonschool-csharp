using System;

class Program
{
    static void Main(string[] args)
    {
        Random random = new Random();
        int number = random.Next(-10, 11); // Generates a random number between -10 and 10 (inclusive)

        Console.Write($"{number} is ");
        
        if (number > 0)
        {
            Console.WriteLine("positive");
        }
        else if (number == 0)
        {
            Console.WriteLine("zero");
        }
        else
        {
            Console.WriteLine("negative");
        }
    }
}

