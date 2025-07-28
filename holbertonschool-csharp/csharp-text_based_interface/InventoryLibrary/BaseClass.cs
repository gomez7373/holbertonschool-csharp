using System;

namespace InventoryLibrary
{
    /// <summary>Base class with ID and timestamps.</summary>
    public class BaseClass
    {
        /// <summary>Unique identifier for the object.</summary>
        public string id { get; set; }

        /// <summary>Date when the object was created.</summary>
        public DateTime date_created { get; set; }

        /// <summary>Date when the object was last updated.</summary>
        public DateTime date_updated { get; set; }

        /// <summary>Initializes a new instance of the BaseClass.</summary>
        public BaseClass()
        {
            id = Guid.NewGuid().ToString();
            date_created = DateTime.Now;
            date_updated = DateTime.Now;
        }

        /// <summary>Updates the last updated date to now.</summary>
        public void Touch()
        {
            date_updated = DateTime.Now;
        }

        /// <summary>Returns a string representation of the object.</summary>
        public override string ToString()
        {
            return $"{GetType().Name} ({id}) - Created: {date_created}, Updated: {date_updated}";
        }
    }
}
