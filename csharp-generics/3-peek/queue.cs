﻿using System;

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
        /// <summary>
        /// The value stored in the node
        /// </summary>
        public T value;

        /// <summary>
        /// Reference to the next node
        /// </summary>
        public Node next;

        /// <summary>
        /// Node constructor that sets the value
        /// </summary>
        /// <param name="value">The value to store</param>
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
    /// Adds a value to the end of the queue
    /// </summary>
    /// <param name="value">The value to add</param>
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

    /// <summary>
    /// Returns the number of elements in the queue
    /// </summary>
    /// <returns>The count of elements</returns>
    public int Count()
    {
        return count;
    }

    /// <summary>
    /// Returns the type name of the generic parameter
    /// </summary>
    public string CheckType()
    {
        return typeof(T).ToString();
    }

    /// <summary>
    /// Removes and returns the first value in the queue
    /// </summary>
    /// <returns>The value removed, or default(T) if empty</returns>
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

    /// <summary>
    /// Returns the first value in the queue without removing it
    /// </summary>
    /// <returns>The value at the head, or default(T) if empty</returns>
    public T Peek()
    {
        if (head == null)
        {
            Console.WriteLine("Queue is empty");
            return default(T);
        }

        return head.value;
    }
}

