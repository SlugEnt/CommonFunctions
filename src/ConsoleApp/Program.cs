using System;
using System.Collections.Generic;
using SlugEnt.CommonFunctions;

namespace SlugEnt.CommonFunctions.ConsoleApp
{
	class Program
	{
		static void Main(string[] args) {
			List<string> values = new List<string> {"a","b","c","d"};

			int i = values.Count;
			while ( true ) {
				Console.WriteLine("Hello World!   --- Item Count:  {0}", i);
				
				Console.WriteLine("Choose an item from 1 to " + values.Count);
				int value = ListFX.ChooseListItem(i, true);
				int j = 1;
				Console.WriteLine();
				Console.WriteLine("Selected: {0}  --> {1}",value,values[value]);
				Console.WriteLine("Press any key to continue on...");
				Console.ReadKey(true);
			}
		}
	}
}
