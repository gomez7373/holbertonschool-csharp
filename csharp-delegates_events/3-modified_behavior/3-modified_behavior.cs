using System;

/// <summary> Modifier type to adjust base value for damage or healing. </summary>
public enum Modifier
{
    /// <summary> Weak effect (50% of base value) </summary>
    Weak,

    /// <summary> Base effect (100% of base value) </summary>
    Base,

    /// <summary> Strong effect (150% of base value) </summary>
    Strong
}

/// <summary> Delegate that calculates a modified value based on a Modifier enum. </summary>
public delegate float CalculateModifier(float baseValue, Modifier modifier);

/// <summary> Represents a player character with health. </summary>
public class Player
{
    private string name;
    private float hp;
    private float maxHp;

    /// <summary> Constructor to set the name and max HP of the player. </summary>
    public Player(string name = "Player", float maxHp = 100f)
    {
        this.name = name;

        if (maxHp <= 0)
        {
            Console.WriteLine("maxHp must be greater than 0. maxHp set to 100f by default.");
            this.maxHp = 100f;
        }
        else
        {
            this.maxHp = maxHp;
        }

        this.hp = this.maxHp;
    }

    /// <summary> Prints the player's current health. </summary>
    public void PrintHealth()
    {
        Console.WriteLine($"{name} has {hp} / {maxHp} health");
    }

    /// <summary> Reduces the player's HP by a damage value, then validates the new HP. </summary>
    public void TakeDamage(float damage)
    {
        if (damage < 0)
        {
            Console.WriteLine($"{name} takes 0 damage!");
            damage = 0;
        }
        else
        {
            Console.WriteLine($"{name} takes {damage} damage!");
        }

        float newHp = hp - damage;
        ValidateHP(newHp);
    }

    /// <summary> Increases the player's HP by a healing value, then validates the new HP. </summary>
    public void HealDamage(float heal)
    {
        if (heal < 0)
        {
            Console.WriteLine($"{name} heals 0 HP!");
            heal = 0;
        }
        else
        {
            Console.WriteLine($"{name} heals {heal} HP!");
        }

        float newHp = hp + heal;
        ValidateHP(newHp);
    }

    /// <summary> Sets the player’s HP to a valid value within 0 and maxHp. </summary>
    public void ValidateHP(float newHp)
    {
        if (newHp < 0)
            this.hp = 0;
        else if (newHp > maxHp)
            this.hp = maxHp;
        else
            this.hp = newHp;
    }

    /// <summary> Applies a modifier to a base value and returns the result. </summary>
    public float ApplyModifier(float baseValue, Modifier modifier)
    {
        if (modifier == Modifier.Weak)
            return baseValue * 0.5f;
        else if (modifier == Modifier.Strong)
            return baseValue * 1.5f;
        else
            return baseValue;
    }
}

