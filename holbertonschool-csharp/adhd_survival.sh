#!/bin/bash
# ADHD Survival Script - Full Holberton C# Text-based Interface Project

function congratulate() {
  echo "✅ Project '$1' created, CRUD implemented, XML documented, and compiled successfully!"
}

# --- Step 1: Constants ---
solution_name="InventoryManagement"
project_dir="csharp-text_based_interface"

echo "📦 Setting up Holberton project: $solution_name"

mkdir -p "$project_dir"
cd "$project_dir" || exit 1

# --- Step 2: Create solution & projects ---
dotnet new sln -n "$solution_name"
dotnet new classlib -n InventoryLibrary
dotnet new console -n InventoryManager
dotnet new xunit -n InventoryManagement.Tests

dotnet sln add InventoryLibrary/InventoryLibrary.csproj
dotnet sln add InventoryManager/InventoryManager.csproj
dotnet sln add InventoryManagement.Tests/InventoryManagement.Tests.csproj

dotnet add InventoryManager/InventoryManager.csproj reference InventoryLibrary/InventoryLibrary.csproj
dotnet add InventoryManagement.Tests/InventoryManagement.Tests.csproj reference InventoryLibrary/InventoryLibrary.csproj

# --- Step 3: Add xUnit packages ---
dotnet add InventoryManagement.Tests package xunit
dotnet add InventoryManagement.Tests package xunit.runner.visualstudio
dotnet restore

# --- Step 4: Update csproj for XML docs ---
for proj in InventoryLibrary InventoryManager InventoryManagement.Tests; do
  csproj_file="$proj/$proj.csproj"
  cat > "$csproj_file" <<EOL
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>net6.0</TargetFramework>
    <GenerateDocumentationFile>true</GenerateDocumentationFile>
    <DocumentationFile>bin/Debug/net6.0/${proj}.xml</DocumentationFile>
  </PropertyGroup>
</Project>
EOL
done

# --- Step 5: Clean up & storage ---
rm -f InventoryLibrary/Class1.cs
mkdir -p storage
echo "{}" > storage/inventory_manager.json
echo "# $solution_name" > README.md

# --- Step 6: Create InventoryLibrary classes (BaseClass, Item, User, Inventory, JSONStorage) ---
cat > InventoryLibrary/BaseClass.cs <<'EOL'
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
EOL

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
EOL

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

        /// <summary>Returns a string representation of the user.</summary>
        public override string ToString()
        {
            return $"User: {name}";
        }
    }
}
EOL

cat > InventoryLibrary/Inventory.cs <<'EOL'
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
EOL

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
        /// <summary>Dictionary of all stored objects (key: ClassName.id, value: object).</summary>
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
            {
                objects = JsonSerializer.Deserialize<Dictionary<string, object>>(json);
            }
        }

        /// <summary>Deletes an object by key.</summary>
        public bool Delete(string key)
        {
            return objects.Remove(key);
        }

        /// <summary>Gets an object by key.</summary>
        public object Get(string key)
        {
            objects.TryGetValue(key, out var obj);
            return obj;
        }
    }
}
EOL

# --- Step 7: Create InventoryManager.cs with full CRUD ---
cat > InventoryManager/InventoryManager.cs <<'EOL'
using System;
using InventoryLibrary;

namespace InventoryManagerApp
{
    /// <summary>Main application class with CRUD operations.</summary>
    class Program
    {
        static JSONStorage storage = new JSONStorage();

        /// <summary>Application entry point.</summary>
        static void Main()
        {
            storage.Load();
            string input;
            do
            {
                PrintMenu();
                input = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(input)) continue;
                string[] parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                string command = parts[0].ToLower();
                switch (command)
                {
                    case "classnames": PrintClassNames(); break;
                    case "all": PrintAll(parts); break;
                    case "create": Create(parts); break;
                    case "show": Show(parts); break;
                    case "update": Update(parts); break;
                    case "delete": Delete(parts); break;
                    case "exit": break;
                    default: Console.WriteLine("Invalid command."); break;
                }
            } while (input.ToLower() != "exit");

            storage.Save();
            Console.WriteLine("Goodbye!");
        }

        static void PrintMenu()
        {
            Console.WriteLine("\nInventory Manager");
            Console.WriteLine("-------------------------");
            Console.WriteLine("<ClassNames>");
            Console.WriteLine("<All>");
            Console.WriteLine("<All [ClassName]>");
            Console.WriteLine("<Create [ClassName]>");
            Console.WriteLine("<Show [ClassName id]>");
            Console.WriteLine("<Update [ClassName id]>");
            Console.WriteLine("<Delete [ClassName id]>");
            Console.WriteLine("<Exit>");
            Console.Write("\n> ");
        }

