using System;
using System.Collections.Generic;

class List
{
    public static List<int> CommonElements(List<int> list1, List<int> list2)
    {
        List<int> listset = new List<int>();

        list1.Sort();
        list2.Sort();

        for (int i = 0, j = 0; i < list1.Count && j < list2.Count; )
        {
            if (list1[i] == list2[j])
            {
                listset.Add(list1[i]);
                i++;
                j++;
            }
            else if (list1[i] > list2[j])
                j++;
            else
                i++;
        }
        return listset;
    }
}
