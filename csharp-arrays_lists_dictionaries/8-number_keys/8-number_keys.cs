using System;
using System.Collections.Generic;

class Dictionary
{
	public static int NumberOfKeys(Dictionary<string, string> myDict)
	{
		int count = 0;

		foreach (var i in myDict)
		{
			count += 1;
		}
		return count;
	}
}
