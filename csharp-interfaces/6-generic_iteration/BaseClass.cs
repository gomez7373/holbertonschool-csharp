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

