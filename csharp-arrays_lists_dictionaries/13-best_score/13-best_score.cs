using System;
using System.Collections.Generic;
using System.Linq;

class Dictionary
{
    public static string BestScore(Dictionary<string, int> myList)
    {
        if (myList == null || myList.Count == 0)
            return "None";

        KeyValuePair<string, int> best = myList.OrderByDescending(item => item.Value).First();
        return best.Key;
    }
}
