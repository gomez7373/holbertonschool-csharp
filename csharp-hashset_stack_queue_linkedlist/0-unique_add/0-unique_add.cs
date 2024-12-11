using System;
using System.Collections.Generic;

public class List
{
    public static int Sum(List<int> myList)
    {
        HashSet<int> uniqueIntegers = new HashSet<int>(myList);
        
        int integerSum = 0;
        foreach (int integer in uniqueIntegers)
        {
            integerSum += integer;
        }
        return integerSum;
    }
}
