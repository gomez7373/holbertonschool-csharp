using System;

/// <summary>
/// Class representing a player with health values.
/// </summary>
public class Player
{
    private string name;
    private float maxHp;
    private float hp;

    /// <summary>
    /// Constructor for Player. Validates maxHp.
    /// </summary>
    /// <param name="name">Player name</param>
    /// <param name="maxHp">Max health</param>
    public Player(string name = "Player", float maxHp = 100f)
    {
        this.name = name;

        if (maxHp <= 0f)
        {
            Console.WriteLine("maxHp must be greater than 0. maxHp set to 100f by default.");
            maxHp = 100f;
        }

        this.maxHp = maxHp;
        this.hp = maxHp;
    }

    /// <summary>
    /// Prints the player's current health.
    /// </summary>
    public void PrintHealth()
    {
        Console.WriteLine($"{this.name} has {this.hp} / {this.maxHp} health");
    }
}

