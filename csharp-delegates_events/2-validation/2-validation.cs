using System;

/// <summary> Delegate to calculate modifier on float amount </summary>
public delegate void CalculateHealth(float amount);

/// <summary> Player class </summary>
class Player
{
    private string name;
    private float maxHp;
    private float hp;

    /// <summary> Constructor with default values </summary>
    public Player(string name = "Player", float maxHp = 100f)
    {
        this.name = name;

        if (maxHp <= 0f)
        {
            this.maxHp = 100f;
            Console.WriteLine("maxHp must be greater than 0. maxHp set to 100f by default.");
        }
        else
            this.maxHp = maxHp;

        this.hp = this.maxHp;
    }

    /// <summary> Prints the player's current health </summary>
    public void PrintHealth()
    {
        Console.WriteLine($"{name} has {hp} / {maxHp} health");
    }

    /// <summary> Decreases player's health by damage amount </summary>
    public void TakeDamage(float damage)
    {
        if (damage < 0f)
            damage = 0f;

        Console.WriteLine($"{name} takes {damage} damage!");

        float newHp = this.hp - damage;
        ValidateHP(newHp);
    }

    /// <summary> Increases player's health by heal amount </summary>
    public void HealDamage(float heal)
    {
        if (heal < 0f)
            heal = 0f;

        Console.WriteLine($"{name} heals {heal} HP!");

        float newHp = this.hp + heal;
        ValidateHP(newHp);
    }

    /// <summary> Validates and sets the new hp value </summary>
    public void ValidateHP(float newHp)
    {
        if (newHp < 0f)
            this.hp = 0f;
        else if (newHp > this.maxHp)
            this.hp = this.maxHp;
        else
            this.hp = newHp;
    }
}

