using System;

class Program
{
    static void Main(string[] args)
    {
        Random rndm = new Random();
        int number = rndm.Next(-10000, 10000);
        int last_number = number % 10;

        if (last_number > 5)
        {
            Console.WriteLine($"The last digit of {number} is {last_number} and is greater than 5");
        }
        else if (last_number < 6 && last_number != 0)
        {
            Console.WriteLine($"The last digit of {number} is {last_number} and is less than 6 and not 0");
        }
        else
        {
            Console.WriteLine($"The last digit of {number} is {last_number} and is 0");
        }
    }
}