
using System.Collections.Generic;
public static List<bool> DivisibleBy2(List<int> myList) {
    List<bool> results = new List<bool>();
    foreach (int num in myList) {
        results.Add(num % 2 == 0);
    }
    return results;
}

