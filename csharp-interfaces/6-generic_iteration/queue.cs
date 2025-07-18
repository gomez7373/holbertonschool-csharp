using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Abstract class representing a base object.
/// </summary>
public abstract class Base
{
    /// <summary> Name of the object. </summary>
    public string name { get; set; }

    /// <summary>
    /// Overrides ToString() to return a formatted string.
    /// </summary>
    /// <returns>Formatted string "<name> is a <type>"</returns>
    public override string ToString()
    {
        return $"{name} is a {this.GetType().Name}";
    }
}

/// <summary> Interface for interactive objects. </summary>
public interface IInteractive
{
    /// <summary> Defines Interact method. </summary>
    void Interact();
}

/// <summary> Interface for breakable objects. </summary>
public interface IBreakable
{
    /// <summary> Durability of the object. </summary>
    int durability { get; set; }

    /// <summary> Defines Break method. </summary>
    void Break();
}

/// <summary> Interface for collectable objects. </summary>
public interface ICollectable
{
    /// <summary> Indicates if the object is collected. </summary>
    bool isCollected { get; set; }

    /// <summary> Defines Collect method. </summary>
    void Collect();
}

/// <summary> Door class that inherits from Base and implements IInteractive. </summary>
public class Door : Base, IInteractive
{
    /// <summary>
    /// Constructor. If no name is provided, defaults to "Door".
    /// </summary>
    public Door(string name = "Door")
    {
        this.name = name;
    }

    /// <summary> Interact implementation. </summary>
    public void Interact()
    {
        Console.WriteLine($"You try to open the {name}. It's locked.");
    }
}

/// <summary> Decoration class that inherits from Base and implements IInteractive and IBreakable. </summary>
public class Decoration : Base, IInteractive, IBreakable
{
    /// <summary> Whether the decoration is a quest item. </summary>
    public bool isQuestItem { get; set; }

    /// <summary> Durability of the decoration. </summary>
    public int durability { get; set; }

    /// <summary>
    /// Constructor with parameters.
    /// </summary>
    public Decoration(string name = "Decoration", int durability = 1, bool isQuestItem = false)
    {
        if (durability <= 0)
            throw new Exception("Durability must be greater than 0");

        this.name = name;
        this.durability = durability;
        this.isQuestItem = isQuestItem;
    }

    /// <summary> Interact implementation. </summary>
    public void Interact()
    {
        if (durability <= 0)
            Console.WriteLine($"The {name} has been broken.");
        else if (isQuestItem)
            Console.WriteLine($"You look at the {name}. There's a key inside.");
        else
            Console.WriteLine($"You look at the {name}. Not much to see here.");
    }

    /// <summary> Break implementation. </summary>
    public void Break()
    {
        durability--;

        if (durability > 0)
            Console.WriteLine($"You hit the {name}. It cracks.");
        else if (durability == 0)
            Console.WriteLine($"You smash the {name}. What a mess.");
        else
            Console.WriteLine($"The {name} is already broken.");
    }
}

/// <summary> Key class that inherits from Base and implements ICollectable. </summary>
public class Key : Base, ICollectable
{
    /// <summary> Whether the key is collected. </summary>
    public bool isCollected { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public Key(string name = "Key", bool isCollected = false)
    {
        this.name = name;
        this.isCollected = isCollected;
    }

    /// <summary> Collect implementation. </summary>
    public void Collect()
    {
        if (!isCollected)
        {
            isCollected = true;
            Console.WriteLine($"You pick up the {name}.");
        }
        else
        {
            Console.WriteLine($"You have already picked up the {name}.");
        }
    }
}

/// <summary>
/// Generic collection class that implements IEnumerable
/// </summary>
public class Objs<T> : IEnumerable<T>
{
    private List<T> items = new List<T>();

    /// <summary>
    /// Adds an item to the collection.
    /// </summary>
    public void Add(T item)
    {
        items.Add(item);
    }

    /// <summary>
    /// Returns a generic enumerator.
    /// </summary>
    public IEnumerator<T> GetEnumerator()
    {
        return items.GetEnumerator();
    }

    /// <summary>
    /// Returns a non-generic enumerator.
    /// </summary>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}

