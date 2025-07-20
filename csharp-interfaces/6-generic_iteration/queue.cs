using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>Abstract base class</summary>
public abstract class Base
{
    /// <summary>Name of the object</summary>
    public string name { get; set; }

    /// <summary>Returns string representation of the object</summary>
    public override string ToString()
    {
        return $"{name} is a {this.GetType().Name}";
    }
}

/// <summary>Interface for interactive objects</summary>
public interface IInteractive
{
    /// <summary>Interact method</summary>
    void Interact();
}

/// <summary>Class representing a door</summary>
public class Door : Base, IInteractive
{
    /// <summary>Constructor for Door</summary>
    public Door(string name = "Door")
    {
        this.name = name;
    }

    /// <summary>Interact with the Door</summary>
    public void Interact()
    {
        Console.WriteLine($"You try to open the {name}. It's locked.");
    }
}

/// <summary>Class representing a decoration</summary>
public class Decoration : Base, IInteractive
{
    /// <summary>Durability of the decoration</summary>
    public int durability { get; set; }

    /// <summary>Is a quest item?</summary>
    public bool isQuestItem;

    /// <summary>Constructor</summary>
    public Decoration(string name = "Decoration", int durability = 1, bool isQuestItem = false)
    {
        if (durability <= 0)
            throw new ArgumentException("Durability must be greater than 0");
        this.name = name;
        this.durability = durability;
        this.isQuestItem = isQuestItem;
    }

    /// <summary>Interact with the decoration</summary>
    public void Interact()
    {
        if (durability <= 0)
            Console.WriteLine($"The {name} has been broken.");
        else if (isQuestItem)
            Console.WriteLine($"You look at the {name}. There's a key inside.");
        else
            Console.WriteLine($"You look at the {name}. Not much to see here.");
    }

    /// <summary>Break the decoration</summary>
    public void Break()
    {
        durability--;
        if (durability > 0)
            Console.WriteLine($"You hit the {name}. It cracks.");
        else
            Console.WriteLine($"You smash the {name}. What a mess.");
    }
}

/// <summary>Generic iterable collection</summary>
public class Objs<T> : IEnumerable<T>
{
    private List<T> _objects = new List<T>();

    /// <summary>Adds an item</summary>
    public void Add(T obj)
    {
        _objects.Add(obj);
    }

    /// <summary>Returns enumerator</summary>
    public IEnumerator<T> GetEnumerator()
    {
        return _objects.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}

