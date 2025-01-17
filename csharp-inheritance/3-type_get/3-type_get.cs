using System;

class Obj
{
    public static void Print(object myObj)
    {
        Type t = myObj.GetType();
        Console.WriteLine($"{t.Name} Properties:");
        foreach (var attribute in t.GetProperties())
            Console.WriteLine(attribute.Name);
        Console.WriteLine($"{t.Name} Methods:");
        foreach (var attribute in t.GetMethods())
            Console.WriteLine(attribute.Name);
    }
}