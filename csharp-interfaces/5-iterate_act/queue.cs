using System;
using System.Collections.Generic;

/// <summary> Abstract base class. </summary>
public abstract class Base
{
    /// <summary> Object name. </summary>
    public string name { get; set; }

    /// <summary> String representation of the object. </summary>
    public override string ToString()
    {
        return $"{this.name} is a {this.GetType().Name}";
    }
}

/// <summary> Interface for interactive objects. </summary>
public interface IInteractive
{
    /// <summary> Interact method. </summary>
    void Interact();
}

/// <summary> Interface for breakable objects. </summary>
public interface IBreakable
{
    /// <summary> Durability property. </summary>
    int durability { get; set; }

    /// <summary> Break method. </summary>
    void Break();
}

/// <summary> Interface for collectable objects. </summary>
public interface ICollectable
{
    /// <summary> isCollected property. </summary>
    bool isCollected { get; set; }

    /// <summary> Collect method. </summary>
    void Collect();
}

/// <summary> Door class implementing IInteractive. </summary>
public class Door : Base, IInteractive
{
    /// <summary> Constructor, sets name. </summary>
    public Door(string name = "Door")
    {
        this.name = name;
    }

    /// <summary> Interact method implementation. </summary>
    public void Interact()
    {
        Console.WriteLine($"You try to open the {name}. It's locked.");
    }
}

/// <summary> Decoration class implementing IInteractive and IBreakable. </summary>
public class Decoration : Base, IInteractive, IBreakable
{
    /// <summary> Durability of the object. </summary>
    public int durability { get; set; }

    /// <summary> Whether it's a quest item. </summary>
    public bool isQuestItem;

    /// <summary> Constructor for Decoration. </summary>
    public Decoration(string name = "Decoration", int durability = 1, bool isQuestItem = false)
    {
        if (durability <= 0)
            throw new Exception("Durability must be greater than 0");

        this.name = name;
        this.durability = durability;
        this.isQuestItem = isQuestItem;
    }

    /// <summary> Interact method. </summary>
    public void Interact()
    {
        if (durability <= 0)
            Console.WriteLine($"The {name} has been broken.");
        else if (isQuestItem)
            Console.WriteLine($"You look at the {name}. There's a key inside.");
        else
            Console.WriteLine($"You look at the {name}. Not much to see here.");
    }

    /// <summary> Break method. </summary>
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

/// <summary> Key class implementing ICollectable. </summary>
public class Key : Base, ICollectable
{
    /// <summary> Indicates whether the key has been collected. </summary>
    public bool isCollected { get; set; }

    /// <summary> Constructor for Key. </summary>
    public Key(string name = "Key", bool isCollected = false)
    {
        this.name = name;
        this.isCollected = isCollected;
    }

    /// <summary> Collect method. </summary>
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

/// <summary> Class for executing actions on room objects. </summary>
public class RoomObjects
{
    /// <summary>
    /// Iterates through a list of Base objects and performs actions
    /// based on implemented interface type.
    /// </summary>
    public static void IterateAction(List<Base> roomObjects, Type type)
    {
        foreach (Base obj in roomObjects)
        {
            if (type == typeof(IInteractive) && obj is IInteractive)
                ((IInteractive)obj).Interact();

            if (type == typeof(IBreakable) && obj is IBreakable)
                ((IBreakable)obj).Break();

            if (type == typeof(ICollectable) && obj is ICollectable)
                ((ICollectable)obj).Collect();
        }
    }
}

