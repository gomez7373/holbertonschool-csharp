using System;
using System.Linq;
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
            PrintCommands();

            while (true)
            {
                Console.Write("\nEnter command: ");
                string? input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input)) continue;
                string[] parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                string cmd = parts[0].ToLower();

                if (cmd == "exit") { storage.Save(); break; }
                else if (cmd == "classnames") ClassNames();
                else if (cmd == "all") All(storage, parts.Length > 1 ? parts[1] : null);
                else if (cmd == "create" && parts.Length > 1) Create(storage, parts[1]);
                else if (cmd == "show" && parts.Length > 2) Show(storage, parts[1], parts[2]);
                else if (cmd == "update" && parts.Length > 2) Update(storage, parts[1], parts[2]);
                else if (cmd == "delete" && parts.Length > 2) Delete(storage, parts[1], parts[2]);
                else Console.WriteLine("Invalid command or missing arguments.");
                PrintCommands();
            }
        }

        static void PrintCommands()
        {
            Console.WriteLine("<ClassNames>\n<All>\n<All [ClassName]>\n<Create [ClassName]>\n<Show [ClassName id]>\n<Update [ClassName id]>\n<Delete [ClassName id]>\n<Exit>");
        }

        static void ClassNames()
        {
            Console.WriteLine("User\nItem\nInventory");
        }

        static void All(JSONStorage storage, string? className)
        {
            foreach (var obj in storage.All())
            {
                if (className == null || obj.Key.StartsWith(className + ".", StringComparison.OrdinalIgnoreCase))
                    Console.WriteLine($"{obj.Key}: {obj.Value}");
            }
        }

        static void Create(JSONStorage storage, string className)
        {
            object newObj;
            if (className.Equals("user", StringComparison.OrdinalIgnoreCase))
            {
                Console.Write("Enter name: "); string name = Console.ReadLine() ?? "";
                newObj = new User(name);
            }
            else if (className.Equals("item", StringComparison.OrdinalIgnoreCase))
            {
                Console.Write("Enter name: "); string name = Console.ReadLine() ?? "";
                newObj = new Item(name);
            }
            else if (className.Equals("inventory", StringComparison.OrdinalIgnoreCase))
            {
                Console.Write("Enter user_id: "); string uid = Console.ReadLine() ?? "";
                Console.Write("Enter item_id: "); string iid = Console.ReadLine() ?? "";
                Console.Write("Enter quantity: "); int qty = int.TryParse(Console.ReadLine(), out int q) ? q : 1;
                newObj = new Inventory(uid, iid, qty);
            }
            else { Console.WriteLine($"{className} is not a valid object type"); return; }

            storage.New(newObj); storage.Save();
            Console.WriteLine($"{className} created successfully.");
        }

        static void Show(JSONStorage storage, string className, string id)
        {
            string key = $"{className}.{id}";
            if (storage.All().ContainsKey(key))
                Console.WriteLine(storage.All()[key]);
            else Console.WriteLine($"Object {id} could not be found");
        }

        static void Update(JSONStorage storage, string className, string id)
        {
            string key = $"{className}.{id}";
            if (!storage.All().ContainsKey(key)) { Console.WriteLine($"Object {id} could not be found"); return; }

            var obj = storage.All()[key];
            Console.WriteLine("Enter property name to update:");
            string? prop = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(prop)) return;

            var property = obj.GetType().GetProperty(prop);
            if (property == null) { Console.WriteLine("Invalid property."); return; }

            Console.Write($"Enter new value for {prop}: ");
            string? val = Console.ReadLine();

            try
            {
                object? converted = Convert.ChangeType(val, property.PropertyType);
                property.SetValue(obj, converted);
                storage.All()[key] = obj;
                storage.Save();
                Console.WriteLine($"{prop} updated.");
            }
            catch { Console.WriteLine("Invalid value."); }
        }

        static void Delete(JSONStorage storage, string className, string id)
        {
            string key = $"{className}.{id}";
            if (storage.All().Remove(key)) { storage.Save(); Console.WriteLine($"{className} deleted."); }
            else Console.WriteLine($"Object {id} could not be found");
        }
    }
}
