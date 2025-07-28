using System;

namespace InventoryLibrary
{
    /// <summary>Represents a user of the system.</summary>
    public class User : BaseClass
    {
        /// <summary>Name of the user.</summary>
        public string name { get; set; }

        /// <summary>Initializes a new User with a required name.</summary>
        public User(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name is required");
            this.name = name;
        }

        /// <summary>Returns a string representation of the user.</summary>
        public override string ToString()
        {
            return $"User: {name}";
        }
    }
}
