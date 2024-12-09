﻿using System;

class Program
{
    static void Main()
    {
        for (char c = 'a'; c <= 'z'; c++)
        {
            if (c == 'q' || c == 'e')
            {
                continue;
            }
            else
            {
                Console.Write(c);
            }
        }
    }
}