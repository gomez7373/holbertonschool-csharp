
using System.Collections.Generic;
public static int NumberOfKeys(Dictionary<string, string> myDict) {
    int count = 0;
    foreach (var key in myDict.Keys) {
        count++;
    }
    return count;
}

