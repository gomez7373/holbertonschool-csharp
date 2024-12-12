using System;
using System.Collections.Generic;

class MyQueue
{
    public static Queue<string> Info(Queue<string> aQueue, string newItem, string search)
    {
        int sum = aQueue.Count;

        Console.WriteLine("Number of items: " + sum);

        if (sum == 0)
            Console.WriteLine("Queue is empty");
        else
            Console.WriteLine("First item: " + aQueue.Peek());

        aQueue.Enqueue(newItem);

        bool search_result = aQueue.Contains(search);

        Console.WriteLine("Queue contains \"" + search + "\": " + search_result);

        if (search_result)
        {
            string temp = "";

            while (temp != search)
                temp = aQueue.Dequeue();
        }

        return aQueue;
    }
}
