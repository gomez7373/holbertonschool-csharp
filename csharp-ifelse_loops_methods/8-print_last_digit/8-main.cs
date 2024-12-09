using System;

class Program
{
    static void Main(string[] args)
    {
        int r;

        Number.PrintLastDigit(98);
        Console.WriteLine();
        Number.PrintLastDigit(0);
        Console.WriteLine();
        r = Number.PrintLastDigit(-1024);
        Console.WriteLine();
        Console.WriteLine(r);
    }
}

