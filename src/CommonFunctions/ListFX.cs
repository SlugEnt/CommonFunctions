using System;
using System.Collections.Generic;
using System.Drawing;

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
		public static int LIST_SELECT_EXIT = -3;



		/// <summary>
		/// Handles all the prompting and inputing of a list selection from the user.
		/// Does not require user to hit enter to accept input, it is smart and can tell if the values entered are enough to make a determination of what the user wanted
		/// <para">All lists should be listed starting at 1.  When the selection is returned it is returned as the index value into the list.</para>
		/// </summary>
		/// <param name="listCount">Number of items in list</param>
		/// <param name="autoSelection">If true, as soon as the user has entered enough digits to uniquely identify their selection, the result is returned without user needing to hit enter.</param>
		/// <param name="useDefaultPrompt">If true a prompt will be displayed for you.  If false, you are providing the prompt prior to calling this.</param>
		/// <returns></returns>
		public static int ChooseListItem(int listCount, bool autoSelection = true, bool useDefaultPrompt = false) {
			int bufferMax = 4;
			char [] inputBuffer = new char[bufferMax];
			int charIndex = -1;
			for ( int i = 0; i < bufferMax; i++ ) inputBuffer [i] = default;

			if ( useDefaultPrompt ) {
				Console.WriteLine("Select item #, Press X to exit without selecting an item:  ");
			}

			int consoleColumn = Console.CursorLeft;

			int currentSelection = -1;

			while (true)
			{
				bool isNumericEntry = false;
				bool enterPressed = false;
				int numericValue = -1;

				// Flush Read Buffer
				while (Console.KeyAvailable) Console.ReadKey(true);

				// Read key
				ConsoleKeyInfo keyPressed = Console.ReadKey(true);

				// Enter key = We use the current selection IF they have typed at least one numeric already
				if ( keyPressed.Key == ConsoleKey.Enter && charIndex > -1) {
					return currentSelection;
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

				if ( keyPressed.Key == ConsoleKey.Q || keyPressed.Key == ConsoleKey.X ) return LIST_SELECT_EXIT;

				// See if numeric
				isNumericEntry = int.TryParse(keyPressed.KeyChar.ToString(), out numericValue);
				
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
				if ( !autoSelection ) continue;

				int value10x = currentSelection * 10;
				if ( value10x > listCount ) {
					// We have a selection, since there is no possibility of having an entry > than this with these starting characters
					return currentSelection - 1;
				}
			}

		}



		/// <summary>
		/// Prompts user to choose an item from a list.  Returns null if the user chose to exit or not select an item. 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="items">The List to be displayed</param>
		/// <param name="ItemDisplayFX">Method used to format the display of each list item. MUST return a string.  If not provided the objects ToString method is used</param>
		/// <param name="typeOfItem">The singular name for the type of item.  So if a list of cars, this would be car.  Or a list of people would be person.  Used in prompt display</param>
		/// <returns></returns>
		public static T AskUserToSelectItemAsString<T> (this List<T> items, string typeOfItem, Func<T, string> ItemDisplayFX = null) where T : class {
			Console.WriteLine();
			if ( items.Count == 0 ) {
				Console.WriteLine("There are no {0}s to be displayed");
				return null;
			}

			
			for ( int i = 0; i < items.Count; i++ ) {
				string item = ItemDisplayFX != null ? ItemDisplayFX(items [i]) : items [i].ToString();
				Console.WriteLine(" ( {0}  )  {1}", (i + 1), item);
			}

			int choice = ChooseListItem(items.Count, true);
			return items [choice];
		}



		/// <summary>
		/// Prompts user to choose an item from a list.  Must provide a method that writes a single item to the console.
		/// <para>Returns null if the user chose to exit or not select an item.</para>
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="items">The List to be displayed</param>
		/// <param name="ItemDisplayFX">Method called to display each item.  This method MUST writeline to the console the item text only.  The return value is not USED</param>
		/// <param name="typeOfItem">The singular name for the type of item.  So if a list of cars, this would be car.  Or a list of people would be person.  Used in prompt display</param>
		/// <returns></returns>
		public static T AskUserToSelectItemAsConsole<T>(this List<T> items, string typeOfItem, Func<T,bool> ItemDisplayFX) where T : class
		{
			Console.WriteLine();
			if (items.Count == 0)
			{
				Console.WriteLine("There are no {0}s to be displayed");
				return null;
			}


			for (int i = 0; i < items.Count; i++)
			{
				Console.Write(" ( {0}  )  ", (i + 1));

				// Call function to finish the item.
				ItemDisplayFX(items [i]);
			}

			int choice = ChooseListItem(items.Count, true);
			return items[choice];
		}
	}
}
