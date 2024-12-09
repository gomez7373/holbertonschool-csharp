using System;

class Program
{
    static void Main(string[] args)
    {
        char[] letters = { 'a', 'A', 'Q', 'h', '9', 'B', 'g' };

        foreach (char c in letters)
        {
            if (Character.IsLower(c))
                Console.WriteLine($"{c} is lowercase");
            else
                Console.WriteLine($"{c} is uppercase");
        }
    }
}

