using System;
using System.Collections.Generic;

namespace InventoryLibrary
{
    /// <summary>Represents an item in the inventory.</summary>
    public class Item : BaseClass
    {
        /// <summary>Name of the item.</summary>
        public string name { get; set; }

        /// <summary>Description of the item.</summary>
        public string description { get; set; }

        /// <summary>Price of the item.</summary>
        public float price { get; set; }

        /// <summary>Tags associated with the item.</summary>
        public List<string> tags { get; set; }

        /// <summary>Initializes a new instance of Item with a required name.</summary>
        public Item(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name is required");
            this.name = name;
            tags = new List<string>();
        }

        /// <summary>Returns a string representation of the item.</summary>
        public override string ToString()
        {
            return $"Item: {name} (${price:F2}) - {description}";
        }
    }
}
