using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using Slugent.CommonFunctions;
using SlugEnt.CommonFunctions;
using Console = Colorful.Console;

namespace SlugEnt.CommonFunctions.ConsoleApp
{
	class Program
	{
		static void Main(string[] args) {
			ProcessPeopleDictionary();
			ProcessPeopleList();
			

			List<string> values = new List<string> {"a","b","c","d"};
			int answer = 0;

			ListPromptOptions<string> strOptions = new ListPromptOptions<string>();
			strOptions.SelectOptionMustChooseItem = true;
			// Test List Extension with no custom display function
			(int returnCode, string answerStr) = values.AskUserToSelectItem(strOptions);

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


		private static void ProcessPeopleDictionary () {
			Dictionary<string, Person> people = new Dictionary<string, Person>()
			{
				{"SkyWalker", new Person("Luke", "SkyWalker", 29)},
				{"Kirk", new Person("James", "Kirk", 35)},
				{"Pickard", new Person("Jean-Luc", "Pickard", 56)},
				{"Solo", new Person("Han", "Solo", 32)}
			};
		

			// Define List Prompt options
			ListPromptOptions<Person> listPromptOptions = new ListPromptOptions<Person>()
			{
				ItemSingularText = "person",
				ListItemDisplay_AsString = DisplayPersonWithAgeFirstDict
				//ListItemDisplay_Custom = WritePersonToConsoleListItem,
			};


			// A - Simple custom display returning the string to be displayed.
			Console.WriteLine("{0}The following displays the list items using the AsString method.  X or Q for exiting is permitted.", Environment.NewLine, Color.Lime);
			(int returnCode, Person selected)  = people.AskUserToSelectItem(listPromptOptions);
			if (selected != null)
				Console.WriteLine("You selected: {0}, {1}", selected.LastName, selected.FirstName, Color.BurlyWood);
			else {
				if (returnCode == ListFX.CHOOSE_LIST_EXIT) Console.WriteLine("To exit");
				else if (returnCode == ListFX.CHOOSE_LIST_NEW) Console.WriteLine("Create new item");
			}
			Console.WriteLine();


			// B - Custom display of line item
			Console.WriteLine("{0}The following displays the list items using the AsString method.  X or Q for exiting is NOT ALLOWED.", Environment.NewLine, Color.Lime);
			listPromptOptions.ListItemDisplay_AsString = null;
			listPromptOptions.ListItemDisplay_Custom = WritePersonToConsoleListItem;
			listPromptOptions.SelectOptionMustChooseItem = true;
			listPromptOptions.SelectOptionNewItemAllowed = true;


			(returnCode, selected) = people.AskUserToSelectItem(listPromptOptions);
			if (selected != null)
				Console.WriteLine("You selected: {0}, {1}", selected.LastName, selected.FirstName, Color.BurlyWood);
			else
			{
				if (returnCode == ListFX.CHOOSE_LIST_EXIT) Console.WriteLine("To exit");
				else if (returnCode == ListFX.CHOOSE_LIST_NEW) Console.WriteLine("Create new item");
			}
			Console.WriteLine();


		}

		private static void ProcessPeopleList () {
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
			(int returnCode, Person selected) = people.AskUserToSelectItem(listPromptOptions);
			if (selected != null)
				Console.WriteLine("You selected: {0}, {1}", selected.LastName, selected.FirstName, Color.BurlyWood);
			else
			{
				if (returnCode == ListFX.CHOOSE_LIST_EXIT) Console.WriteLine("To exit");
				else if (returnCode == ListFX.CHOOSE_LIST_NEW) Console.WriteLine("Create new item");
			}
			Console.WriteLine();


			// B - Custom display of line item
			Console.WriteLine("{0}The following displays the list items using the AsString method.  X or Q for exiting is NOT ALLOWED.", Environment.NewLine, Color.Lime);
			listPromptOptions.ListItemDisplay_AsString = null;
			listPromptOptions.ListItemDisplay_Custom = WritePersonToConsoleListItem;
			listPromptOptions.SelectOptionMustChooseItem = true;


			(returnCode, selected) = people.AskUserToSelectItem(listPromptOptions);
			if (selected != null)
				Console.WriteLine("You selected: {0}, {1}", selected.LastName, selected.FirstName, Color.BurlyWood);
			else
			{
				if (returnCode == ListFX.CHOOSE_LIST_EXIT) Console.WriteLine("To exit");
				else if (returnCode == ListFX.CHOOSE_LIST_NEW) Console.WriteLine("Create new item");
			}
			Console.WriteLine();
			
		}


		private static string DisplayPersonWithAgeFirst (Person a) {
			return String.Format(" Age: {0}  -->  {1} {2}", a.Age, a.FirstName, a.LastName);
		}


		private static string DisplayPersonWithAgeFirstDict(Person a)
		{
			return String.Format(" Age: {0}  -->  {1} {2}",  a.Age, a.FirstName, a.LastName);
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
