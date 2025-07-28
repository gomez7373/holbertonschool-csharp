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
