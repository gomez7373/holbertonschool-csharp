using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Generic class that stores a collection of objects of type T
/// </summary>
public class Objs<T> : IEnumerable<T>
{
    private List<T> _objects = new List<T>();

    /// <summary>
    /// Adds an item to the collection
    /// </summary>
    public void Add(T item)
    {
        _objects.Add(item);
    }

    /// <summary>
    /// Returns an enumerator that iterates through the collection
    /// </summary>
    public IEnumerator<T> GetEnumerator()
    {
        return _objects.GetEnumerator();
    }

    /// <summary>
    /// Returns an enumerator that iterates through the collection (non-generic)
    /// </summary>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}
