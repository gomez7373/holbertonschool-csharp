using System;

/// <summary> Generic Queue class </summary>
public class Queue<T>
{
    /// <summary> Node class to hold queue data </summary>
    public class Node
    {
        public T value;
        public Node next;

        public Node(T value)
        {
            this.value = value;
            this.next = null;
        }
    }

    private Node head;
    private Node tail;
    private int count = 0;

    /// <summary> Adds a value to the end of the queue </summary>
    public void Enqueue(T value)
    {
        Node newNode = new Node(value);
        if (head == null)
            head = tail = newNode;
        else
        {
            tail.next = newNode;
            tail = newNode;
        }
        count++;
    }

    /// <summary> Returns the number of elements in the queue </summary>
    public int Count()
    {
        return count;
    }

    /// <summary> Removes the first node and returns its value </summary>
    public T Dequeue()
    {
        if (head == null)
        {
            Console.WriteLine("Queue is empty");
            return default(T);
        }

        T value = head.value;
        head = head.next;
        count--;
        if (head == null)
            tail = null;
        return value;
    }

    /// <summary> Returns the first value without removing it </summary>
    public T Peek()
    {
        if (head == null)
        {
            Console.WriteLine("Queue is empty");
            return default(T);
        }
        return head.value;
    }

    /// <summary> Prints all values in the queue from head to tail </summary>
    public void Print()
    {
        if (head == null)
        {
            Console.WriteLine("Queue is empty");
            return;
        }

        Node current = head;
        while (current != null)
        {
            Console.WriteLine(current.value);
            current = current.next;
        }
    }

    /// <summary> Concatenates values if type is string or char </summary>
    public string Concatenate()
    {
        if (head == null)
        {
            Console.WriteLine("Queue is empty");
            return null;
        }

        if (typeof(T) != typeof(string) && typeof(T) != typeof(char))
        {
            Console.WriteLine("Concatenate is for a queue of Strings or Chars only.");
            return null;
        }

        Node current = head;
        string result = "";

        while (current != null)
        {
            result += current.value;
            if (typeof(T) == typeof(string) && current.next != null)
                result += " ";
            current = current.next;
        }

        return result;
    }
}

