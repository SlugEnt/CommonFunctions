using System;
using System.Collections.Generic;
using System.Drawing;
using Slugent.CommonFunctions;
using SlugEnt.CommonFunctions;
using Console = Colorful.Console;

namespace SlugEnt.CommonFunctions.ConsoleApp
{
	class Program
	{
		static void Main(string[] args) {
			ProcessPeople();

			List<string> values = new List<string> {"a","b","c","d"};
			int answer = 0;

			ListPromptOptions<string> strOptions = new ListPromptOptions<string>();

			// Test List Extension with no custom display function
			string answerStr = values.AskUserToSelectItem(strOptions);
			Console.WriteLine("You selected:  {0}",answerStr);


			// Test ChooseListItem
			int i = values.Count;
			while ( true ) {
				Console.WriteLine("Hello World!   --- Item Count:  {0}", i);
				
				Console.WriteLine("Choose an item from 1 to " + values.Count);
				int value = ListFX.ChooseListItem(i, strOptions);
				int j = 1;
				Console.WriteLine();
				Console.WriteLine("Selected: {0}  --> {1}",value,values[value]);
				Console.WriteLine("Press any key to continue on...");
				Console.ReadKey(true);
			}
		}


		private static void ProcessPeople () {
			List<Person> people = new List<Person>()
			{
				new Person("Luke","SkyWalker",29),
				new Person("James","Kirk",35),
				new Person("Jean-Luc","Pickard",56),
				new Person("Han", "Solo", 32)
			};

			// Define List Prompt options
			ListPromptOptions<Person> listPromptOptions = new ListPromptOptions<Person>()
			{
				ItemSingularText = "person",
				ListItemDisplay_AsString = DisplayPersonWithAgeFirst
				//ListItemDisplay_Custom = WritePersonToConsoleListItem,
			};


			// A - Simple custom display returning the string to be displayed.
			Console.WriteLine("{0}The following displays the list items using the AsString method.  X or Q for exiting is permitted.",Environment.NewLine, Color.Lime);
			Person selected = people.AskUserToSelectItem(listPromptOptions);
			Console.WriteLine("You selected: {0}, {1}",selected.LastName,selected.FirstName,Color.BurlyWood);
			Console.WriteLine();


			// B - Custom display of line item
			Console.WriteLine("{0}The following displays the list items using the AsString method.  X or Q for exiting is NOT ALLOWED.", Environment.NewLine, Color.Lime);
			listPromptOptions.ListItemDisplay_AsString = null;
			listPromptOptions.ListItemDisplay_Custom = WritePersonToConsoleListItem;
			listPromptOptions.MustSelectItem = true;


			selected = people.AskUserToSelectItem(listPromptOptions);
			Console.WriteLine("You selected: {0}, {1}", selected.LastName, selected.FirstName, Color.BurlyWood);


		}


		private static string DisplayPersonWithAgeFirst (Person a) {
			return String.Format(" Age: {0}  -->  {1} {2}", a.Age, a.FirstName, a.LastName);
		}


		private static bool WritePersonToConsoleListItem (Person a) {
			Color ageColor = Color.Green;
			if ( a.Age > 60 ) ageColor = Color.DarkRed;
			else if ( a.Age > 50 ) ageColor = Color.Blue;
			else if ( a.Age > 30 ) ageColor = Color.Yellow;
			Console.Write("[",Color.WhiteSmoke);
			Console.Write(" {0} ",a.Age,ageColor);
			Console.Write("]", Color.WhiteSmoke);

			Color nameColor = Color.Cyan;
			if ( a.LastName == "Pickard" ) nameColor = Color.DarkOrange;
			Console.WriteLine("   -->  {0}, {1}",a.LastName,a.FirstName,nameColor);
			return true;
		}


		private string DisplayValue (string a) {
			//Console.WriteLine("yep - " + a);
			return "Yep - " + a;
		}
	}

}
