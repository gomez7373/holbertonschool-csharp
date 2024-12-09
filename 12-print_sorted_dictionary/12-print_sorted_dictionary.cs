
using System.Collections.Generic;
using System.Linq;
public static void PrintSorted(Dictionary<string, string> myDict) {
    foreach (var pair in myDict.OrderBy(x => x.Key)) {
        Console.WriteLine($"{pair.Key}: {pair.Value}");
    }
}

