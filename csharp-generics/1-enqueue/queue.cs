using System;

/// <summary>
/// Generic Queue class
/// </summary>
public class Queue<T>
{
    /// <summary>
    /// Node class to hold queue data
    /// </summary>
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

    /// <summary>
    /// Adds an item to the queue
    /// </summary>
    public void Enqueue(T value)
    {
        Node newNode = new Node(value);

        if (head == null)
        {
            head = tail = newNode;
        }
        else
        {
            tail.next = newNode;
            tail = newNode;
        }

        count++;
    }

    /// <summary>
    /// Returns the number of elements in the queue
    /// </summary>
    public int Count()
    {
        return count;
    }
}

