using System;

/// <summary> Abstract base class. </summary>
public abstract class Base
{
    /// <summary> Name of the object. </summary>
    public string name { get; set; }

    /// <summary> Override ToString to show object type. </summary>
    public override string ToString()
    {
        return $"{this.name} is a {this.GetType().Name}";
    }
}

/// <summary> Interface for interactive objects. </summary>
public interface IInteractive
{
    /// <summary> Defines interaction behavior. </summary>
    void Interact();
}

/// <summary> Interface for breakable objects. </summary>
public interface IBreakable
{
    /// <summary> Durability of the object. </summary>
    int durability { get; set; }

    /// <summary> Defines break behavior. </summary>
    void Break();
}

/// <summary> Decoration class that inherits Base, IInteractive, and IBreakable. </summary>
public class Decoration : Base, IInteractive, IBreakable
{
    /// <summary> Durability of the decoration. </summary>
    public int durability { get; set; }

    /// <summary> Indicates if it's a quest item. </summary>
    public bool isQuestItem { get; set; }

    /// <summary> Constructor for Decoration object. </summary>
    public Decoration(string name = "Decoration", int durability = 1, bool isQuestItem = false)
    {
        if (durability <= 0)
            throw new Exception("Durability must be greater than 0");

        this.name = name;
        this.durability = durability;
        this.isQuestItem = isQuestItem;
    }

    /// <summary> Interact with the decoration. </summary>
    public void Interact()
    {
        if (durability <= 0)
        {
            Console.WriteLine($"The {name} has been broken.");
        }
        else if (isQuestItem)
        {
            Console.WriteLine($"You look at the {name}. There's a key inside.");
        }
        else
        {
            Console.WriteLine($"You look at the {name}. Not much to see here.");
        }
    }

    /// <summary> Break the decoration. </summary>
    public void Break()
    {
        durability--;

        if (durability > 0)
        {
            Console.WriteLine($"You hit the {name}. It cracks.");
        }
        else if (durability == 0)
        {
            Console.WriteLine($"You smash the {name}. What a mess.");
        }
        else
        {
            Console.WriteLine($"The {name} is already broken.");
        }
    }
}

