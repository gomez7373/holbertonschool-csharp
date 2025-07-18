using System;

/// <summary>Abstract class Base</summary>
public abstract class Base
{
    /// <summary>Name of the object</summary>
    public string name { get; set; }

    /// <summary>Returns a string representation of the object</summary>
    public override string ToString()
    {
        return $"{name} is a {this.GetType().Name}";
    }
}

/// <summary>Interface for interactive behavior</summary>
public interface IInteractive
{
    /// <summary>Interact with the object</summary>
    void Interact();
}

/// <summary>Interface for breakable behavior</summary>
public interface IBreakable
{
    /// <summary>Durability of the object</summary>
    int durability { get; set; }

    /// <summary>Break the object</summary>
    void Break();
}

/// <summary>Interface for collectable behavior</summary>
public interface ICollectable
{
    /// <summary>Whether the object has been collected</summary>
    bool isCollected { get; set; }

    /// <summary>Collect the object</summary>
    void Collect();
}

/// <summary>Class that implements all interfaces</summary>
public class TestObject : Base, IInteractive, IBreakable, ICollectable
{
    /// <summary>Durability implementation</summary>
    public int durability { get; set; }

    /// <summary>isCollected implementation</summary>
    public bool isCollected { get; set; }

    /// <summary>Interact implementation</summary>
    public void Interact() { }

    /// <summary>Break implementation</summary>
    public void Break() { }

    /// <summary>Collect implementation</summary>
    public void Collect() { }
}

