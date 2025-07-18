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

/// <summary> Interface for collectable objects. </summary>
public interface ICollectable
{
    /// <summary> Indicates if the object is collected. </summary>
    bool isCollected { get; set; }

    /// <summary> Collect method. </summary>
    void Collect();
}

/// <summary> Key class that inherits from Base and ICollectable. </summary>
public class Key : Base, ICollectable
{
    /// <summary> Indicates if the key has been collected. </summary>
    public bool isCollected { get; set; }

    /// <summary> Constructor for Key object. </summary>
    public Key(string name = "Key", bool isCollected = false)
    {
        this.name = name;
        this.isCollected = isCollected;
    }

    /// <summary> Collect the key. </summary>
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

