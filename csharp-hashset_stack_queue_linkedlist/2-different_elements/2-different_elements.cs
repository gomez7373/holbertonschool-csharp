using System;
using System.Collections.Generic;

class List
{
    public static List<int> DifferentElements(List<int> list1, List<int> list2)
    {
        List<int> listset = new List<int>();

        list1.Sort();
        list2.Sort();

        int i = 0, j = 0;

        for (; i < list1.Count && j < list2.Count; )
        {
            if (list1[i] == list2[j])
            {
                i++;
                j++;
            }
            else if (list1[i] > list2[j])
            {
                listset.Add(list2[i]);
                j++;
            }
            else
            {
                listset.Add(list1[i]);
                i++;
            }
        }
        for (; i < list1.Count; i++)
            listset.Add(list1[i]);
        for (; j < list2.Count; j++)
            listset.Add(list2[j]);
        return listset;
    }
}
