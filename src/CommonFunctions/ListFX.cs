using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using Slugent.CommonFunctions;
using Console = Colorful.Console;

namespace SlugEnt.CommonFunctions
{
	/// <summary>
	///  List related add on functions and extensions
	/// </summary>
	public static class ListFX
	{
		/// <summary>
		/// User selected to exit and thus nothing selected.
		/// </summary>
		public static int CHOOSE_LIST_NOTHING = -2;
		public static int CHOOSE_LIST_EXIT = -3;
		public static int CHOOSE_LIST_NEW = -4;
		

		/// <summary>
		/// Handles all the prompting and inputing of a list selection from the user.
		/// Does not require user to hit enter to accept input, it is smart and can tell if the values entered are enough to make a determination of what the user wanted
		/// <para">All lists should be listed starting at 1.  When the selection is returned it is returned as the index value into the list.</para>
		/// </summary>
		/// <param name="listCount">Number of items in list</param>
		/// <param name="autoSelection">If true, as soon as the user has entered enough digits to uniquely identify their selection, the result is returned without user needing to hit enter.</param>
		/// <returns></returns>
		public static int ChooseListItem<T> (int listCount, ListPromptOptions<T> options) {
			int bufferMax = 4;
			char [] inputBuffer = new char[bufferMax];
			int charIndex = -1;
			int startingInputCol = Console.CursorLeft;
			int currentSelection = -1;
			Console.ForegroundColor = Color.White;
			Console.WriteLine();

			// Clear keyboard buffer
			for ( int i = 0; i < bufferMax; i++ ) inputBuffer [i] = default;
			while (true)
			{
				bool isNumericEntry = false;
				

				// Flush Read Buffer
				while (Console.KeyAvailable) Console.ReadKey(true);

				// Read key
				ConsoleKeyInfo keyPressed = Console.ReadKey(true);

				// Enter key = We use the current selection IF they have typed at least one numeric already
				if ( keyPressed.Key == ConsoleKey.Enter && charIndex > -1) {
					return SetReturn(startingInputCol, currentSelection);
				}

				// If backspace or delete, remove the current key and continue looping
				if ( keyPressed.Key == ConsoleKey.Backspace || keyPressed.Key == ConsoleKey.Delete ) {
					if ( charIndex != -1 ) {
						inputBuffer [charIndex] = default;
						charIndex--;

						// Remove character from console.
						Console.CursorLeft = Console.CursorLeft - 1;
						Console.Write(" ");
						Console.CursorLeft = Console.CursorLeft - 1;
					}
					continue;
				}

				// Only allow not selecting an item if special options are set.
				if ( !options.SelectOptionMustChooseItem ) {
					if ( keyPressed.Key == ConsoleKey.Q || keyPressed.Key == ConsoleKey.X ) return SetReturn(startingInputCol, CHOOSE_LIST_EXIT);
					else if ( keyPressed.Key == ConsoleKey.N && options.SelectOptionNewItemAllowed) return SetReturn(startingInputCol, CHOOSE_LIST_NEW);
				}


				// See if numeric
				isNumericEntry = int.TryParse(keyPressed.KeyChar.ToString(), out int numericValue);
				
				if ( isNumericEntry && charIndex + 1 < bufferMax) {
					inputBuffer [++charIndex] = keyPressed.KeyChar; 
					Console.Write(keyPressed.KeyChar);
				}
				else
					continue;
				// All keys that are not numeric should be handled above.


				// IF the user entered zero and its the first key, then return - invalid to start with zero
				if (charIndex < 0 && numericValue == 0) continue;

				// Store the pressed key into buffer if numeric

				// Convert to string
				string entry = new string(inputBuffer);
				if (!int.TryParse(entry, out currentSelection)) throw new ArgumentException("The input buffer should only contain strings");

				// If selection is > than list count, remove the character and return to top
				if ( currentSelection > listCount ) {
					inputBuffer [charIndex] = default;
					charIndex--;

					// Remove character from console.
					Console.CursorLeft = Console.CursorLeft - 1;
					Console.Write(" ");
					Console.CursorLeft = Console.CursorLeft - 1;
					entry = new string(inputBuffer);
					if ( charIndex < 0 ) {
						currentSelection = charIndex;
						continue;
					}
					if (!int.TryParse(entry, out currentSelection)) throw new ArgumentException("The input buffer should only contain strings");

					continue;
				}

				// See if we can autoselect based upon the choice
				if ( !options.AutoItemSelection ) continue;

				int value10x = currentSelection * 10;
				if ( value10x > listCount ) {
					// We have a selection, since there is no possibility of having an entry > than this with these starting characters
					// Clear the entered text.
					return SetReturn(startingInputCol,currentSelection - 1);
				}
			}
		}

		

