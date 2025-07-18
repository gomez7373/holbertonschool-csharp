using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Generic class that implements IEnumerable<T>
/// </summary>
public class Objs<T> : IEnumerable<T>
{
    private List<T> _objs = new List<T>();

    /// <summary>
    /// Adds an item of type T to the list
    /// </summary>
    public void Add(T item)
    {
        _objs.Add(item);
    }

    /// <summary>
    /// Returns an enumerator for foreach iteration
    /// </summary>
    public IEnumerator<T> GetEnumerator()
    {
        return _objs.GetEnumerator();
    }

    /// <summary>
    /// Non-generic enumerator implementation (required by IEnumerable)
    /// </summary>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}

