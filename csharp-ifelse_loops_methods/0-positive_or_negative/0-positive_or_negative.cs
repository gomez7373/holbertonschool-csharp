﻿using System;

class Program
{
    static void Main(string[] args)
    {
        Random rndm = new Random();
        int number = rndm.Next(-10, 10);
        string result;
        switch (number)
        {
            case var a when number > 0:
            result = "is positive";
            break;

            case var a when number == 0:
            result = "is zero";
            break;

            default:
            result = "is negative";
            break;
        }
        Console.WriteLine($"{number} {result}");
        }
    }