		/// <summary>
		/// Prompts user to choose an item from a list.  Returns null if the user chose to exit or create a new item.  the integer in the return will indicate whether Exit or New. 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="items">The List to be displayed</param>
		/// <returns>(int, T) where T is an object in the list.
		/// <para>If int is less than zero, then T will be null and int will indicate the special list function the user choose.</para>
		/// <para>Special list functions could be:  Exit, selecting nothing, Create new item</para></returns>
		public static (int,T) AskUserToSelectItem<T>(this List<T> items, ListPromptOptions<T> options) where T : class
		{
			Console.ForegroundColor = Color.White;
			Console.WriteLine();
			Color promptColor = Color.White;
			if (items.Count == 0) {
				if ( options.UseColorPrompts ) promptColor = options.ColorNoItemsInList;

				Console.WriteLine("There are no {0}s to be displayed",promptColor);
				return (CHOOSE_LIST_NOTHING, null);
			}


			// A.  Base String display 
			if (options.ListItemDisplay_Custom == null) 
				for (int i = 0; i < items.Count; i++)
				{
					string item = options.ListItemDisplay_AsString != null ? options.ListItemDisplay_AsString(items[i]) : items[i].ToString();
					Console.WriteLine(" ( {0}  )  {1}", (i + 1), item);
				}
			else {
				for (int i = 0; i < items.Count; i++)
				{
					Console.Write(" ( {0}  )  ", (i + 1));
					
					// Call function to finish the item.
					options.ListItemDisplay_Custom (items[i]);
				}
			}


			// Display Prompt
			string prompt = "";
			promptColor = options.UseColorPrompts == true ? options.ColorListSelectionPrompt : Color.White;

			if ( options.CustomPrompt != string.Empty ) {
				Console.WriteLine(" {0}", options.CustomPrompt, promptColor);
			}
			else {
				Console.WriteLine("Select one of the following:  ", promptColor);
				DisplaySelectionKey("  --> Item ",'#'," from above",promptColor,Color.Cyan);

				if ( !options.SelectOptionMustChooseItem ) {
					if ( options.SelectOptionNewItemAllowed )
						DisplaySelectionKey("  --> Press ", 'N', " to Create a New " + options.ItemSingularText, promptColor, Color.Green);

					DisplaySelectionKey("  --> Press ", 'X', " to Exit, selecting nothing", promptColor, Color.Red);
				}

				Console.WriteLine();
			}
			

			// Get choice from user.
			int choice = ChooseListItem(items.Count, options);
			if ( choice < 0 ) {
				return (choice, null);
			}
			return (choice,items[choice]);
		}



		private static void DisplaySelectionKey (string prefix, char keyValue, string suffix, Color promptColor,Color keyColor) {
			Color consoleColor = Console.ForegroundColor;
			Console.Write("{0}",prefix,promptColor);
			Console.Write("{0}",keyValue,keyColor);
			Console.Write("{0}",suffix,promptColor);
			Console.ForegroundColor = consoleColor;
			Console.WriteLine();
		}


		/// <summary>
		/// Prompts user to choose an item from a list of Dictionary items.  Returns null if the user chose to exit or not select an item. 
		/// </summary>
		/// <param name="options">The set of options to use to display and operate the selection criteria</param>
		/// <returns></returns>
		public static (int,TValue) AskUserToSelectItem<TKey,TValue>(this Dictionary<TKey,TValue> items, ListPromptOptions<TValue> options) where TValue : class {
			//IList<T> selectionList;
			List<TValue> selectionList = new List<TValue>();

			selectionList = items.Values.ToList();

			return selectionList.AskUserToSelectItem(options);

		}

		
#region "Utility Methods"

		/// <summary>
		/// Used to perform cleanup activities when the ChooseListItems method is returning a value
		/// </summary>
		/// <param name="startingCol"></param>
		/// <param name="itemSelected"></param>
		/// <returns></returns>
		private static int SetReturn(int startingCol, int itemSelected)
		{
			ClearInput(startingCol);
			return itemSelected;
		}


		/// <summary>
		/// Clears the input line for list selection upon selection
		/// </summary>
		/// <param name="startingCol"></param>
		private static void ClearInput(int startingCol)
		{
			Console.CursorLeft = startingCol;
			Console.WriteLine("          ");
			Console.CursorLeft = startingCol;
			Console.ForegroundColor = Color.White;
		}
#endregion
	}

}
