﻿using System;
using System.Collections.Generic;

class MyStack
{
    public static Stack<string> Info(Stack<string> aStack, string newItem, string search)
    {
        int sum = aStack.Count;
        bool search_result = aStack.Contains(search);

        Console.WriteLine("Number of items: " + sum);

        if (sum == 0)
            Console.WriteLine("Stack is empty");
        else
            Console.WriteLine("Top item: " + aStack.Peek());

        Console.WriteLine("Stack contains \"" + search + "\": " + search_result);

        if (search_result)
        {
            string temp = "";

            while (temp != search)
                temp = aStack.Pop();
        }

        aStack.Push(newItem);

        return aStack;
    }
}
