using System;

namespace Enemies
{
    class Zombie
    {
        public int health;
        private string name = "(No name)";

        public Zombie()
        {
            health = 0;
        }
        public Zombie(int value)
        {
            if (value >= 0)
                health = value;
            else
                throw new ArgumentException("Health must be greater than or equal to 0");
        }
        public int GetHealth()
        {
            return health;
        }
        public String Name {
        get {
            return name;
        }
        set {
            name = value;
        }
    }
    }
}