        static void PrintClassNames()
        {
            Console.WriteLine("Classes: Item, User, Inventory");
        }

        static void PrintAll(string[] parts)
        {
            foreach (var kv in storage.All())
            {
                if (parts.Length == 2 && !kv.Key.StartsWith(parts[1], StringComparison.OrdinalIgnoreCase)) continue;
                Console.WriteLine($"{kv.Key}: {kv.Value}");
            }
        }

        static void Create(string[] parts)
        {
            if (parts.Length < 2) { Console.WriteLine("Usage: Create [ClassName]"); return; }
            string className = parts[1].ToLower();
            object obj = className switch
            {
                "item" => new Item(Prompt("Name: ")) { description = Prompt("Description: "), price = float.Parse(Prompt("Price: ")) },
                "user" => new User(Prompt("Name: ")),
                "inventory" => new Inventory(Prompt("User ID: "), Prompt("Item ID: "), int.Parse(Prompt("Quantity: "))),
                _ => null
            };
            if (obj == null) { Console.WriteLine($"{parts[1]} is not a valid object type"); return; }
            storage.New(obj);
            storage.Save();
            Console.WriteLine($"{parts[1]} created!");
        }

        static void Show(string[] parts)
        {
            if (parts.Length < 3) { Console.WriteLine("Usage: Show [ClassName id]"); return; }
            string key = $"{parts[1]}.{parts[2]}";
            var obj = storage.Get(key);
            Console.WriteLine(obj ?? $"Object {parts[2]} could not be found");
        }

        static void Update(string[] parts)
        {
            if (parts.Length < 3) { Console.WriteLine("Usage: Update [ClassName id]"); return; }
            string key = $"{parts[1]}.{parts[2]}";
            var obj = storage.Get(key);
            if (obj == null) { Console.WriteLine($"Object {parts[2]} could not be found"); return; }
            Console.WriteLine("Enter property name to update:");
            string prop = Console.ReadLine();
            var property = obj.GetType().GetProperty(prop);
            if (property == null) { Console.WriteLine("Invalid property."); return; }
            Console.WriteLine($"Enter new value for {prop}:");
            string value = Console.ReadLine();
            property.SetValue(obj, Convert.ChangeType(value, property.PropertyType));
            (obj as BaseClass)?.Touch();
            storage.Save();
            Console.WriteLine("Updated successfully.");
        }

        static void Delete(string[] parts)
        {
            if (parts.Length < 3) { Console.WriteLine("Usage: Delete [ClassName id]"); return; }
            string key = $"{parts[1]}.{parts[2]}";
            if (storage.Delete(key)) { storage.Save(); Console.WriteLine("Deleted."); }
            else Console.WriteLine($"Object {parts[2]} could not be found");
        }

        static string Prompt(string message)
        {
            Console.Write(message);
            return Console.ReadLine();
        }
    }
}
EOL

# --- Step 8: Unit tests ---
cat > InventoryManagement.Tests/UnitTest1.cs <<'EOL'
using System;
using Xunit;
using InventoryLibrary;

namespace InventoryManagement.Tests
{
    /// <summary>Unit tests for CRUD functionality.</summary>
    public class UnitTest1
    {
        [Fact]
        public void BaseClass_InitializesCorrectly()
        {
            var obj = new BaseClass();
            Assert.False(string.IsNullOrWhiteSpace(obj.id));
            Assert.True(obj.date_created <= DateTime.Now);
            Assert.True(obj.date_updated <= DateTime.Now);
        }

        [Fact]
        public void CanCreateAndRetrieveItem()
        {
            var storage = new JSONStorage();
            var item = new Item("Test") { description = "desc", price = 10.5f };
            storage.New(item);
            var key = $"Item.{item.id}";
            Assert.True(storage.All().ContainsKey(key));
        }

        [Fact]
        public void CanDeleteObject()
        {
            var storage = new JSONStorage();
            var user = new User("User");
            storage.New(user);
            var key = $"User.{user.id}";
            Assert.True(storage.Delete(key));
        }
    }
}
EOL

# --- Step 9: Clean & build ---
dotnet clean
dotnet build
if [ $? -ne 0 ]; then
  echo "❌ Build failed. Fix errors above."
  exit 1
fi

congratulate "$solution_name"

