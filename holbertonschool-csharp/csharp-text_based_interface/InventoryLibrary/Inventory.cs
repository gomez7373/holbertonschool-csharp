using System;

namespace InventoryLibrary
{
    /// <summary>Represents the quantity of an item owned by a user.</summary>
    public class Inventory : BaseClass
    {
        /// <summary>ID of the user who owns the item.</summary>
        public string user_id { get; set; }

        /// <summary>ID of the item in the inventory.</summary>
        public string item_id { get; set; }

        private int _quantity;
        /// <summary>Quantity of the item (cannot be negative).</summary>
        public int quantity
        {
            get => _quantity;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Quantity cannot be negative");
                _quantity = value;
            }
        }

        /// <summary>Initializes a new inventory entry.</summary>
        public Inventory(string userId, string itemId, int qty = 1)
        {
            user_id = userId;
            item_id = itemId;
            quantity = qty;
        }

        /// <summary>Returns a string representation of the inventory record.</summary>
        public override string ToString()
        {
            return $"Inventory: User({user_id}) Item({item_id}) Qty: {quantity}";
        }
    }
}
