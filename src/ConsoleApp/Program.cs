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
			Console.WriteLine("Sample application demonstrating various ways of prompting user for selection from a list of values");
			ConsoleExtensions.PressAnyKey();


			ProcessEmptyList();


			ProcessPeopleDictionary();
			ProcessPeopleList();

			

			ListPromptOptions<Person> a = new ListPromptOptions<Person>()
			{
				AutoItemSelection = true,
				ColorListSelectionPrompt = 
			}

			// Demonstrates Providing a list, prompting the user for their selection and returning the result.
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
				ListItemDisplay_AsString = DisplayPersonWithAgeFirstDict,
				
				//ListItemDisplay_Custom = WritePersonToConsoleListItem,
			};
			 

			// A - Simple custom display returning the string to be displayed.
			Console.WriteLine("{0}{0}Sample:  Dictionary A:  {0}The following displays the list items using the AsString method.  X or Q for exiting is permitted.", Environment.NewLine, Color.Lime);
			(int returnCode, Person selected)  = people.AskUserToSelectItem(listPromptOptions);
			if (selected != null)
				Console.WriteLine("User selected the item: {0}, {1}", selected.LastName, selected.FirstName, Color.BurlyWood);
			else {
				if (returnCode == ListFX.CHOOSE_LIST_EXIT) Console.WriteLine("User choose to exit");
				else if (returnCode == ListFX.CHOOSE_LIST_NEW) Console.WriteLine("User choose to create new item");
			}
			Console.WriteLine();


			// B - Custom display of line item
			Console.WriteLine("{0}Sample: Dictionary B{0}The following displays the list items using a custom formatting method.  X or Q for exiting is NOT ALLOWED.", Environment.NewLine, Color.Lime);
			listPromptOptions.ListItemDisplay_AsString = null;
			listPromptOptions.ListItemDisplay_Custom = WritePersonToConsoleListItem;
			listPromptOptions.SelectOptionNewItemAllowed = true;


			(returnCode, selected) = people.AskUserToSelectItem(listPromptOptions);
			if (selected != null)
				Console.WriteLine("User selected the item: {0}, {1}", selected.LastName, selected.FirstName, Color.BurlyWood);
			else
			{
				if (returnCode == ListFX.CHOOSE_LIST_EXIT) Console.WriteLine("User choose to exit");
				else if (returnCode == ListFX.CHOOSE_LIST_NEW) Console.WriteLine("User choose to create new item");
			}
			Console.WriteLine();


		}


		/// <summary>
		/// This method demonstrates the processing that occurs when the list is empty.
		/// </summary>
		private static void ProcessEmptyList () {

			Console.WriteLine("{0}Sample - Empty List:{0} In this sample, we are prompting for input when the list contains no values.  Note, we also allow them enter a new item.  ", Environment.NewLine);
			List<Person> noPeople = new List<Person>();
			ListPromptOptions<Person> listPromptOptions = new ListPromptOptions<Person>()
			{
				// Allow for menu item to add a new item
				SelectOptionNewItemAllowed = true, 

				// The text for displaying the singular of the items in the list.
				ItemSingularText = "person",
			};
			(int returnCode, Person selected) = noPeople.AskUserToSelectItem(listPromptOptions);
			if (selected != null) 
				Console.WriteLine("{0}", selected.LastName);
			else {
				if (returnCode == ListFX.CHOOSE_LIST_EXIT)
					Console.WriteLine("User chose to exit the list{0}{0}", Environment.NewLine);
				else if ( returnCode == ListFX.CHOOSE_LIST_NEW ) Console.WriteLine("User chose to add a new item{0}{0}", Environment.NewLine);
				else if (returnCode == ListFX.CHOOSE_LIST_NOTHING)
					Console.WriteLine("Nothing selected{0}{0}", Environment.NewLine);
			}
			
		}


		/// <summary>
		/// Demonstrates displaying the list with a custom function for displaying each list item
		/// </summary>
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
				// This is the method that is called for each list item to display it.
				ListItemDisplay_AsString = DisplayPersonWithAgeFirst
			};



			// A - Simple custom display returning the string to be displayed.
			Console.WriteLine("{0}List Sample A:{0}The following displays the list items using the AsString method which requires the custom function to just return a string of the item.{0}.  X or Q are valid selection items for exiting without selecting.", Environment.NewLine, Color.Lime);
			(int returnCode, Person selected) = people.AskUserToSelectItem(listPromptOptions);
			if (selected != null)
				Console.WriteLine("You selected: {0}, {1}", selected.LastName, selected.FirstName, Color.BurlyWood);
			else
			{
				if (returnCode == ListFX.CHOOSE_LIST_EXIT)
					Console.WriteLine("User chose to exit the list{0}{0}", Environment.NewLine);
				else if (returnCode == ListFX.CHOOSE_LIST_NEW) Console.WriteLine("User chose to add a new item{0}{0}", Environment.NewLine);
				else if (returnCode == ListFX.CHOOSE_LIST_NOTHING)
					Console.WriteLine("Nothing selected{0}{0}", Environment.NewLine);
			}
			Console.WriteLine();




			// B - Custom display of line item
			Console.WriteLine("{0}List Sample B:  The following displays the list items using a custom display method that gives complete control of the line to the method, so you can use custom colors, etc.  X or Q for exiting is NOT ALLOWED.", Environment.NewLine, Color.Lime);
			listPromptOptions.ListItemDisplay_AsString = null;
			listPromptOptions.ListItemDisplay_Custom = WritePersonToConsoleListItem;
			listPromptOptions.SelectOptionMustChooseItem = true;


			(returnCode, selected) = people.AskUserToSelectItem(listPromptOptions);
			if (selected != null)
				Console.WriteLine("User selected the following item: {0}, {1}", selected.LastName, selected.FirstName, Color.BurlyWood);
			else
			{
				if (returnCode == ListFX.CHOOSE_LIST_EXIT)
					Console.WriteLine("User chose to exit the list{0}{0}", Environment.NewLine);
				else if (returnCode == ListFX.CHOOSE_LIST_NEW) Console.WriteLine("User chose to add a new item{0}{0}", Environment.NewLine);
				else if (returnCode == ListFX.CHOOSE_LIST_NOTHING)
					Console.WriteLine("Nothing selected{0}{0}", Environment.NewLine);
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
