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
				
				int value = ListFX.ChooseListItem(3, true);
				int j = 1;
				Console.WriteLine("Selected: {0}  --> {1}",value,values[value]);
				Console.ReadKey();
			}
		}
	}
}
