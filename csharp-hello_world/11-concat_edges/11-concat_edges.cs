using System;

class Program
{
    static void Main(string[] args)
    {
        string str = "C# is an object-oriented programming language.";
        str = str.Substring(8, 12) + str.Substring(28, 12) + str.Substring(0, 2);
        Console.WriteLine(str);
    }
}

