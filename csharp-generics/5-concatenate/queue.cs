using System;

/// <summary> Generic Queue class </summary>
public class Queue<T>
{
    /// <summary> Node class to hold queue data </summary>
    public class Node
    {
        /// <summary> Value stored in the node </summary>
        public T value;

        /// <summary> Reference to the next node </summary>
        public Node next;

        /// <summary> Constructor that sets the value of the node </summary>
        public Node(T value)
        {
            this.value = value;
            this.next = null;
        }
    }

    /// <summary> First node in the queue </summary>
    private Node head;

    /// <summary> Last node in the queue </summary>
    private Node tail;

    /// <summary> Number of nodes in the queue </summary>
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

    /// <summary> Returns the first value and removes it from the queue </summary>
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

    /// <summary>
    /// Concatenates all values if type is string or char
    /// </summary>
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
