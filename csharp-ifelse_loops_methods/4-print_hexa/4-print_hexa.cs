using System;

class Program
{
    static void Main(string[] args)
    {
        for (int i = 0; i <= 98; i++)
        {
            Console.WriteLine($"{i} = 0x{i:X}");
        }
    }
}