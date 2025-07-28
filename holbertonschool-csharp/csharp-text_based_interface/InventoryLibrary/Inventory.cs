using System;

namespace InventoryLibrary
{
    /// <summary>Represents an inventory record linking a user and an item.</summary>
    public class Inventory : BaseClass
    {
        /// <summary>ID of the user.</summary>
        public string user_id { get; set; }

        /// <summary>ID of the item.</summary>
        public string item_id { get; set; }

        private int _quantity;

        /// <summary>Quantity of the item (cannot be negative).</summary>
        public int quantity
        {
            get => _quantity;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Quantity cannot be less than 0");
                _quantity = value;
            }
        }

        /// <summary>Initializes a new Inventory record.</summary>
        public Inventory(string userId, string itemId, int qty = 1)
        {
            user_id = userId;
            item_id = itemId;
            quantity = qty;
        }
    }
}
