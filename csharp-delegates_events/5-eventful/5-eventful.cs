using System;

/// <summary>Modifiers for actions</summary>
public enum Modifier
{
    /// <summary>Weak modifier - 0.5x effect</summary>
    Weak,
    /// <summary>Base modifier - 1.0x effect</summary>
    Base,
    /// <summary>Strong modifier - 1.5x effect</summary>
    Strong
}

/// <summary>Delegate to calculate modifier on a base value</summary>
public delegate float CalculateModifier(float baseValue, Modifier modifier);

/// <summary>Custom EventArgs to pass current HP value</summary>
public class CurrentHPArgs : EventArgs
{
    /// <summary>Current HP value (readonly)</summary>
    public float currentHp { get; }

    /// <summary>Constructor setting the current HP value</summary>
    public CurrentHPArgs(float newHp)
    {
        currentHp = newHp;
    }
}

/// <summary>Represents a game player</summary>
public class Player
{
    private string name;
    private float maxHp;
    private float hp;
    private string status;

    /// <summary>Event triggered on HP check</summary>
    public EventHandler<CurrentHPArgs> HPCheck;

    /// <summary>Constructor to initialize player</summary>
    public Player(string name = "Player", float maxHp = 100f)
    {
        if (maxHp <= 0f)
        {
            Console.WriteLine("maxHp must be greater than 0. maxHp set to 100f by default.");
            maxHp = 100f;
        }

        this.name = name;
        this.maxHp = maxHp;
        this.hp = maxHp;
        this.status = $"{name} is ready to go!";
        this.HPCheck += CheckStatus;
    }

    /// <summary>Prints current health</summary>
    public void PrintHealth()
    {
        Console.WriteLine($"{name} has {hp} / {maxHp} health");
    }

    /// <summary>Delegate method to apply a modifier</summary>
    public float ApplyModifier(float baseValue, Modifier modifier)
    {
        switch (modifier)
        {
            case Modifier.Weak:
                return baseValue * 0.5f;
            case Modifier.Strong:
                return baseValue * 1.5f;
            default:
                return baseValue;
        }
    }

    /// <summary>Calculates and applies damage</summary>
    public void TakeDamage(float damage)
    {
        if (damage < 0f)
            damage = 0f;

        Console.WriteLine($"{name} takes {damage} damage!");
        float newHp = hp - damage;
        ValidateHP(newHp);
    }

    /// <summary>Calculates and applies healing</summary>
    public void HealDamage(float heal)
    {
        if (heal < 0f)
            heal = 0f;

        Console.WriteLine($"{name} heals {heal} HP!");
        float newHp = hp + heal;
        ValidateHP(newHp);
    }

    /// <summary>Validates and updates the HP value</summary>
    public void ValidateHP(float newHp)
    {
        if (newHp < 0f)
            hp = 0f;
        else if (newHp > maxHp)
            hp = maxHp;
        else
            hp = newHp;

        OnCheckStatus(new CurrentHPArgs(hp));
    }

    /// <summary>Checks and updates status based on current HP</summary>
    private void CheckStatus(object sender, CurrentHPArgs e)
    {
        float curr = e.currentHp;
        float half = maxHp / 2;
        float quarter = maxHp / 4;

        if (curr == maxHp)
            status = $"{name} is in perfect health!";
        else if (curr >= half)
            status = $"{name} is doing well!";
        else if (curr >= quarter)
            status = $"{name} isn't doing too great...";
        else if (curr > 0f)
            status = $"{name} needs help!";
        else
            status = $"{name} is knocked out!";

        Console.WriteLine(status);
    }

    /// <summary>Prints warning messages based on critical health levels</summary>
    private void HPValueWarning(object sender, CurrentHPArgs e)
    {
        Console.ForegroundColor = ConsoleColor.Red;

        if (e.currentHp == 0f)
            Console.WriteLine("Health has reached zero!");
        else
            Console.WriteLine("Health is low!");

        Console.ResetColor();
    }

    /// <summary>Attaches warning handler and invokes HPCheck if necessary</summary>
    public void OnCheckStatus(CurrentHPArgs e)
    {
        if (e.currentHp < maxHp / 4f)
            HPCheck += HPValueWarning;

        HPCheck?.Invoke(this, e);
    }
}

