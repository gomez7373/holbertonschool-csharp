
using System.Collections.Generic;
public static List<int> DeleteAt(List<int> myList, int index) {
    if (index < 0 || index >= myList.Count) {
        Console.WriteLine("Index is out of range");
        return myList;
    }
    myList.RemoveAt(index);
    return myList;
}

