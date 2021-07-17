using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slugent.CommonFunctions
{
	/// <summary>
	/// Settings used in various Display functions for Lists and prompts
	/// </summary>
	public class ListPromptOptions {
		#region "Color Settings"

		/// <summary>
		/// If true, prompts will utilize Console.Color to be displayed.
		/// </summary>
		public bool UseColorPrompts { get; set; }= true;


		/// <summary>
		/// Color used to display that there are no items in the list.
		/// </summary>
		public Color ColorNoItemsInList { get; set; } = Color.Yellow;


		/// <summary>
		/// Color used to display the list prompt for selection
		/// </summary>
		public Color ColorListSelectionPrompt { get; set; } = Color.DarkOrange;
		#endregion


		/// <summary>
		/// Method to be used to display each list item.  Method should return a formatted string.
		/// </summary>
		public Func<Object,string> ListItemDisplay_AsString { get; set; }
		
		/// <summary>
		/// Method to be used to display each list item.  Method must write the item information to the console and return true;
		/// <para>If this and ListItemDisplay_AsString are set, this overrides</para>
		/// </summary>
		public Func<Object,bool> ListItemDisplay_Custom { get; set; }


		/// <summary>
		/// This is what each item in the list is, such as car, person, name, etc.  Should be in singular case.
		/// </summary>
		public string ItemSingularText { get; set; } = "item";


		/// <summary>
		/// If true, then the user must select an item.  If False, then the option X is available to exit without selecting.
		/// </summary>
		public bool MustSelectItem { get; set; }
		/// <summary>
		/// Constructor
		/// </summary>
		public ListPromptOptions () {}
	}
}
