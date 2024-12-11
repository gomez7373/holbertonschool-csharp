using System;
using System.Collections.Generic;
using System.Linq;

    class Dictionary
    {
        public static void PrintSorted(Dictionary<string, string> myDict)
        {
            foreach (var entry in myDict.OrderBy(entry => entry.Key))
                Console.WriteLine($"{entry.Key}: {entry.Value}");
        }
    }
