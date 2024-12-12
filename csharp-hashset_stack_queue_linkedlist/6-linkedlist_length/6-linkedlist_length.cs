using System;
using System.Collections.Generic;
class LList
{
    public static int Length(LinkedList<int> myLList)
    {
        int sum = 0;

        foreach(int value in myLList)
            sum++;

        return sum;
    }
}
