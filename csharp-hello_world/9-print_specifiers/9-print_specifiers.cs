using System;

class Program
{
    static void Main(string[] args)
    {
        double percent = 75.5345;
        double currency = 98765.4321;
        Console.WriteLine($"Percent: {percent:F2}%");
        Console.WriteLine($"Currency: {currency:C2}");
    }
}

