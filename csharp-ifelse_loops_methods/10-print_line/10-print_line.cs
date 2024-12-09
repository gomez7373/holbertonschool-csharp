using System;

class Line
{
    public static void PrintLine(int length)
    {
        if (length <= 0)
        {
            Console.WriteLine();
            return;
        }
        Console.WriteLine(new string('_', length));
    }
}
