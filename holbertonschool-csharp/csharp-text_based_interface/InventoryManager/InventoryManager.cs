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
