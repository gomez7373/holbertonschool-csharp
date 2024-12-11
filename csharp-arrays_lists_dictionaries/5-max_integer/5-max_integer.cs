﻿using System;
using System.Collections.Generic;

class List
{
    public static int MaxInteger(List<int> myList)
    {
        if (myList == null || myList.Count == 0)
        {
            Console.WriteLine("List is empty");
            return -1;
        }

        int max = myList[0];
        foreach (int i in myList)
        {
            if (i > max)
                max = i;
        }
        return max;
    }
}
