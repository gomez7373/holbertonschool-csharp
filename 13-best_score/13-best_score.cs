
using System.Collections.Generic;
public static string BestScore(Dictionary<string, int> myList) {
    if (myList.Count == 0) return "None";
    KeyValuePair<string, int> maxPair = myList.Aggregate((x, y) => x.Value > y.Value ? x : y);
    return maxPair.Key;
}

