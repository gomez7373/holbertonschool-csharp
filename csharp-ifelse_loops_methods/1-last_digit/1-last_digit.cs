using System;

class Program
{
    static void Main(string[] args)
    {
        Random rndm = new Random();
        int number = rndm.Next(-10000, 10000);

        int digit = number % 10;
        if (digit > 5)
            Console.WriteLine("The last digit of {0} is {1} and is greater than 5", number, digit);
        else if (digit == 0)
            Console.WriteLine("The last digit of {0} is {1} and is 0", number, digit);
        else
            Console.WriteLine("The last digit of {0} is {1} and is less than 6 and not 0", number, digit);
    }
}
