using System;
using System.Collections.Generic;

class List
{
    public static int Sum(List<int> myList)
    {
        HashSet<int> uniqueNumbers = new HashSet<int>(myList);
        int sum = 0;
        foreach (int number in uniqueNumbers)
        {
            sum += number;
        }
        return sum;
    }
}