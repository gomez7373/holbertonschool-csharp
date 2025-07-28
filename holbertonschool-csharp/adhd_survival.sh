#!/bin/bash
# ADHD Survival Script - Holberton C# Text-based Interface Project

solution_name="InventoryManagement"
project_dir="csharp-text_based_interface"

echo "📦 Setting up Holberton project: $solution_name"

# Crear estructura limpia
mkdir -p "$project_dir"
cd "$project_dir" || exit 1
rm -rf InventoryLibrary InventoryManager InventoryManagement.Tests storage $solution_name.sln

# Crear solución y proyectos
dotnet new sln -n "$solution_name"
dotnet new classlib -n InventoryLibrary
dotnet new console -n InventoryManager
dotnet new xunit -n InventoryManagement.Tests

# Agregar proyectos a la solución
dotnet sln "$solution_name.sln" add InventoryLibrary/InventoryLibrary.csproj
dotnet sln "$solution_name.sln" add InventoryManager/InventoryManager.csproj
dotnet sln "$solution_name.sln" add InventoryManagement.Tests/InventoryManagement.Tests.csproj
dotnet add InventoryManager/InventoryManager.csproj reference InventoryLibrary/InventoryLibrary.csproj
dotnet add InventoryManagement.Tests/InventoryManagement.Tests.csproj reference InventoryLibrary/InventoryLibrary.csproj

# Crear carpeta de almacenamiento
mkdir -p storage
echo "{}" > storage/inventory_manager.json

# Crear README
cat > README.md <<EOL
# Inventory Management
Holberton School - C# Text-based Interface Project.
EOL

# Crear BaseClass
cat > InventoryLibrary/BaseClass.cs <<'EOL'
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
EOL

# Crear Item
cat > InventoryLibrary/Item.cs <<'EOL'
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

        /// <summary>Price of the item (limited to 2 decimals).</summary>
        public float price { get; set; }

        /// <summary>Tags for the item.</summary>
        public List<string> tags { get; set; }

        /// <summary>Initializes a new Item with a required name.</summary>
        public Item(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name is required");
            this.name = name;
            tags = new List<string>();
        }
    }
}
EOL

# Crear User
cat > InventoryLibrary/User.cs <<'EOL'
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
    }
}
EOL

# Crear Inventory
cat > InventoryLibrary/Inventory.cs <<'EOL'
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
EOL

# Crear JSONStorage
cat > InventoryLibrary/JSONStorage.cs <<'EOL'
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace InventoryLibrary
{
    /// <summary>Handles JSON storage for all objects.</summary>
    public class JSONStorage
    {
        /// <summary>Dictionary of stored objects (key: ClassName.id, value: object).</summary>
        public Dictionary<string, object> objects { get; set; } = new Dictionary<string, object>();
        private readonly string filePath = Path.Combine("storage", "inventory_manager.json");

        /// <summary>Returns all stored objects.</summary>
        public Dictionary<string, object> All() => objects;

        /// <summary>Adds a new object to storage.</summary>
        public void New(object obj)
        {
            string key = $"{obj.GetType().Name}.{obj.GetType().GetProperty("id").GetValue(obj)}";
            objects[key] = obj;
        }

        /// <summary>Saves objects to a JSON file.</summary>
        public void Save()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            File.WriteAllText(filePath, JsonSerializer.Serialize(objects, options));
        }

        /// <summary>Loads objects from the JSON file.</summary>
        public void Load()
        {
            if (!File.Exists(filePath)) return;
            string json = File.ReadAllText(filePath);
            if (!string.IsNullOrWhiteSpace(json))
                objects = JsonSerializer.Deserialize<Dictionary<string, object>>(json);
        }
    }
}
EOL

# Renombrar Program.cs a InventoryManager.cs
mv InventoryManager/Program.cs InventoryManager/InventoryManager.cs

# Crear código base de InventoryManager.cs
cat > InventoryManager/InventoryManager.cs <<'EOL'
using System;
using InventoryLibrary;

namespace InventoryManagerApp
{
    /// <summary>Console application for managing inventory.</summary>
    class InventoryManager
    {
        static void Main()
        {
            JSONStorage storage = new JSONStorage();
            storage.Load();
            Console.WriteLine("Inventory Manager");
            Console.WriteLine("-------------------------");
            Console.WriteLine("<ClassNames>\n<All>\n<All [ClassName]>\n<Create [ClassName]>\n<Show [ClassName id]>\n<Update [ClassName id]>\n<Delete [ClassName id]>\n<Exit>");
        }
    }
}
EOL

# Build del proyecto
dotnet build

echo "✅ Project setup complete: $solution_name"

