using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Objs generic class that implements IEnumerable<T>
/// </summary>
/// <typeparam name="T">The type parameter</typeparam>
class Objs<T> : IEnumerable<T>
{
    List<T> collection = new List<T>();

    /// <summary>
    /// Adds an object to the list
    /// </summary>
    public void Add(T obj)
    {
        collection.Add(obj);
    }

    /// <summary>
    /// Returns an enumerator that iterates through the collection
    /// </summary>
    public IEnumerator<T> GetEnumerator()
    {
        return collection.GetEnumerator();
    }

    /// <summary>
    /// Non-generic version required by IEnumerable
    /// </summary>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}

