using System;

namespace InventoryLibrary
{
    /// <summary>Base class for all objects in the system.</summary>
    public class BaseClass
    {
        /// <summary>Unique identifier for the object.</summary>
        public string id { get; set; }

        /// <summary>Date when the object was created.</summary>
        public DateTime date_created { get; set; }

        /// <summary>Date when the object was last updated.</summary>
        public DateTime date_updated { get; set; }

        /// <summary>Initializes a new instance of BaseClass.</summary>
        public BaseClass()
        {
            id = Guid.NewGuid().ToString();
            date_created = DateTime.Now;
            date_updated = DateTime.Now;
        }
    }
}
