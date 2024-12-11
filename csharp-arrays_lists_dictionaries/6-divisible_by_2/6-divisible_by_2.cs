using System;
using System.Collections.Generic;

class List
{
    public static List<bool> DivisibleBy2(List<int> myList)
    {
        List<bool> division = new List<bool>();

        foreach (int i in myList)
        {
            if (i % 2 == 0)
                division.Add(true);
            else
                division.Add(false);
        }
        return division;
    }
}
