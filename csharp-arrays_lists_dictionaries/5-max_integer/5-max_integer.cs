
using System.Collections.Generic;
public static int MaxInteger(List<int> myList) {
    if (myList.Count == 0) {
        Console.WriteLine("List is empty");
        return -1;
    }
    int max = int.MinValue;
    foreach (int num in myList) {
        if (num > max) max = num;
    }
    return max;
}

