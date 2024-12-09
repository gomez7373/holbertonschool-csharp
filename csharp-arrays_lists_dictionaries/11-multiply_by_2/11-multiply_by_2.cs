
using System.Collections.Generic;
public static Dictionary<string, int> MultiplyBy2(Dictionary<string, int> myDict) {
    Dictionary<string, int> result = new Dictionary<string, int>();
    foreach (var pair in myDict) {
        result[pair.Key] = pair.Value * 2;
    }
    return result;
}

